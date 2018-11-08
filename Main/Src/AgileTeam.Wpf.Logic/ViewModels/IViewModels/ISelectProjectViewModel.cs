using AgileTeam.Core.Tasks;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public interface ISelectProjectViewModel : IAgileTeamViewModel
	{
		Project Project { get; }
	}
}