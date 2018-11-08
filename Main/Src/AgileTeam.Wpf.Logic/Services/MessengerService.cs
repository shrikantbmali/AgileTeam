using System;
using System.Mvvm;

namespace AgileTeam.Wpf.Logic.Services
{
	public class MessengerService : IMessengerService
	{
		public static event EventHandler<ShowViewMessengerEventArgs> ShowViewRequested;

		public static event EventHandler<ShowDialogViewMessengerEventArgs> ShowChildDialog;

		public static event EventHandler<ShowViewMessengerEventArgs> ShowStandAloneDialog;

		public static event EventHandler<MessengerEventArgs> CloseViewRequested;

		public static IMessengerService Instance { get { return new MessengerService(); } }

		public void ShowView<TViewModel>(IViewModel dataContext)
		{
			var handler = ShowViewRequested;
			if (handler != null)
				handler(this, new ShowViewMessengerEventArgs(typeof(TViewModel), dataContext));
		}

		public void ShowViewAsChildDialog<TViewModel, TViewModelParent>(IViewModel dataContext)
		{
			var handler = ShowChildDialog;
			if (handler != null)
				handler(null, new ShowDialogViewMessengerEventArgs(typeof(TViewModel), typeof(TViewModelParent), dataContext));
		}

		public void ShowViewAsStandAloneDialog<TViewModel>(IViewModel dataContext)
		{
			var handler = ShowStandAloneDialog;
			if (handler != null)
				handler(null, new ShowViewMessengerEventArgs(typeof(TViewModel), dataContext));
		}

		public void CloseView<TViewModel>()
		{
			var handler = CloseViewRequested;
			if (handler != null)
				handler(null, new MessengerEventArgs(typeof(TViewModel)));
		}

		public DialogResult ShowMessageBox(string title, string message, MessageBoxButtons messageBoxButtons,
			MessageBoxIcon mesageBoxIcon = MessageBoxIcon.None)
		{
			throw new NotImplementedException();
		}

		public string ShowSelectFileDialog<TParentViewModel>(bool multiselect, FileSelectionFilter fileSelectionFilter)
		{
			throw new NotImplementedException();
		}
	}
}