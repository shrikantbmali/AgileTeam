using AgileTeam.AppInteraction.IVMs;
using AgileTeam.Common.Resources;
using AgileTeam.Core;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using System;
using System.Infrastructure.Services;
using System.Mvvm;
using System.Mvvm.EventArgs;

namespace AgileTeam.AppInteraction.Startup
{
	internal sealed class AppStartupManager : IStartupManager
	{
		private const string HkeyCurrentUserAgileteam = @"HKEY_CURRENT_USER\AgileTeam";
		private const string ValueName = "connectionString";

		private readonly IServiceLocator _serviceLocator;
		private readonly IMessengerService _messengerService;
		private readonly IApplicationSettings _applicationSettings;

		public AppStartupManager(IServiceLocator serviceLocator, IMessengerService messengerService, IApplicationSettings applicationSettings)
		{
			_serviceLocator = serviceLocator;
			_messengerService = messengerService;
			_applicationSettings = applicationSettings;
		}

		public void StartupProcedure()
		{
			RaiseStartedEvent(StartupFailureReason.None);

			SetupFacade();
		}

		private void SetupFacade()
		{
			var connectionString = GetConnectionString();

			if (!string.IsNullOrEmpty(connectionString))
			{
				CompleteSqlConnectionSetupProcedure(connectionString);
			}
			else
			{
				SetupSqlConnection();
			}
		}

		private void SetupSqlConnection()
		{
			var sqlConnectionSetupVM = _serviceLocator.GetInstance<ISqlConnectionSetupVM>();

			sqlConnectionSetupVM.Closed += sqlConnectionSetupVM_Closed;

			_messengerService.ShowView<ISqlConnectionSetupVM>(sqlConnectionSetupVM,
				Regions.CenterFlatRegion);
		}

		private static string GetConnectionString()
		{
			string connectionString;

			try
			{
				connectionString = (string)Registry.GetValue(HkeyCurrentUserAgileteam, ValueName, string.Empty);
			}
			catch (Exception)
			{
				return null;
			}

			return string.IsNullOrEmpty(connectionString) ? null : connectionString;
		}

		private void CompleteSqlConnectionSetupProcedure(string connectionString)
		{
			var dbConnectionBridge = new DBConnectionBridge();
			var checkConnection = dbConnectionBridge.CheckConnection(connectionString);

			if (checkConnection.IsSuccessful)
			{
				_applicationSettings.SetupDatabaseConnection(connectionString);

				CompleteAccountSetupProcedure();
			}
			else
			{
				RaiseFailedEvent(StartupFailureReason.DatabaseConnectionError);
			}
		}

		private void CompleteAccountSetupProcedure()
		{
			if (_applicationSettings.IsAccountSetupCompleted())
			{
				RaiseCompletedEvent(StartupFailureReason.NoError);
			}
			else
			{
				var accountSetupVM = _serviceLocator.GetInstance<IAccountSetupVM>();

				_messengerService.ShowView<IAccountSetupVM>(accountSetupVM, Regions.CenterFlatRegion);

				accountSetupVM.Closed += accountSetupVM_Closed;
			}
		}

		private void sqlConnectionSetupVM_Closed(ISqlConnectionSetupVM vm, ViewModelCloseResult e)
		{
			if (e == ViewModelCloseResult.OK)
			{
				if (!string.IsNullOrEmpty(vm.ConnectionString))
				{
					StoreConnectionString(vm.ConnectionString);
					SetupFacade();
					return;
				}
			}

			RaiseFailedEvent(StartupFailureReason.SqlConnectionStringRetrivalError);
		}

		private void StoreConnectionString(string connectionString)
		{
			try
			{
				Registry.SetValue(HkeyCurrentUserAgileteam, ValueName, connectionString);
			}
			catch (Exception)
			{
				RaiseFailedEvent(StartupFailureReason.ErrorSettingSqlConnectionString);
			}
		}

		private void accountSetupVM_Closed(IAccountSetupVM sender, ViewModelCloseResult e)
		{
			if (e == ViewModelCloseResult.OK)
			{
				if (sender.IsAccountSetupComplete)
					SetupFacade();

				return;
			}

			RaiseFailedEvent(StartupFailureReason.IncompleteAccountSetup);
		}

		#region EVENTS

		public event EventHandler<AssociatableEventArgs<StartupFailureReason>> Started;

		private void RaiseStartedEvent(StartupFailureReason e)
		{
			var handler = Started;
			if (handler != null)
				handler(this, new AssociatableEventArgs<StartupFailureReason>(e));
		}

		public event EventHandler<AssociatableEventArgs<StartupFailureReason>> Failed;

		private void RaiseFailedEvent(StartupFailureReason e)
		{
			var handler = Failed;
			if (handler != null)
				handler(this, new AssociatableEventArgs<StartupFailureReason>(e));
		}

		public event EventHandler<AssociatableEventArgs<StartupFailureReason>> Completed;

		private void RaiseCompletedEvent(StartupFailureReason e)
		{
			var handler = Completed;
			if (handler != null)
				handler(this, new AssociatableEventArgs<StartupFailureReason>(e));
		}

		#endregion EVENTS
	}
}