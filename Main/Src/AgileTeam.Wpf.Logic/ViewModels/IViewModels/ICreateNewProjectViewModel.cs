using AgileTeam.Core.Tasks;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public interface ICreateNewProjectViewModel : IAgileTeamViewModel
	{
		string ProjectTitle { get; set; }

		string ProjectDescription { get; set; }

		ICommand CancelCommand { get; }

		ICommand CreateProjectCommand { get; }

		Project Project { get; }
	}
}