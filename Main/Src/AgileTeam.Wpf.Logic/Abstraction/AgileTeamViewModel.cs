using System.Mvvm;

namespace AgileTeam.Wpf.ViewModels.Abstraction
{
	internal abstract class ViewModelFactoryDependentViewModel : ViewModelBase
	{
		protected IViewModelFactory ViewModelFactory { get; private set; }

		protected ViewModelFactoryDependentViewModel(IViewModelFactory viewModelFactory)
		{
			ViewModelFactory = viewModelFactory;
		}
	}
}