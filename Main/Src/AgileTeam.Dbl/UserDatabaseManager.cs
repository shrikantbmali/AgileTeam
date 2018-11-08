using AgileTeam.Core.Entities;
using AgileTeam.Dbl.Database;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AgileTeam.Dbl
{
	internal class UserDatabaseManager : IUserDatabaseManager
	{
		private readonly DatabaseConnection _dbCon;

		public UserDatabaseManager(DatabaseConnection dbCon)
		{
			_dbCon = dbCon;
		}

		public bool IsAccountSetupCompleted()
		{
			var isUserAccountSetupComplete = string.Format("Select Count(UserName) from {0}", DbConstants.UsersTable);

			var sqlCommand = GetSqlCommand(isUserAccountSetupComplete);

			var dataTable = FillData(new SqlDataAdapter(sqlCommand));

			return (int)dataTable.Rows[0][0] != 0;
		}

		public bool IsUsernameExists(string userName)
		{
			var dataTable = GetUserId(userName);

			return dataTable.Rows.Count == 1;
		}

		private DataTable GetUserId(string userName)
		{
			var isUsernameExistQuery = string.Format("Select {3} From {0} Where {1}='{2}'", DbConstants.UsersTable,
				DbConstants.UserTable_UserName, userName, DbConstants.IdColoumn);
			var sqlCommand = GetSqlCommand(isUsernameExistQuery);

			var dataTable = FillData(new SqlDataAdapter(sqlCommand));
			return dataTable;
		}

		public bool InsertUser(CredentialsEntity credentials, UserPropertiesEntity userProperties,
			UserPermissionsEntity userPermissions)
		{
			var sqlConnection = DatabaseConnection.Instance.GetConnection();
			var trasaction = sqlConnection.BeginTransaction("Insert user transaction");
			try
			{
				var insertUserCredentialsQuery = string.Format("INSERT INTO {0} VALUES ('{1}','{2}')", DbConstants.UsersTable,
					credentials.UserName, credentials.Password);

				var sqlCommand = sqlConnection.CreateCommand();
				sqlCommand.Transaction = trasaction;

				sqlCommand.CommandText = insertUserCredentialsQuery;

				sqlCommand.ExecuteNonQuery();

				var getUserIdQuery = string.Format("Select Id From {0} where {1}='{2}' and {3}='{4}'",
					DbConstants.UsersTable, DbConstants.UserTable_UserName, credentials.UserName, DbConstants.UserTable_PasswordColoumn,
					credentials.Password);

				sqlCommand.CommandText = getUserIdQuery;
				var sqlDataReader = sqlCommand.ExecuteReader();
				sqlDataReader.Read();
				var userId = (int)sqlDataReader[0];
				sqlDataReader.Close();

				var insertIntoUserPropertiesQuery = string.Format("Insert Into {0} Values({1},'{2}','{3}','{4}', {5})",
					DbConstants.UserPropertiesTable, userId, userProperties.Title, userProperties.FirstName,
					userProperties.LastName, 0);

				sqlCommand.CommandText = insertIntoUserPropertiesQuery;
				sqlCommand.ExecuteNonQuery();

				var insertIntoUserPermissionsQuery = string.Format("Insert Into {0} Values({1}, {2}, {3}, {4}, {5}, {6}, {7})",
					"UserPermissions",
					userId,
					userPermissions.CanCreateProject.GetBitValue(),
					userPermissions.CanEditProject.GetBitValue(),
					userPermissions.CanCreateTask.GetBitValue(),
					userPermissions.CanEditTask.GetBitValue(),
					userPermissions.CanAssignTask.GetBitValue(),
					userPermissions.CanCloseTask.GetBitValue());

				sqlCommand.CommandText = insertIntoUserPermissionsQuery;
				sqlCommand.ExecuteNonQuery();

				trasaction.Commit();
				return true;
			}
			catch (Exception)
			{
				trasaction.Rollback();
				return false;
			}
		}

		public UserEntity GetUserEntity(CredentialsEntity credentials)
		{
			var userId = GetUserId(credentials);

			if (userId < 1)
				return null;

			var userPropertiesEntity = GetUserPropertiesEntity(userId);
			var userPermissionsEntity = GetUserPermissionsEntity(userId);

			if (userPropertiesEntity == null || userPermissionsEntity == null)
				return null;

			return new UserEntity(userId, userPropertiesEntity, userPermissionsEntity);
		}

		private UserPermissionsEntity GetUserPermissionsEntity(int userId)
		{
			var userPropertiesQuery = string.Format("SELECT * FROM {0} WHERE {1}={2}", DbConstants.UserPermissionsTable,
				DbConstants.IdColoumn,
				userId);

			var sqlCommand = GetSqlCommand(userPropertiesQuery);

			var sqlDataReader = sqlCommand.ExecuteReader();

			if (!sqlDataReader.Read())
				return null;

			return new UserPermissionsEntity
			{
				CanCreateProject = (bool)sqlDataReader["CanCreateProject"],
				CanEditProject = (bool)sqlDataReader["CanEditProject"],
				CanCreateTask = (bool)sqlDataReader["CanCreateTask"],
				CanEditTask = (bool)sqlDataReader["CanEditTask"],
				CanAssignTask = (bool)sqlDataReader["CanAssignTask"],
				CanCloseTask = (bool)sqlDataReader["CanCloseTask"],
			};
		}

		private UserPropertiesEntity GetUserPropertiesEntity(int userId)
		{
			var userPropertiesQuery = string.Format("SELECT * FROM {0} WHERE {1}={2}", DbConstants.UserPropertiesTable,
				DbConstants.IdColoumn,
				userId);

			var sqlCommand = GetSqlCommand(userPropertiesQuery);

			var sqlDataReader = sqlCommand.ExecuteReader();

			if (!sqlDataReader.Read())
				return null;

			var userPropertiesEntity = new UserPropertiesEntity
			{
				Title = (string)sqlDataReader["Title"],
				FirstName = (string)sqlDataReader["FirstName"],
				LastName = (string)sqlDataReader["LastName"],
				Image = (byte[])sqlDataReader["Image"]
			};

			return userPropertiesEntity;
		}

		private int GetUserId(CredentialsEntity credentials)
		{
			var isUserPresentCheckQuery = string.Format("Select Id From {0} where {1}='{2}' and {3}='{4}'",
				DbConstants.UsersTable, DbConstants.UserTable_UserName, credentials.UserName, DbConstants.UserTable_PasswordColoumn,
				credentials.Password);

			var sqlCommand = GetSqlCommand(isUserPresentCheckQuery);
			var sqlDataReader = sqlCommand.ExecuteReader();

			if (sqlDataReader.Read())
				return (int)sqlDataReader[0];

			return -1;
		}

		private static DataTable FillData(DbDataAdapter sqlDataAdapter)
		{
			var userPropertiesDataTable = new DataTable();
			sqlDataAdapter.Fill(userPropertiesDataTable);
			return userPropertiesDataTable;
		}

		private SqlCommand GetSqlCommand(string query)
		{
			return new SqlCommand(query, _dbCon.GetConnection());
		}
	}

	internal static class DbLinq
	{
		public static int GetBitValue(this bool boolValue)
		{
			return boolValue ? 1 : 0;
		}
	}
}