using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public interface ISqlServerSetupViewModel : IAgileTeamViewModel
	{
		ObservableCollection<string> SqlConnectionNames { get; set; }

		string ConnectionString { get; }

		ICommand TestSqlConnectionCommand { get; }

		ICommand SetupSqlConnectionCommand { get; }
	}
}