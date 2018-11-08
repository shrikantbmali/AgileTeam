using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System.Collections.ObjectModel;
using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal class SqlServerSetupVM : AgileTeamVM, ISqlServerSetupViewModel
	{
		public ObservableCollection<string> SqlConnectionNames { get; set; }

		public string ConnectionString { get; private set; }

		public ICommand TestSqlConnectionCommand { get; private set; }

		public ICommand SetupSqlConnectionCommand { get; private set; }

		public SqlServerSetupVM(IMessengerService messengerService)
			: base(messengerService)
		{
			SqlConnectionNames = GetSqlConnectionNames();

			TestSqlConnectionCommand = new RelayCommand<object, string>(null, TestSqlConnection);
			SetupSqlConnectionCommand = new RelayCommand<object, string>(null, SetupSqlConnection);
		}

		private static ObservableCollection<string> GetSqlConnectionNames()
		{
			var sqlConnectionNames = new ObservableCollection<string>();

			foreach (var localSqlServerName in AgileTeamCoreFactory.DatabaseServer.GetLocalSqlServerNames())
				sqlConnectionNames.Add(localSqlServerName);

			return sqlConnectionNames;
		}

		private void SetupSqlConnection(string sqlServerName)
		{
			if (IsSqlServerAlive(sqlServerName))
			{
				ConnectionString = GenerateConnectionString(sqlServerName);

				Close<ISqlServerSetupViewModel>(System.Infrastructure.Dialogs.DialogResult.Cancel, this);
			}
		}

		private static void TestSqlConnection(string sqlServerName)
		{
			if (IsSqlServerAlive(sqlServerName))
			{
			}
		}

		private static bool IsSqlServerAlive(string sqlServerName)
		{
			return AgileTeamCoreFactory.DatabaseServer.IsSqlServerAlive(GenerateConnectionString(sqlServerName));
		}

		private static string GenerateConnectionString(string sqlServerName)
		{
			return AgileTeamCoreFactory.DatabaseServer.GetSqlConnectionString(sqlServerName);
		}
	}
}