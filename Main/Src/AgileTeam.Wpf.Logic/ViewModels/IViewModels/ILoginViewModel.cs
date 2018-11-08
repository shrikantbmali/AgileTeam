using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	/// <summary>
	/// Represents the Model contract for login view.
	/// </summary>
	public interface ILoginViewModel : IAgileTeamViewModel
	{
		/// <summary>
		/// Command to close the window.
		/// </summary>
		ICommand CloseCommand { get; }

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		string UserName { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		string Password { get; set; }

		/// <summary>
		/// A command to attempt the login.
		/// </summary>
		ICommand LoginCommand { get; }
	}
}