using AgileTeam.AppInteraction.Startup;
using System.Windows;

namespace AgileTeam
{
	internal interface IApplicationExecuter
	{
		void Start(Application application, string[] args, IStartupManager appStartupManager);
	}
}