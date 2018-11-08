using AgileTeam.AppInteraction.IVMs;
using AgileTeam.AppInteraction.Startup;
using AgileTeam.AppInteraction.VMs;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Infrastructure.Services;

namespace AgileTeam.AppInteraction
{
	public class InteractionModule : IModule
	{
		private readonly IUnityContainer _container;

		public InteractionModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterInstance<IBootstrapper>(new Bootstarpper(_container.Resolve<IServiceLocator>()));
			_container.RegisterType<IStartupManager, AppStartupManager>();

			_container.RegisterType<IAccountSetupVM, AccountSetupVM>();
			_container.RegisterType<ICommandableVM, CommandableVM>();
			_container.RegisterType<ILoginVM, LoginVM>();
			_container.RegisterType<IShellVM, ShellVM>();
			_container.RegisterType<ISqlConnectionSetupVM, SqlConnectionSetupVM>();
			_container.RegisterType<IAccountSetupVM, AccountSetupVM>();

			_container.RegisterInstance<IMessengerService>(new MessengerService());
		}
	}
}