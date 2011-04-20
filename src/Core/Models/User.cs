using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace RavenDbBlog.Core.Models
{
	public class User
	{
		const string ConstantSalt = "xi07cevs01q4#";
		public string Id { get; set; }
		public string FullName { get; set; }
		public string Username { get; set; }
		protected string HashedPassword { get; private set; }

		public void SetPassword(string pwd)
		{
			HashedPassword = GetHashedPassword(pwd);
		}

		private string GetHashedPassword(string pwd)
		{
			string hashedPassword;
			using (var sha = SHA256.Create())
			{
				var saltPerUser = Id;
				var computedHash = sha.ComputeHash(
					Encoding.Unicode.GetBytes(saltPerUser + pwd + ConstantSalt)
					);

				hashedPassword = Convert.ToBase64String(computedHash);
			}
			return hashedPassword;
		}

		public bool ValidatePassword(SecureString maybePwd)
		{
			return HashedPassword == GetHashedPassword(maybePwd.ToString());
		}
	}
}