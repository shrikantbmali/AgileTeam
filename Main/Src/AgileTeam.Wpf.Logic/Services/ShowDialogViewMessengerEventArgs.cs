using System;
using System.Mvvm;

namespace AgileTeam.Wpf.Logic.Services
{
	public class ShowDialogViewMessengerEventArgs : ShowViewMessengerEventArgs
	{
		public Type ViewModelParentType { get; private set; }

		public ShowDialogViewMessengerEventArgs(Type viewModelType, Type viewModelParentType, IViewModel dataContext = null)
			: base(viewModelType, dataContext)
		{
			ViewModelParentType = viewModelParentType;
		}
	}
}