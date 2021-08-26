using System.Security.Cryptography;
using System.Text;

namespace CommonLogic
{
    public class PasswordHasher
    {
        public string HashThePassword(string password)
        {
            if (password == null)
                return null;

            HashAlgorithm sha = SHA256.Create(); // nice, that IS a good way of hashing passwords

            StringBuilder result = new StringBuilder();

            foreach (var hashValue in sha.ComputeHash(Encoding.UTF8.GetBytes(password)))
            {
                result.Append(hashValue + " ");
            }

            return result.ToString();
        }
    }
}