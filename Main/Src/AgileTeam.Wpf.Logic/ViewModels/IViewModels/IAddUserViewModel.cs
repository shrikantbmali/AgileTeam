using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public interface IAddUserViewModel : IAgileTeamViewModel
	{
		bool IsUserAdded { get; }

		byte[] Image { get; set; }

		string FirstName { get; set; }

		string LastName { get; set; }

		string Username { get; set; }

		string Password { get; set; }

		string PasswordToCheck { get; set; }

		ICommand SelectImageCommand { get; }

		ICommand CreateUserCommand { get; }

		ICommand CancelCommand { get; }

		IUserPermissionViewModel UserPermissionViewModel { get; }
	}
}