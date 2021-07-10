using System.Text.RegularExpressions;
using System;

namespace DataValidator
{
    public class UserValidator
    {
        public string ValidateUserInfo(string userName, int age)
        {
            if (userName.Length > 255)
                return Constants.usernameTooLong;

            if (!ValidateParameter(age.ToString(), Constants.ageRegexPattern))
                return Constants.wrongAge;

            return Constants.allOk;
        }

        public string ValidateUserIdentity(string login, string password)
        {
            if (password.Length > 100)
                return Constants.passwordTooLong;

            if (login.Length > 100)
                return Constants.loginTooLong;

            return Constants.allOk;
        }

        public static bool ValidateParameter(string parameter, string regexExpression) => new Regex(regexExpression).IsMatch(parameter);
    }
}
