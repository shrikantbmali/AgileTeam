using AgileTeam.Core.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public interface IStartViewModel : IAgileTeamViewModel
	{
		ICommand NewProjectCommand { get; }

		ICommand SelectProjectCommand { get; }

		ICommand SelectRecentProjectCommand { get; }

		event EventHandler ProjectSelected;

		Project Project { get; }

		ObservableCollection<Project> RecentProjects { get; }
	}
}