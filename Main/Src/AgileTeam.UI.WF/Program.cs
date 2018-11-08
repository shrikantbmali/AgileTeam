using AgileTeam.UI.WF.Views;
using AgileTeam.Wpf.Infrastructure;
using AgileTeam.Wpf.ViewModels.Abstraction;
using AgileTeam.Wpf.ViewModels.IViewModels;
using System;
using System.Mvvm;
using System.Windows.Forms;

namespace AgileTeam.UI.WF
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			CreateViewModelToViewMapping();
			AddMessegingServiceEventHandlers();
			
			StartApplication();
		}

		private static void StartApplication()
		{
			var loginForm = CreateLoginForm();
			loginForm.Show();
			
			Application.Run();
		}

		private static LoginView CreateLoginForm()
		{
			var loginView = ViewModelToViewMapper.Instance.GetInstance<ILoginViewModel, LoginView>();
			
			loginView.DataContext = ViewModelFactory.GetLoginWindowViewModel();

			return loginView;
		}

		private static void CreateViewModelToViewMapping()
		{
			ViewModelToViewMapper.Instance.AddMapping<ILoginViewModel, LoginView>();
			ViewModelToViewMapper.Instance.AddMapping<IMainViewModel, MainForm>();
			ViewModelToViewMapper.Instance.AddMapping<ISettingsViewModel, SettingsForm>();
			ViewModelToViewMapper.Instance.AddMapping<ICreateNewProjectViewModel, CreateNewProjectForm>();
		}

		private static void AddMessegingServiceEventHandlers()
		{
			InfrastructureFactory.Instance.MessengerService.CloseView += MessengerService_CloseView;
			InfrastructureFactory.Instance.MessengerService.OpenView += MessengerService_OpenView;
			InfrastructureFactory.Instance.MessengerService.OpenDialogView += MessengerService_OpenDialogView;
			InfrastructureFactory.Instance.MessengerService.ShutDownApplication += MessengerService_ShutDownApplication;
		}

		private static void MessengerService_ShutDownApplication(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private static void MessengerService_OpenDialogView(object sender, OpenDialogViewMessengerEventArgs e)
		{
			var dialogView = ViewModelToViewMapper.Instance.GetInstance<Form>(e.ViewModelType);
			var dialogParentView = ViewModelToViewMapper.Instance.GetRelativeView<Form>(e.ViewModelParentType);

			dialogView.ShowDialog(dialogParentView);
		}

		private static void MessengerService_OpenView(object sender, OpenViewMessengerEventArgs e)
		{
			var view = ViewModelToViewMapper.Instance.GetInstance<ViewBase>(e.ViewModelType);
			view.DataContext = e.DataContext;

			view.Show();
		}

		private static void MessengerService_CloseView(object sender, MessengerEventArgs e)
		{
			var relativeView = ViewModelToViewMapper.Instance.GetRelativeView<ViewBase>(e.ViewModelType);

			if(relativeView.IsClosed || relativeView.IsClosing)
				return;

			relativeView.Close();

			TryDisposeViewModel(relativeView);

			ViewModelToViewMapper.Instance.RemoveRelativeView(e.ViewModelType);
		}

		private static void TryDisposeViewModel(ViewBase relativeView)
		{
			if (relativeView.DataContext == null)
				return;

			var viewModel = GetViewModelOfView(relativeView);

			if (!viewModel.IsDisposed)
				viewModel.Dispose();

			relativeView.Dispose();
		}

		private static IViewModel GetViewModelOfView(ViewBase relativeView)
		{
			return relativeView.DataContext;
		}
	}
}