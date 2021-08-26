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
                return "Password"+Constants.stringIsShort;

            // use static methods or one instance (https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex#static-vs-instance-methods tl;dr - regex uses cache)
            if (!new Regex(@"\d{1,}").IsMatch(password)) 
                return Constants.addNumberToPasswordMessage;

            if (!new Regex(@"[!\p{P}?.]").IsMatch(password))
                return Constants.addPunctuantionToPasswordMessage;

            if (!new Regex(@"[A-Z]").IsMatch(password))
                return Constants.addCapitalLetterToPasswordMessage;
            // Or use one regex to check all and if it doesn't match, tell user all the rules
            // ^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\p{P}!?.]).{8,100}$   (with lookaheads like these, You don't have to care about ordering these rules)

            if (password.Length > 100)
                return "Password" + Constants.paramterTooLong;

            return Constants.allOk;
        }
    }
}
