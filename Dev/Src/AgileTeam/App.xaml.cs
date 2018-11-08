using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Windows;

namespace AgileTeam
{
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var container = new UnityContainer();

			container.RegisterInstance(Current);
			container.RegisterInstance<IServiceLocator>(new UnityServiceLocator(container));
			container.RegisterInstance<ILoggerFacade>(new EmptyLogger());
			container.RegisterInstance<IModuleCatalog>(
				ModuleCatalog.CreateFromXaml(new Uri("ModuleCatalog.xaml", UriKind.Relative)));

			container.RegisterType<IModuleInitializer, ModuleInitializer>();
			container.RegisterType<IModuleManager, ModuleManager>();

			var moduleManager = container.Resolve<IModuleManager>();

			moduleManager.Run();
		}
	}
}