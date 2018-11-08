using System.Infrastructure.Dialogs;
using System.Infrastructure.Services;
using AgileTeam.Core;
using Microsoft.Win32;

namespace AgileTeam.Wpf.Logic.AppExecution
{
	internal class SqlServerSetupExecutionFlow : AppExecutionFlow
	{
		private const string AgileTeamRegKeyPath = @"Software\AgileTeam";
		private const string ConnectionSettingsKey = "ConSettings";

		public SqlServerSetupExecutionFlow(AppExecutionFlow successor)
			: base(successor)
		{
		}

		protected override ExectionFlowType ExectionFlowType
		{
			get { return ExectionFlowType.SqlServerSetup; }
		}

		protected override void Execute()
		{
			if (IsSqlServerSetupComplete() || SetupSqlServerConnection())
			{
				CheckSqlConnectionAndStartApplication();
			}
			else
			{
				MessengerService.Instance.ShowMessageBox("Error!", "SQL server setup not completed, can not continue!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);

				ShutDownApplication();
			}
		}

		private void CheckSqlConnectionAndStartApplication()
		{
			if (IsSqlServerOnline())
			{
				StartApplicationNormally();
			}
			else
			{
				var result = MessengerService.Instance.ShowMessageBox("Error!",
					"Cannot connect to the SQL Server, do you want to review the connection settings?!", MessageBoxButtons.OKCancel,
					MessageBoxIcon.Error);

				if (result == DialogResult.Cancel)
				{
					ShutDownApplication();
				}
				else
				{
					SetupSqlServerConnection();
					CheckSqlConnectionAndStartApplication();
				}
			}
		}

		private static bool IsSqlServerOnline()
		{
			return true;
		}

		private static bool IsSqlServerSetupComplete()
		{
			var openSubKey = Registry.CurrentUser.OpenSubKey(AgileTeamRegKeyPath);

			return openSubKey != null && openSubKey.GetValue(ConnectionSettingsKey) != null;
		}

		private void StartApplicationNormally()
		{
			ApplicationState.Initialize(GetConnectionString());
			ToSuccessor();
		}

		private static bool SetupSqlServerConnection()
		{
			//TODO need to implement this feature from scratch.

			//var sqlConnectionSetupViewModel = ViewModelFactory.Instance.GetSqlServerSetupViewModel();

			//MessengerService.Instance.ShowViewAsStandAloneDialog<ISqlServerSetupViewModel>(sqlConnectionSetupViewModel);

			//if (sqlConnectionSetupViewModel.DialogResult == DialogResult.Cancel)
			//	return false;

			//AddConnectionStringToRegistry(sqlConnectionSetupViewModel.ConnectionString);

			return true;
		}

		private static void AddConnectionStringToRegistry(string connectionString)
		{
			var agileTeamReg = GetAgileTeamReg();

			if (agileTeamReg == null)
				return;

			agileTeamReg.SetValue(ConnectionSettingsKey, connectionString);
		}

		private static RegistryKey GetAgileTeamReg()
		{
			var agileTeamReg = Registry.CurrentUser.OpenSubKey(AgileTeamRegKeyPath) ??
							   Registry.CurrentUser.CreateSubKey(AgileTeamRegKeyPath);

			return agileTeamReg;
		}

		private static string GetConnectionString()
		{
			var openSubKey = Registry.CurrentUser.OpenSubKey(AgileTeamRegKeyPath);

			return openSubKey != null
				? (string)openSubKey.GetValue(ConnectionSettingsKey)
				: string.Empty;
		}
	}
}