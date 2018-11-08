using AgileTeam.AppInteraction;
using AgileTeam.AppInteraction.IVMs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgileTeam.AppInteractions.Tests.BoostrapperTests
{
	[TestClass]
	public class Bootstrapper_On_Boot
	{
		[TestInitialize]
		public void Initialize()
		{
			if (!Bootstarpper.IsInitizlied)
				Bootstarpper.Intialize(ShellVM.Instance);
		}

		[TestMethod]
		public void Should_Raise_Initialized_Event()
		{
			var isIntializedEventRaised = false;
			Bootstarpper.Instance.Initialized += (sender, args) => isIntializedEventRaised = true;

			Bootstarpper.Instance.Boot();

			Assert.IsTrue(isIntializedEventRaised, "On Boot call the Initialized event should be raised!");
		}

		[TestMethod]
		public void Should_Give_Back_The_Instance_Of_IShellViewModel_In_Intialized_Event_Args()
		{
			IShellVM shellVM = null;
			Bootstarpper.Instance.Initialized += (sender, args) => shellVM = args.AssociatedObject;
			
			Bootstarpper.Instance.Boot();
			
			Assert.IsNotNull(shellVM, "Shell ViewModel is NULL");
		}
	}
}