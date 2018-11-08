using System.Infrastructure.Dialogs;
using System.Infrastructure.Services;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System;
using System.Mvvm;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal abstract class AgileTeamVM : BindableBase, IAgileTeamViewModel
	{
		public bool IsClosing { get; private set; }

		public bool IsClosed { get; private set; }

		public IMessengerService MessengerService { get; private set; }

		public DialogResult DialogResult { get; protected set; }

		protected AgileTeamVM(IMessengerService messengerService)
		{
			MessengerService = messengerService;
		}

		#region EVENTS

		public event EventHandler<ViewModelEventArgs> Closed;

		private void RaiseViewClosedEvent(ViewModelEventArgs e)
		{
			var handler = Closed;
			if (handler != null)
				handler(this, e);
		}

		public void Close<TViewModel>(DialogResult dialogResult, IAgileTeamViewModel viewModel) where TViewModel : IAgileTeamViewModel
		{
			IsClosing = true;

			MessengerService.CloseView<TViewModel>();
			RaiseViewClosedEvent(new ViewModelEventArgs(dialogResult, viewModel));

			IsClosed = true;
		}

		#endregion EVENTS
	}
}