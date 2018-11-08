using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AgileTeam.Core
{
	internal class PasswordEncryptionService : IPasswordEncryptionService
	{
		public string Hash(string password)
		{
			return GetEncriptedPassword(password);
		}

		private static string GetEncriptedPassword(string password)
		{
			var newsha = SHA1.Create();
			var buffer = Encoding.Unicode.GetBytes(password);
			return GetPasswordHash(newsha.ComputeHash(buffer));
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