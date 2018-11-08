using System;

namespace AgileTeam.Wpf.Logic.Services
{
	public class MessengerEventArgs : EventArgs
	{
		public Type ViewModelType { get; private set; }

		public MessengerEventArgs(Type viewModelType)
		{
			ViewModelType = viewModelType;
		}
	}
}