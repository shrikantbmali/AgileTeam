using AgileTeam.AppInteraction.IVMs;
using AgileTeam.Core;
using AgileTeam.DataContext;
using Microsoft.Practices.ServiceLocation;
using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.AppInteraction.VMs
{
	internal class AccountSetupVM : VMProviderBindableBase<IAccountSetupVM>, IAccountSetupVM
	{
		private readonly IUserAccountsManager _userAccountsManager;

		public bool IsAccountSetupComplete { get; private set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string RePassword { get; set; }

		public ICommand CreateAdminAccountCommand { get; private set; }

		public AccountSetupVM(IServiceLocator serviceLocator, IUserAccountsManager userAccountsManager)
			: base(serviceLocator)
		{
			_userAccountsManager = userAccountsManager;
			CreateAdminAccountCommand = new RelayCommand(CanCreateAccount, CreateAccount);
		}

		private bool CanCreateAccount(object obj)
		{
			return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(RePassword);
		}

		private void CreateAccount(object obj)
		{
			var result = _userAccountsManager.CreateUser(new Credential(UserName, Password));

			if (result.IsSuccessful)
			{
			}
			else
			{
				Error = result.Message;
			}
		}
	}
}