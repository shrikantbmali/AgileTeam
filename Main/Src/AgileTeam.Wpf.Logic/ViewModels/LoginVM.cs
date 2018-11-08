using System.Infrastructure.Dialogs;
using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal sealed class LoginVM : AgileTeamVM, ILoginViewModel
	{
		private readonly ILoginManager _loginManager;

		public ICommand CloseCommand { get; private set; }

		public ICommand LoginCommand { get; private set; }

		public string UserName
		{
			get { return GetValue<string>(); }
			set { SetValue(value); }
		}

		public string Password
		{
			get { return GetValue<string>(); }
			set { SetValue(value); }
		}

		public LoginVM(IMessengerService messengerService, ILoginManager loginManager)
			: base(messengerService)
		{
			_loginManager = loginManager;

			CloseCommand = new RelayCommand(null, Close);
			LoginCommand = new RelayCommand(CanLogin, Login);
		}

		private bool CanLogin(object obj)
		{
			return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
		}

		private void Login(object obj)
		{
			var loginResult = _loginManager.TryLogin(GetUserCredentials());

			if (!loginResult.Success)
			{
				MessengerService.ShowMessageBox("Invalid Username or Password",
					"Entered wrong user name or password, please try again.", MessageBoxButtons.OK);
				UserName = string.Empty;
				Password = string.Empty;
			}
			else
				Close(DialogResult.OK);
		}

		private UserCredentials GetUserCredentials()
		{
			return new UserCredentials(UserName, Password);
		}

		private void Close(object obj)
		{
			Close(DialogResult.Cancel);
		}

		private void Close(DialogResult dialogResult)
		{
			if (!IsClosing)
				Close<ILoginViewModel>(dialogResult, this);
		}
	}
}