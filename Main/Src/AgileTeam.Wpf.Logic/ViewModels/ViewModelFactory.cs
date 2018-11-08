using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal class ViewModelFactory : IViewModelFactory
	{
		private static readonly Lazy<IViewModelFactory> _lazyFactory =
			new Lazy<IViewModelFactory>(() => new ViewModelFactory());

		public static IViewModelFactory Instance
		{
			get { return _lazyFactory.Value; }
		}

		public IMainViewModel GetMainWindowViewModel()
		{
			return new MainVM(MessengerService.Instance);
		}

		public ILoginViewModel GetLoginWindowViewModel(ILoginManager loginManager)
		{
			return new LoginVM(MessengerService.Instance, loginManager);
		}

		public IAddUserViewModel GetAddUserViewModel(bool createUserAsAdmin)
		{
			return new AddUserVM(MessengerService.Instance, createUserAsAdmin);
		}

		public ISqlServerSetupViewModel GetSqlServerSetupViewModel()
		{
			return new SqlServerSetupVM(MessengerService.Instance);
		}

		public ICreateNewProjectViewModel GetCreateNewProjectViewModel()
		{
			return new CreateNewProjectVM(MessengerService.Instance);
		}

		public ISettingsViewModel GetSettingsViewModel()
		{
			return new SettingsVM(MessengerService.Instance);
		}

		public IUserPermissionViewModel GetUserPermissionViewModel(bool createUserAsAdmin)
		{
			return new UserPermissionVM(MessengerService.Instance, createUserAsAdmin);
		}

		public IStartViewModel GetStartViewModel()
		{
			return new StartVM(MessengerService.Instance);
		}

		public ISelectProjectViewModel GetSelectProjectViewModel()
		{
			return new SelectProjectVM(MessengerService.Instance);
		}
	}
}