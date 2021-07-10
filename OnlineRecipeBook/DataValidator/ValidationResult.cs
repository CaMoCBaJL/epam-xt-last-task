namespace DataValidator
{
    public static class ValidationResult
    {
        public static bool ValidationPassed(this string validationResult) => validationResult.StartsWith(Constants.sucResult);
    }
}
