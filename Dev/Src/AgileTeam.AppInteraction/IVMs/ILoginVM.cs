using AgileTeam.Core;
using System;
using System.Common;
using System.Mvvm;
using System.Mvvm.EventArgs;
using System.Windows.Input;

namespace AgileTeam.AppInteraction.IVMs
{
	public interface ILoginVM : IBindable<ILoginVM>
	{
		ICommand LoginCommand { get; }

		string Password { get; set; }

		string UserName { get; set; }

		event EventHandler<AssociatableEventArgs<IResult<IUser>>> UserLoggedIn;
	}
}