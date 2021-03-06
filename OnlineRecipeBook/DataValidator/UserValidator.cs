using System.Text.RegularExpressions;

namespace DataValidator
{
    public class UserValidator
    {
        public string ValidateData(string userName, string login, string password, int age)
        {
            if (ValidateUserInfo(userName, age).ValidationPassed())
            {
                if (ValidateUserIdentity(login, password).ValidationPassed())
                {
                    return Constants.allOk;
                }
                else
                    return ValidateUserIdentity(login, password);
            }
            else
                return ValidateUserInfo(userName, age);
        }

        public string ValidateUserInfo(string userName, int age)
        {
            if (userName.Length > 255)
                return "UserName" + Constants.paramterTooLong;

            if (!ValidateParameter(age.ToString(), Constants.ageRegexPattern))
                return Constants.wrongAge;

            return Constants.allOk;
        }

        public string ValidateUserIdentity(string login, string password)
        {
            if (!new PasswordValidator().ValidatePassword(password).ValidationPassed())
                return new PasswordValidator().ValidatePassword(password);

            if (login.Length > 100)
                return "Login" + Constants.paramterTooLong;

            if (login.Length < 8)
                return "Login" + Constants.stringIsShort;

            return Constants.allOk;
        }

        public static bool ValidateParameter(string parameter, string regexExpression) => new Regex(regexExpression).IsMatch(parameter);
    }
}
