using AgileTeam.AppInteraction.IVMs;
using System;
using System.Infrastructure.Services;
using System.Mvvm.EventArgs;

namespace AgileTeam.AppInteraction.Startup
{
	public interface IBootstrapper
	{
		event EventHandler<AssociatableEventArgs<IShellVM>> Initialized;

		event EventHandler<AssociatableEventArgs<StartupFailureReason>> BootFailed;

		void Boot(string[] args, IStartupManager startupManager, IMessengerService messengerService);
	}
}