using System;
using System.Infrastructure.Dialogs;
using System.Mvvm;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public interface IAgileTeamViewModel : IBindable
	{
		bool IsClosing { get; }

		bool IsClosed { get; }

		event EventHandler<ViewModelEventArgs> Closed;

		void Close<TViewModel>(DialogResult dialogResult, IAgileTeamViewModel viewModel)
			where TViewModel : IAgileTeamViewModel;
	}
}