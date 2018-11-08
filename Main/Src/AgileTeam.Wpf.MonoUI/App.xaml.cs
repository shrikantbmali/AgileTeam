using System.Infrastructure;
using System.Infrastructure.Services;
using System.Mvvm;
using AgileTeam.Wpf.Logic.AppExecution;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using AgileTeam.Wpf.MonoUI.UserControls;
using AgileTeam.Wpf.MonoUI.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AgileTeam.Wpf.MonoUI
{
	public partial class App
	{
		private Shell _shell;
		private Container _container;
		private readonly Dictionary<ExectionFlowType, Action> _executionFlowChangedSetupFlow =
			new Dictionary<ExectionFlowType, Action>();

		protected override void OnStartup(StartupEventArgs e)
		{
			_shell = new Shell();
			
			Current.MainWindow = _shell;
			
			_shell.Show();

			_container = new Container();

			AddMessengerEventHandler();
			AddAppExecutionFlowEventArgs();
			AddExecutionFlowSetupActions();

			AppExecutionFlow.Instance.Start();
		}

		private void AppExecutionFlow_ExecutionFlowChanged(object sender, ExecutionFlowChangedEvenArgs e)
		{
			_container.Clear();

			_executionFlowChangedSetupFlow[e.CurrentExectionFlow]();
		}

		private void SetupSqlServerSetupExecutionFlow()
		{
			_container.AddMapping<ISqlServerSetupViewModel, SqlServerSetupView>();
		}

		private void SetupUserAccountSetupExecutionFlow()
		{
			_container.AddMapping<IAddUserViewModel, AddUserView>();
		}

		private void SetupMainExecutionFlow()
		{
			_container.AddMapping<ILoginViewModel, LoginView>();
			_container.AddMapping<IMainViewModel, MainView>();
			_container.AddMapping<ISettingsViewModel, SettingsView>();
			_container.AddMapping<ICreateNewProjectViewModel, CreateNewPeojectView>();
			_container.AddMapping<ISelectProjectViewModel, SelectProjectView>();
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

		private void MessengerService_ShowViewRequested(object sender, ShowViewMessengerEventArgs e)
		{
			var view = _container.GetInstance<UserControl>(e.ViewModelType);
			view.DataContext = e.DataContext;

			_shell.ContentControl.Content = view;
		}

		private void MessengerService_ShowChildDialog(object sender, ShowDialogViewMessengerEventArgs e)
		{
			var dialogView = _container.GetInstance<Window>(e.ViewModelType);
			var dialogParentView = _container.GetRelativeView<Window>(e.ViewModelParentType);

			dialogView.Owner = dialogParentView;
			dialogView.DataContext = e.DataContext;

			dialogView.ShowDialog();
		}

		private void MessengerService_StandAloneDialog(object sender, ShowViewMessengerEventArgs e)
		{
			var view = _container.GetInstance<UserControl>(e.ViewModelType);
			view.DataContext = e.DataContext;

			_shell.ContentControl.Content = view;
		}

		private void MessengerService_CloseViewRequested(object sender, MessengerEventArgs e)
		{
			_shell.ContentControl.Content = null;

			var relativeView = _container.GetRelativeView<UserControl>(e.ViewModelType);
			TryDisposeViewModel(relativeView);

			_container.RemoveRelativeView(e.ViewModelType);
		}

		private static void TryDisposeViewModel(FrameworkElement relativeView)
		{
			if (relativeView.DataContext == null)
				return;

			var viewModel = GetViewModelOfView(relativeView);

			if (!viewModel.IsDisposed)
				viewModel.Dispose();
		}

		private static IBindable GetViewModelOfView(FrameworkElement relativeView)
		{
			return ((IBindable)relativeView.DataContext);
		}

		private void AddAppExecutionFlowEventArgs()
		{
			AppExecutionFlow.ApplicationShutdownRequested += ExecutionFlow_ApplicationShutdownRequested;
			AppExecutionFlow.FatalErrorOccured += ExecutionFlow_FatalErrorOccured;
			AppExecutionFlow.ExecutionFlowChanged += AppExecutionFlow_ExecutionFlowChanged;
		}

		private void AddExecutionFlowSetupActions()
		{
			_executionFlowChangedSetupFlow.Add(ExectionFlowType.SqlServerSetup, SetupSqlServerSetupExecutionFlow);
			_executionFlowChangedSetupFlow.Add(ExectionFlowType.UserAccountSetup, SetupUserAccountSetupExecutionFlow);
			_executionFlowChangedSetupFlow.Add(ExectionFlowType.Main, SetupMainExecutionFlow);
		}

		private void AddMessengerEventHandler()
		{
			MessengerService.Instance.CloseViewRequested += MessengerService_CloseViewRequested;
			MessengerService.Instance.ShowViewRequested += MessengerService_ShowViewRequested;
			MessengerService.Instance.ShowChildDialog += MessengerService_ShowChildDialog;
			MessengerService.Instance.ShowStandAloneDialog += MessengerService_StandAloneDialog;
		}

		private void RemoveExecutionFlowSetupActions()
		{
			_executionFlowChangedSetupFlow.Clear();
		}

		private void RemoveAppExecutionFlowEventHandler()
		{
			AppExecutionFlow.ApplicationShutdownRequested -= ExecutionFlow_ApplicationShutdownRequested;
			AppExecutionFlow.FatalErrorOccured -= ExecutionFlow_FatalErrorOccured;
			AppExecutionFlow.ExecutionFlowChanged -= AppExecutionFlow_ExecutionFlowChanged;
		}

		private void RemoveMessengerEventHandler()
		{
			MessengerService.Instance.CloseViewRequested -= MessengerService_CloseViewRequested;
			MessengerService.Instance.ShowViewRequested -= MessengerService_ShowViewRequested;
			MessengerService.Instance.ShowChildDialog -= MessengerService_ShowChildDialog;
			MessengerService.Instance.ShowStandAloneDialog -= MessengerService_StandAloneDialog;
		}
	}
}