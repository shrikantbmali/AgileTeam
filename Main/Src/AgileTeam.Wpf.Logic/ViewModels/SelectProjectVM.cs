using System.Infrastructure.Services;
using AgileTeam.Core.Tasks;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal class SelectProjectVM : AgileTeamVM, ISelectProjectViewModel
	{
		public SelectProjectVM(IMessengerService messengerService)
			: base(messengerService)
		{
		}

		public Project Project { get; private set; }
	}
}