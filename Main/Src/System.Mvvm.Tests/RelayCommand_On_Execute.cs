using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Mvvm.Tests
{
	[TestClass]
	public class RelayCommand_On_Execute
	{
		[TestMethod]
		public void Should_Execute_The_Execute_Action_On_Execute()
		{
			var isExecuted = false;
			var relayCommand = new RelayCommand(null, o => isExecuted = true);

			relayCommand.Execute(null);

			Assert.IsTrue(isExecuted, "Should execute the execute action when Execute is called!");
		}
	}
}