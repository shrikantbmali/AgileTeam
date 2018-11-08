using AgileTeam.AppInteraction.IVMs;
using AgileTeam.AppInteraction.Startup;
using AgileTeam.Common.Resources;
using AgileTeam.UserControls;
using AgileTeam.Views;
using System;
using System.Infrastructure;
using System.Infrastructure.Services;
using System.Mvvm.EventArgs;
using System.Windows;
using System.Windows.Controls;

namespace AgileTeam
{
	internal sealed class ApplicationExecuter : IApplicationExecuter
	{
		private Shell _shell;

		private readonly IContainer _iocContainer;
		private readonly IMessengerService _messengerService;
		private Application _application;
		private readonly IBootstrapper _bootstrapper;

		public ApplicationExecuter(IBootstrapper bootstrapper, IMessengerService messengerService, IContainer iocContainer)
		{
			_bootstrapper = bootstrapper;
			_messengerService = messengerService;
			_iocContainer = iocContainer;

			AddBootstrapperEventHandler();
			AddMessengerServiceEventsHandlers(_messengerService);
		}

		public void Start(Application application, string[] args, IStartupManager appStartupManager)
		{
			_application = application;
			_bootstrapper.Boot(args, appStartupManager, _messengerService);
		}

		private void AddBootstrapperEventHandler()
		{
			_bootstrapper.Initialized += Bootstrapper_Initialized;
			_bootstrapper.BootFailed += Bootstrapper_BootFailed;
		}

		private void Bootstrapper_BootFailed(object sender, AssociatableEventArgs<StartupFailureReason> e)
		{
			string message;

			switch (e.AssociatedObject)
			{
				case StartupFailureReason.DatabaseConnectionError:
					message = "Connection with the database failed.";
					break;

				case StartupFailureReason.SqlConnectionStringRetrivalError:
					message = "Error retrieving sql connection string";
					break;

				case StartupFailureReason.IncompleteAccountSetup:
					message = "Account setup is incomplete";
					break;

				case StartupFailureReason.ErrorSettingSqlConnectionString:
					message = "Error in setting sql connection string";
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
			if (e.AssociatedObject == StartupFailureReason.DatabaseConnectionError)
				_messengerService.ShowMessageBox("Error", message, MessageBoxButtons.OK, MessageBoxIcon.Error);

			_application.Shutdown();
		}

		private void Bootstrapper_Initialized(object sender, AssociatableEventArgs<IShellVM> e)
		{
			_shell = new Shell { DataContext = e.AssociatedObject };
			_shell.Show();

			AddMappingInIocContainer();
		}

		private void AddMappingInIocContainer()
		{
			_iocContainer.AddMapping<ILoginVM, LoginView>();
			_iocContainer.AddMapping<ISqlConnectionSetupVM, SqlConnectionSetupView>();
			_iocContainer.AddMapping<IAccountSetupVM, AccountSetupViewModel>();
		}

		private ContentControl ResolveRegion(string region)
		{
			if (Regions.CenterFlatRegion.Equals(region, StringComparison.InvariantCulture))
				return _shell.CenterFlatRegion;

			return null;
		}

		private void ShutDownApplicaiton()
		{
			Cleanup();

			_application.Shutdown();
		}

		private void Cleanup()
		{
			RemoveMessengerServiceEventArgs(_messengerService);

			_iocContainer.Clear();
		}

		private void AddMessengerServiceEventsHandlers(IMessengerService messengerService)
		{
			messengerService.ApplicationShutDownRequested += _messengerService_ApplicationShutDownRequested;
			messengerService.CloseViewRequested += _messengerService_CloseViewRequested;
			messengerService.ShowChildDialog += _messengerService_ShowChildDialog;
			messengerService.ShowStandAloneDialog += _messengerService_ShowStandAloneDialog;
			messengerService.ShowViewRequested += _messengerService_ShowViewRequested;
		}

		private void RemoveMessengerServiceEventArgs(IMessengerService messengerService)
		{
			messengerService.ApplicationShutDownRequested -= _messengerService_ApplicationShutDownRequested;
			messengerService.CloseViewRequested -= _messengerService_CloseViewRequested;
			messengerService.ShowChildDialog -= _messengerService_ShowChildDialog;
			messengerService.ShowStandAloneDialog -= _messengerService_ShowStandAloneDialog;
			messengerService.ShowViewRequested -= _messengerService_ShowViewRequested;
		}

		private void _messengerService_ShowViewRequested(object sender, ShowViewMessengerEventArgs e)
		{
			var contentControl = ResolveRegion(e.Region);

			var userControl = _iocContainer.ResolveFor<UserControl>(e.ViewModelType);
			userControl.DataContext = e.DataContext;

			contentControl.Content = userControl;
		}

		private static void _messengerService_ShowStandAloneDialog(object sender, ShowViewMessengerEventArgs e)
		{
			throw new NotImplementedException();
		}

		private static void _messengerService_ShowChildDialog(object sender, ShowDialogViewMessengerEventArgs e)
		{
			throw new NotImplementedException();
		}

		private static void _messengerService_CloseViewRequested(object sender, MessengerEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void _messengerService_ApplicationShutDownRequested(object sender, EventArgs e)
		{
			ShutDownApplicaiton();
		}
	}
}