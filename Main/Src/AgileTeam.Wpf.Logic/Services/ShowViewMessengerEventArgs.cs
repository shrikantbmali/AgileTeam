using System;
using System.Mvvm;

namespace AgileTeam.Wpf.Logic.Services
{
	public class ShowViewMessengerEventArgs : MessengerEventArgs
	{
		public IViewModel DataContext { get; private set; }

		public ShowViewMessengerEventArgs(Type viewModelType, IViewModel dataContext)
			: base(viewModelType)
		{
			DataContext = dataContext;
		}
	}
}