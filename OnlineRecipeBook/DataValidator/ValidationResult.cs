namespace DataValidator
{
    public static class ValidationResult // if its an extensions class, put Extensions as postfix. I thought this is a model.
    {
        // now every string can be tested if Validation has passed, that's bad, because confusing
        // you can use it like userName.ValidationPassed() without actual validation, and this will not seem wrong if you're not enlightened
        public static bool ValidationPassed(this string validationResult) => validationResult.StartsWith(Constants.sucResult);
    }
}
