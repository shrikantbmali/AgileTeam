using System.Mvvm;

namespace AgileTeam.AppInteraction.Startup
{
	public interface IStartupManager : IProcedure<StartupFailureReason>
	{
		void StartupProcedure();
	}
}