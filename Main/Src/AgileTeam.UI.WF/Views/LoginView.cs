using AgileTeam.Wpf.ViewModels.IViewModels;
using System;
using System.Windows.Input;

namespace AgileTeam.UI.WF.Views
{
	internal partial class LoginView : DialogTemplateForm
	{
		private ILoginViewModel ViewModel { get { return (ILoginViewModel)DataContext; } }

		public LoginView()
		{
			InitializeComponent();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			ViewModel.UserName = tbxUserName.Text;
			ViewModel.Password = tbxPassword.Text;

			ViewModel.LoginCommand.ExecuteCommand();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			ViewModel.CloseCommand.ExecuteCommand();
		}

		private void LoginView_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			ViewModel.CloseCommand.ExecuteCommand();
		}
	}

	internal static class CommandExecuter
	{
		public static void ExecuteCommand(this ICommand command, object parameter = null)
		{
			command.Execute(parameter);
		}
	}
}