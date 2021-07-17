namespace WebPL.Models
{
    public static class RegistrationData
    {
        public static string Login { get; set; }

        public static string Age { get; set; }

        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string DataValidationResult { get; set; }


        public static void ResetFields()
        {
            Password = null;

            Login = null;

            Age = null;

            UserName = null;

            DataValidationResult = null;
        }
    }
}