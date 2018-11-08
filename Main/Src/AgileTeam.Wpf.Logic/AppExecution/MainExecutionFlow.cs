using System.Infrastructure.Dialogs;
using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Wpf.Logic.ViewModels;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;

namespace AgileTeam.Wpf.Logic.AppExecution
{
	internal sealed class MainExecutionFlow : AppExecutionFlow
	{
		public MainExecutionFlow(AppExecutionFlow successor)
			: base(successor)
		{
		}

		protected override ExectionFlowType ExectionFlowType
		{
			get { return ExectionFlowType.Main; }
		}

		protected override void Execute()
		{
			ShowLoginScreen();
		}

		private static void ShowLoginScreen()
		{
			var logicWindowViewModel = ViewModelFactory.Instance.GetLoginWindowViewModel(LoginManager.Instance);
			MessengerService.Instance.ShowView<ILoginViewModel>(logicWindowViewModel);

			logicWindowViewModel.Closed += logicWindowViewModel_Closed;
		}

		private static void logicWindowViewModel_Closed(object sender, ViewModelEventArgs e)
		{
			if (e.DialogResult == DialogResult.OK)
			{
				IMainViewModel mainWindowViewModel = ViewModelFactory.Instance.GetMainWindowViewModel();
				MessengerService.Instance.ShowView<IMainViewModel>(mainWindowViewModel);
			}
			else
				ShutDownApplication();
		}
	}
}