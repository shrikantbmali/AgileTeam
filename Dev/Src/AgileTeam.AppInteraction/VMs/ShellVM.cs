using AgileTeam.AppInteraction.IVMs;
using AgileTeam.Common.Resources;
using AgileTeam.Core;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Common;
using System.Infrastructure.Dialogs;
using System.Infrastructure.Services;
using System.Mvvm;
using System.Mvvm.EventArgs;

namespace AgileTeam.AppInteraction.VMs
{
	internal sealed class ShellVM : VMProviderBindableBase<IShellVM>, IShellVM
	{
		private static readonly object _lock = new object();
		private static IMessengerService _messengerService;

		public ShellVM(IServiceLocator serviceProvider, IMessengerService messengerService)
			: base(serviceProvider)
		{
			lock (_lock)
			{
				if (messengerService == null)
					throw new ArgumentNullException("messengerService");

				if (serviceProvider == null)
					throw new ArgumentNullException("serviceProvider");

				_messengerService = messengerService;
			}
		}

		public void StartApplication()
		{
			_messengerService.ShowView<ILoginVM>(GetLoginVM(), Regions.CenterFlatRegion);
		}

		private ILoginVM GetLoginVM()
		{
			var loginManagerVM = ServiceLocator.GetInstance<ILoginVM>();

			loginManagerVM.UserLoggedIn += loginManagerVM_UserLoggedIn;
			loginManagerVM.Closed += loginManagerVM_Closed;

			return loginManagerVM;
		}

		private void loginManagerVM_UserLoggedIn(object sender, AssociatableEventArgs<IResult<IUser>> e)
		{
			StartupMainApplicationCource(e.AssociatedObject.Value);
		}

		private static void loginManagerVM_Closed(ILoginVM sender, ViewModelCloseResult e)
		{
			var choice = _messengerService.ShowMessageBox("Warning",
				"Application will close, do you want to close application?",
				MessageBoxButtons.OKCancel);

			if (choice == DialogResult.OK)
				_messengerService.ShutDownApplication();
		}

		private void StartupMainApplicationCource(IUser user)
		{
			throw new NotImplementedException();
		}
	}
}