using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.AppInteraction.IVMs
{
	public interface IAccountSetupVM : IBindable<IAccountSetupVM>
	{
		bool IsAccountSetupComplete { get; }

		string UserName { get; set; }

		string Password { get; set; }

		string RePassword { get; set; }

		ICommand CreateAdminAccountCommand { get; }
	}
}