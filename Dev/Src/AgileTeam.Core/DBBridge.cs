using AgileTeam.Data;

namespace AgileTeam.Core
{
	internal sealed class DBBridge
	{
		private static IDatabaseManager _dbManager;

		public bool IsAccountSetupCompleted()
		{
			return _dbManager.IsAccountSetupCompleted();
		}

		public static void Initialize(IDatabaseManager dbManager)
		{
			_dbManager = dbManager;
		}
	}
}