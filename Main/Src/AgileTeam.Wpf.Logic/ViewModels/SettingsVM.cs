using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal class SettingsVM : AgileTeamVM, ISettingsViewModel
	{
		public SettingsVM(IMessengerService messengerService)
			: base(messengerService)
		{
		}

		public string Title
		{
			get { return LoginManager.Instance.CurrentUser.FirstName + " Settings"; }
		}
	}
}