using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidator
{
    public static class Constants
    {
        public const string sucResult = "All";

        public const string serverError = "Something gone wrong... Please try again later."

        public const string passwordTooLong = "Password is too long. Please change it(< 100 symbols)";

        public const string usernameTooLong = "Username is too long. Please change it(< 255 symbols)";

        public const string loginTooLong = "Login is too long. Please change it(< 100 symbols)";

        public const string wrongAge = "Input age is incorrect. Avaliable values : 0-99";

        public const string allOk = "All is Ok";

        public const string ageRegexPattern = "\\d{1,2}";
    }
}
