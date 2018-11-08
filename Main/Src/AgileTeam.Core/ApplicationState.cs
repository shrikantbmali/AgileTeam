using AgileTeam.Dbl.Database;
using System;

namespace AgileTeam.Core
{
	public class ApplicationState
	{
		private static Lazy<ApplicationState> _lazyApplicationState;

		private static readonly object _appStateLock = new object();

		public static ApplicationState Instance
		{
			get
			{
				lock (_appStateLock)
				{
					if (_lazyApplicationState == null)
						throw new InvalidOperationException("ApplicationState is not initialized!");

					return _lazyApplicationState.Value;
				}
			}
		}

		private ApplicationState()
		{
		}

		public static void Initialize(string sqlConnectionString)
		{
			lock (_appStateLock)
			{
				if (_lazyApplicationState != null)
					throw new InvalidOperationException("ApplicationState is already initialized!");

				_lazyApplicationState = new Lazy<ApplicationState>(() => new ApplicationState());

				DatabaseConnection.Initialize(sqlConnectionString);
			}
		}
	}
}