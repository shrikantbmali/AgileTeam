using AgileTeam.Core.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AgileTeam.Core
{
	public class UserCredentials : CredentialsEntity
	{
		public UserCredentials(string userName, string password)
			: base(userName, password)
		{
			Password = GetPasswordHash(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(password)));
		}

		private static string GetPasswordHash(IEnumerable<byte> p)
		{
			return p.Aggregate(string.Empty,
				(current, b) =>
					current +
					((b < 100)
						? ((b < 10) ? "00" + b.ToString(CultureInfo.InvariantCulture) : "0" + b.ToString(CultureInfo.InvariantCulture))
						: b.ToString(CultureInfo.InvariantCulture)));
		}
	}
}