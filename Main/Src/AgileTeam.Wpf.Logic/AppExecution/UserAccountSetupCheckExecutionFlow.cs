using System.Infrastructure.Dialogs;
using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Wpf.Logic.ViewModels;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System.Infrastructure;

namespace AgileTeam.Wpf.Logic.AppExecution
{
	internal sealed class UserAccountSetupCheckExecutionFlow : AppExecutionFlow
	{
		public UserAccountSetupCheckExecutionFlow(AppExecutionFlow successor)
			: base(successor)
		{
		}

		protected override ExectionFlowType ExectionFlowType
		{
			get { return ExectionFlowType.UserAccountSetup; }
		}

		protected override void Execute()
		{
			if (LoginManager.Instance.IsUserAccountSetupCompleted())
				ToSuccessor();
			else
				AddFirstUser();
		}

		private void AddFirstUser()
		{
			var addUserViewModel = ViewModelFactory.Instance.GetAddUserViewModel(true);
			MessengerService.Instance.ShowView<IAddUserViewModel>(addUserViewModel);
			addUserViewModel.Closed += addUserViewModel_Closed;
		}

		private void addUserViewModel_Closed(object sender, ViewModelEventArgs e)
		{
			if (e.DialogResult == DialogResult.OK)
			{
				if (((IAddUserViewModel)(e.ViewModel)).IsUserAdded)
				{
					ToSuccessor();
				}
				else
				{
					MessengerService.Instance.ShowMessageBox("Aborted!", "No user is created, application is exiting.",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
					ShutDownApplication();
				}
				
			}
		}
	}
}