using AgileTeam.AppInteraction.Startup;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System.Infrastructure;
using System.Infrastructure.Services;
using System.Windows;

namespace AgileTeam
{
	public class MainModule : IModule
	{
		private readonly IUnityContainer _container;
		private readonly Application _application;

		public MainModule(IUnityContainer container, Application application)
		{
			_container = container;
			_application = application;
		}

		public void Initialize()
		{
			_container.RegisterType<IApplicationExecuter, ApplicationExecuter>();
			_container.RegisterInstance<IContainer>(new IocContainer());
			_container.RegisterInstance<IMessengerService>(new MessengerService());
			_container.RegisterInstance<IApplicationExecuter>(new ApplicationExecuter(_container.Resolve<IBootstrapper>(),
				_container.Resolve<IMessengerService>(), _container.Resolve<IContainer>()));

			var applicationExecuter = _container.Resolve<IApplicationExecuter>();

			applicationExecuter.Start(_application, new[] { "" }, _container.Resolve<IStartupManager>());
		}
	}
}