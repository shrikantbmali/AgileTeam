using System;
using System.Infrastructure.Dialogs;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public class ViewModelEventArgs : EventArgs
	{
		public DialogResult DialogResult { get; private set; }

		public IAgileTeamViewModel ViewModel { get; private set; }

		public ViewModelEventArgs(DialogResult dialogResult, IAgileTeamViewModel viewModel)
		{
			DialogResult = dialogResult;
			ViewModel = viewModel;
		}
	}
}