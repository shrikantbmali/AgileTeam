using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Mvvm.Tests
{
	[TestClass]
	public class RelayCommand_On_CanExecute
	{
		[TestMethod]
		public void Should_Return_True_When_The_Can_Execute_Predicate_Is_Null()
		{
			var relayCommand = new RelayCommand(null, o => { });

			Assert.IsTrue(relayCommand.CanExecute(null), "If Can execute predicate it not null then it should return true!");
		}
	}
}