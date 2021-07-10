﻿using System.Text.RegularExpressions;

namespace DataValidator
{
    class PasswordValidator
    {
        public string ValidatePassword(string password)
        {
            var regex = new Regex(password);

            if (password.Length < 8)
                return Constants.passwordIsShort;

            if (!regex.IsMatch("\\d"))
                return Constants.addNumberToPasswordMessage;

            if (!regex.IsMatch("\\p{P}"))
                return Constants.addPunctuantionToPasswordMessage;

            if (!regex.IsMatch("A-Z"))
                return Constants.addCapitalLetterToPasswordMessage;

            if (password.Length > 100)
                return "Password" + Constants.paramterTooLong;

            return Constants.allOk;
        }
    }
}