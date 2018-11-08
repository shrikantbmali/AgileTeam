namespace AgileTeam.Data
{
	public abstract class DBContext
	{
		protected IDatabaseManager DBManager { get; set; }

		protected DBContext(IDatabaseManager dbManager)
		{
			DBManager = dbManager;
		}
	}
}