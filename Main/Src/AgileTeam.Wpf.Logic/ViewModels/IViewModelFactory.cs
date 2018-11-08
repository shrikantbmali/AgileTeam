using AgileTeam.Core;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	public interface IViewModelFactory
	{
		ILoginViewModel GetLoginWindowViewModel(ILoginManager loginManager);

		IAddUserViewModel GetAddUserViewModel(bool createUserAsAdmin);

		ISqlServerSetupViewModel GetSqlServerSetupViewModel();

		IMainViewModel GetMainWindowViewModel();

		ICreateNewProjectViewModel GetCreateNewProjectViewModel();

		ISettingsViewModel GetSettingsViewModel();

		IUserPermissionViewModel GetUserPermissionViewModel(bool createUserAsAdmin);

		IStartViewModel GetStartViewModel();

		ISelectProjectViewModel GetSelectProjectViewModel();
	}
}