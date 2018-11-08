using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public interface IMainViewModel : IAgileTeamViewModel
	{
		ICommand SettingsCommand { get; }

		ICommand ClosedCommand { get; }

		string UserName { get; }

		IStartViewModel StartViewModel { get; }

		VMVisibility StartViewVisibility { get; set; }

		string HeaderText { get; }
	}
}