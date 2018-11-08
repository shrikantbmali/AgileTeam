using AgileTeam.AppInteraction.IVMs;
using AgileTeam.Core;
using Microsoft.Practices.ServiceLocation;
using System.Common;
using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.AppInteraction.VMs
{
	internal class SqlConnectionSetupVM : VMProviderBindableBase<ISqlConnectionSetupVM>, ISqlConnectionSetupVM
	{
		private readonly DBConnectionBridge _connectionBridge;

		public bool IsConnectionTested
		{
			get { return GetValue<bool>(); }
			set { SetValue(value); }
		}

		public string ConnectionString { get; private set; }

		public string DataSource { get; set; }

		public string DatabaseName { get; set; }

		public ICommand SetupConnectionCommand { get; private set; }

		public ICommand TestConnectionCommand { get; private set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public SqlConnectionSetupVM(IServiceLocator serviceLocator)
			: base(serviceLocator)
		{
			SetupConnectionCommand = new RelayCommand(CanSetupConnection, SetupConnection);
			TestConnectionCommand = new RelayCommand(null, TestConnection);

			_connectionBridge = new DBConnectionBridge();
		}

		private bool CanSetupConnection(object obj)
		{
			return IsConnectionTested;
		}

		private void SetupConnection(object obj)
		{
			ConnectionString = GenerateConnectionString();

			Close(this, ViewModelCloseResult.OK);
		}

		private string GenerateConnectionString()
		{
			return _connectionBridge.GetConnectionString(DataSource, DatabaseName, DBAuthMode.Windows);
		}

		private void TestConnection(object obj)
		{
			var result = _connectionBridge.CheckConnection(GenerateConnectionString());

			IsConnectionTested = result.Value == DBFailureReason.None && result.IsSuccessful;
		}
	}
}