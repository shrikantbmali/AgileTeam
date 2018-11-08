using AgileTeam.AppInteraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AgileTeam.AppInteractions.Tests.BoostrapperTests
{
	[TestClass]
	public class Bootstrapper_On_Initialize
	{
		[TestInitialize]
		public void Initialize()
		{
			if (!Bootstarpper.IsInitizlied)
				Bootstarpper.Intialize(ShellVM.Instance);
		}

		[TestMethod]
		public void Should_Instantiate_The_Bootstraper()
		{
			Assert.IsNotNull(Bootstarpper.Instance, "Bootstrapper is not initialized!");
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Should_Thorw_InvalidOperationExecption_On_ReInitalization_Try()
		{
			Bootstarpper.Intialize(ShellVM.Instance);
		}
	}
}