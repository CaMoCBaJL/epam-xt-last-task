using System.Text.RegularExpressions;

namespace DataValidator
{
    class PasswordValidator
    {
        public string ValidatePassword(string password)
        {
            if (password == Constants.emptyPasswordConstant)
                return Constants.allOk;

            if (password.Length < 8)
                return Constants.passwordIsShort;

            if (!new Regex(@"\d{1,}").IsMatch(password))
                return Constants.addNumberToPasswordMessage;

            if (!new Regex(@"[!\p{P}?.]").IsMatch(password))
                return Constants.addPunctuantionToPasswordMessage;

            if (!new Regex(@"[A-Z]").IsMatch(password))
                return Constants.addCapitalLetterToPasswordMessage;

            if (password.Length > 100)
                return "Password" + Constants.paramterTooLong;

            return Constants.allOk;
        }
    }
}
