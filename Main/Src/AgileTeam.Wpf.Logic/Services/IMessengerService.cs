using System.Mvvm;

namespace AgileTeam.Wpf.Logic.Services
{
	public interface IMessengerService
	{
		void ShowView<TViewModel>(IViewModel dataContext);

		void ShowViewAsChildDialog<TViewModel, TViewModelParent>(IViewModel dataContext);

		void ShowViewAsStandAloneDialog<TViewModel>(IViewModel dataContext);

		void CloseView<TViewModel>();

		DialogResult ShowMessageBox(string title, string message, MessageBoxButtons messageBoxButtons, MessageBoxIcon mesageBoxIcon = MessageBoxIcon.None);

		string ShowSelectFileDialog<TParentViewModel>(bool multiselect, FileSelectionFilter fileSelectionFilter);
	}
}