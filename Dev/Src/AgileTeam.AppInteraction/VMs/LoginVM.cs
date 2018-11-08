using AgileTeam.AppInteraction.IVMs;
using AgileTeam.Core;
using AgileTeam.DataContext;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Common;
using System.Mvvm;
using System.Mvvm.EventArgs;
using System.Windows.Input;

namespace AgileTeam.AppInteraction.VMs
{
	internal sealed class LoginVM : VMProviderBindableBase<ILoginVM>, ILoginVM
	{
		private readonly IUserAccountsManager _userAccountsManager;

		public ICommand LoginCommand { get; private set; }

		public string Password { get; set; }

		public string UserName { get; set; }

		public LoginVM(IServiceLocator serviceLocator, IUserAccountsManager userAccountsManager)
			: base(serviceLocator)
		{
			_userAccountsManager = userAccountsManager;
			LoginCommand = new RelayCommand(null, Login);
		}

		private void Login(object obj)
		{
			if (string.IsNullOrEmpty(UserName))
				Error = "User name cannot be empty.";
			else if (string.IsNullOrEmpty(Password))
				Error = "Password cannot be empty.";
			else
			{
				var loginResult = _userAccountsManager.Login(new Credential(UserName, Password));

				if (loginResult.IsSuccessful)
					RaiseUserLoggedInEvent(loginResult);
				else
				{
					switch (loginResult.FailureReason)
					{
						case UserAccountFailureReason.ConnectionError:
							Error = "There was a problem with database connection, please try again letter";
							break;

						case UserAccountFailureReason.InvalidUserNameOrPassword:
							Error = "Invalid Username or Password";
							break;

						default:
							throw new ArgumentOutOfRangeException();
					}
				}
			}
		}

		#region EVENTS

		public event EventHandler<AssociatableEventArgs<IResult<IUser>>> UserLoggedIn;

		private void RaiseUserLoggedInEvent(IResult<IUser> loginResult)
		{
			var handler = UserLoggedIn;
			if (handler != null)
				handler(this, new AssociatableEventArgs<IResult<IUser>>(loginResult));
		}

		#endregion EVENTS
	}
}