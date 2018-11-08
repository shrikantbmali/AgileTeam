using AgileTeam.AppInteraction.IVMs;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Infrastructure.Services;
using System.Mvvm.EventArgs;

namespace AgileTeam.AppInteraction.Startup
{
	internal sealed class Bootstarpper : IBootstrapper
	{
		private static IShellVM _shellVM;
		private readonly IServiceLocator _serviceProvider;

		public Bootstarpper(IServiceLocator serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public void Boot(string[] args, IStartupManager startupManager, IMessengerService messengerService)
		{
			_shellVM = _serviceProvider.GetInstance<IShellVM>();

			RaiseInitializedEvent(_shellVM);

			startupManager.Completed += appStartupManager_Completed;
			startupManager.Failed += appStartupManager_Failed;

			startupManager.StartupProcedure();
		}

		private void appStartupManager_Failed(object sender, AssociatableEventArgs<StartupFailureReason> e)
		{
			RaiseBootFailedEvent(e.AssociatedObject);
		}

		private void appStartupManager_Completed(object sender, AssociatableEventArgs<StartupFailureReason> e)
		{
			if (e.AssociatedObject == StartupFailureReason.NoError)
				_shellVM.StartApplication();
			else
				RaiseBootFailedEvent(e.AssociatedObject);
		}

		#region EVENTS

		public event EventHandler<AssociatableEventArgs<IShellVM>> Initialized;

		private void RaiseInitializedEvent(IShellVM shellVM)
		{
			var handler = Initialized;
			if (handler != null)
				handler(this, new AssociatableEventArgs<IShellVM>(shellVM));
		}

		public event EventHandler<AssociatableEventArgs<StartupFailureReason>> BootFailed;

		private void RaiseBootFailedEvent(StartupFailureReason startupFailureReason)
		{
			var handler = BootFailed;
			if (handler != null)
				handler(this, new AssociatableEventArgs<StartupFailureReason>(startupFailureReason));
		}

		#endregion EVENTS
	}
}