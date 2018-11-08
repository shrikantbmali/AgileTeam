using System.Infrastructure;
using AgileTeam.Wpf.Logic.AppExecution;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using AgileTeam.Wpf.UI.Views;
using System;
using System.Collections.Generic;
using System.Mvvm;
using System.Windows;
using MessageBoxButton = System.Windows.MessageBoxButton;

namespace AgileTeam.Wpf.UI
{
	public partial class App
	{
		private readonly Dictionary<ExectionFlowType, Action> _executionFlowChangedActions =
			new Dictionary<ExectionFlowType, Action>();

		protected override void OnStartup(StartupEventArgs e)
		{
			AddMessengerEventHandler();
			AddAppExecutionFlowEventArgs();
			AddExecutionFlowSetupActions();

			AppExecutionFlow.Instance.Start();
		}

		private void AppExecutionFlow_ExecutionFlowChanged(object sender, ExecutionFlowChangedEvenArgs e)
		{
			Container.Instance.Clear();

			_executionFlowChangedActions[e.CurrentExectionFlow]();
		}

		private static void SetupSqlServerSetupExecutionFlow()
		{
			Container.Instance.AddMapping<ISqlServerSetupViewModel, SqlServerSetupView>();
		}

		private static void SetupUserAccountSetupExecutionFlow()
		{
			Container.Instance.AddMapping<IAddUserViewModel, AddUserView>();
		}

		private static void SetupMainExecutionFlow()
		{
			Container.Instance.AddMapping<ILoginViewModel, LoginView>();
			Container.Instance.AddMapping<IMainViewModel, MainView>();
			Container.Instance.AddMapping<ISettingsViewModel, SettingsView>();
			Container.Instance.AddMapping<ICreateNewProjectViewModel, CreateNewPeojectView>();
			Container.Instance.AddMapping<ISelectProjectViewModel, SelectProjectView>();
		}

		private void ExecutionFlow_ApplicationShutdownRequested(object sender, EventArgs e)
		{
			RemoveMessengerEventHandler();
			RemoveAppExecutionFlowEventHandler();
			RemoveExecutionFlowSetupActions();

			Current.Shutdown();
		}

		private static void ExecutionFlow_FatalErrorOccured(object sender, EventArgs e)
		{
			MessageBox.Show("Unknown Error occurred, Exiting the system", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
			Current.Shutdown();
		}

		private static void MessengerService_ShowViewRequested(object sender, ShowViewMessengerEventArgs e)
		{
			var view = Container.Instance.GetInstance<Window>(e.ViewModelType);
			view.DataContext = e.DataContext;

			view.Show();
		}

		private static void MessengerService_ShowChildDialog(object sender, ShowDialogViewMessengerEventArgs e)
		{
			var dialogView = Container.Instance.GetInstance<Window>(e.ViewModelType);
			var dialogParentView = Container.Instance.GetRelativeView<Window>(e.ViewModelParentType);

			dialogView.Owner = dialogParentView;
			dialogView.DataContext = e.DataContext;

			dialogView.ShowDialog();
		}

		private static void MessengerService_StandAloneDialog(object sender, ShowViewMessengerEventArgs e)
		{
			var view = Container.Instance.GetInstance<Window>(e.ViewModelType);
			view.DataContext = e.DataContext;

			view.ShowDialog();
		}

		private static void MessengerService_CloseViewRequested(object sender, MessengerEventArgs e)
		{
			var relativeView = Container.Instance.GetRelativeView<Window>(e.ViewModelType);
			relativeView.Close();

			TryDisposeViewModel(relativeView);

			Container.Instance.RemoveRelativeView(e.ViewModelType);
		}

		private static void TryDisposeViewModel(FrameworkElement relativeView)
		{
			if (relativeView.DataContext == null)
				return;

			var viewModel = GetViewModelOfView(relativeView);

			if (!viewModel.IsDisposed)
				viewModel.Dispose();
		}

		private static IViewModel GetViewModelOfView(FrameworkElement relativeView)
		{
			return ((IViewModel)relativeView.DataContext);
		}

		private void AddAppExecutionFlowEventArgs()
		{
			AppExecutionFlow.ApplicationShutdownRequested += ExecutionFlow_ApplicationShutdownRequested;
			AppExecutionFlow.FatalErrorOccured += ExecutionFlow_FatalErrorOccured;
			AppExecutionFlow.ExecutionFlowChanged += AppExecutionFlow_ExecutionFlowChanged;
		}

		private void AddExecutionFlowSetupActions()
		{
			_executionFlowChangedActions.Add(ExectionFlowType.SqlServerSetup, SetupSqlServerSetupExecutionFlow);
			_executionFlowChangedActions.Add(ExectionFlowType.UserAccountSetup, SetupUserAccountSetupExecutionFlow);
			_executionFlowChangedActions.Add(ExectionFlowType.Main, SetupMainExecutionFlow);
		}

		private static void AddMessengerEventHandler()
		{
			MessengerService.Instance.CloseViewRequested += MessengerService_CloseViewRequested;
			MessengerService.Instance.ShowViewRequested += MessengerService_ShowViewRequested;
			MessengerService.Instance.ShowChildDialog += MessengerService_ShowChildDialog;
			MessengerService.Instance.ShowStandAloneDialog += MessengerService_StandAloneDialog;
		}

		private void RemoveExecutionFlowSetupActions()
		{
			_executionFlowChangedActions.Clear();
		}

		private void RemoveAppExecutionFlowEventHandler()
		{
			AppExecutionFlow.ApplicationShutdownRequested -= ExecutionFlow_ApplicationShutdownRequested;
			AppExecutionFlow.FatalErrorOccured -= ExecutionFlow_FatalErrorOccured;
			AppExecutionFlow.ExecutionFlowChanged -= AppExecutionFlow_ExecutionFlowChanged;
		}

		private static void RemoveMessengerEventHandler()
		{
			MessengerService.Instance.CloseViewRequested -= MessengerService_CloseViewRequested;
			MessengerService.Instance.ShowViewRequested -= MessengerService_ShowViewRequested;
			MessengerService.Instance.ShowChildDialog -= MessengerService_ShowChildDialog;
			MessengerService.Instance.ShowStandAloneDialog -= MessengerService_StandAloneDialog;
		}
	}
}