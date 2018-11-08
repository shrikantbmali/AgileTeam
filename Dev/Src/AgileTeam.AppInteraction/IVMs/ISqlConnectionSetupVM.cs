using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.AppInteraction.IVMs
{
	public interface ISqlConnectionSetupVM : IBindable<ISqlConnectionSetupVM>
	{
		bool IsConnectionTested { get; set; }

		string ConnectionString { get; }

		string DataSource { get; set; }

		string DatabaseName { get; set; }

		ICommand SetupConnectionCommand { get; }

		ICommand TestConnectionCommand { get; }

		string UserName { get; set; }

		string Password { get; set; }
	}
}