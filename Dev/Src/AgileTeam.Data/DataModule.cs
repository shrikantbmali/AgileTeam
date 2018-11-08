using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace AgileTeam.Data
{
	public class DataModule : IModule
	{
		private readonly IUnityContainer _container;

		public DataModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterInstance<IDatabaseManager>(new DatabaseManager());
		}
	}
}