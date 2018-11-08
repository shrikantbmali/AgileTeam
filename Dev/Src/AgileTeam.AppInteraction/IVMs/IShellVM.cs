using System.Mvvm;

namespace AgileTeam.AppInteraction.IVMs
{
	public interface IShellVM : IBindable<IShellVM>
	{
		void StartApplication();
	}
}