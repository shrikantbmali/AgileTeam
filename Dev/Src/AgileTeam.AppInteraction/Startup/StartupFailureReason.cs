namespace AgileTeam.AppInteraction.Startup
{
	public enum StartupFailureReason
	{
		/// <summary>
		/// Represent no status, not even No error. <remarks> This status should have no
		/// significance in the code. </remarks>
		/// </summary>
		None,

		NoError,

		DatabaseConnectionError,

		SqlConnectionStringRetrivalError,

		IncompleteAccountSetup,

		ErrorSettingSqlConnectionString
	}
}