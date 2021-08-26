namespace DataValidator
{
    // Very cool that you added a class for text strings. Though it wasn't required, it made different validator reuse the same strings,
    // and also it would make localization much easier.
    // Also, you can look at resource files in C# - they are slightly better at localization purposes.
    public static class Constants // though the name should've been more clear
    {
        public const string emptyPasswordConstant = "Password is empty"; // C# constants should be in PascalCase

        public const string addCapitalLetterToPasswordMessage = "There are no capital letters in password. Add some to it.";

        public const string addPunctuantionToPasswordMessage = "There are no punctuation marks in password. Add some to it.";

        public const string addNumberToPasswordMessage = "There are no numbers in password. Add some to it.";

        public const string stringIsShort = " is too short. Minimum passwords symbols amount - 8.";

        public const string stringIsEmpty = " content is empty. Please fix it.";

        public const string sucResult = "All"; // bad naming (wrong meaning and words are shortened without reason)

        public const string serverError = "Something gone wrong... Please try again later.";

        public const string paramterTooLong = " is too long. Please fix it(< 100 symbols)";

        public const string wrongAge = "Input age is incorrect. Avaliable values : 0-99";

        public const string allOk = "All is Ok";

        public const string ageRegexPattern = "\\d{1,2}"; // That could be moved to UserValidator
    }
}
