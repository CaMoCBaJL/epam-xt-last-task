using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidator
{
    public static class Constants
    {
        public const string emptyPasswordConstant = "Password is empty";

        public const string addCapitalLetterToPasswordMessage = "There are no capital letters in password. Add some to it.";

        public const string addPunctuantionToPasswordMessage = "There are no punctuation marks in password. Add some to it.";

        public const string addNumberToPasswordMessage = "There are no numbers in password. Add some to it.";

        public const string passwordIsShort = "Password is too short. Minimum passwords symbols amount - 8.";

        public const string stringIsEmpty = " content is empty. Please fix it.";

        public const string sucResult = "All";

        public const string serverError = "Something gone wrong... Please try again later.";

        public const string paramterTooLong = " is too long. Please fix it(< 100 symbols)";

        public const string wrongAge = "Input age is incorrect. Avaliable values : 0-99";

        public const string allOk = "All is Ok";

        public const string ageRegexPattern = "\\d{1,2}";
    }
}
