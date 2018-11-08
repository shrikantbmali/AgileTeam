using Microsoft.Practices.ServiceLocation;
using System.Mvvm;

namespace AgileTeam.AppInteraction.VMs
{
	internal abstract class VMProviderBindableBase<TViewModel> : BindableBase<TViewModel> where TViewModel : IBindable
	{
		protected IServiceLocator ServiceLocator { get; private set; }

		public VMProviderBindableBase(IServiceLocator serviceLocator)
		{
			ServiceLocator = serviceLocator;
		}
	}
}