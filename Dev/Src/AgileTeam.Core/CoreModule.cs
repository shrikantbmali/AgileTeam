using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace AgileTeam.Core
{
	public class CoreModule : IModule
	{
		private readonly IUnityContainer _container;

		public CoreModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType<IUserAccountsManager, UserAccountsManager>();
			_container.RegisterInstance<IPasswordEncryptionService>(new PasswordEncryptionService());
			_container.RegisterInstance<IApplicationSettings>(new ApplicationSettings());
		}
	}
}