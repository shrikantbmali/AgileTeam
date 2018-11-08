using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Mvvm.Tests
{
	[TestClass]
	public class RelayCommand_On_Instantiation
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), "The Execute action cannot be null!")]
		public void Should_Throw_ArgumentNullException_If_Execute_Action_Is_Null()
		{
			var relayCommand = new RelayCommand(null, null);
		}
	}
}