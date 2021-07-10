namespace DataValidator
{
    public class RecipeValidator
    {
        public string ValidateData(string recipeTitle, string recipeText, string recipeIngridients)
        {
            if (ValidateTitle(recipeTitle).ValidationPassed())
            {
                if (ValidateRecipeIngridients(recipeIngridients).ValidationPassed())
                {
                    if (ValidateRecipeText(recipeText).ValidationPassed())
                        return Constants.allOk;
                    else
                        return ValidateRecipeText(recipeText);
                }
                else
                    return ValidateRecipeIngridients(recipeIngridients);
            }
            else
                return ValidateTitle(recipeTitle);
        }

        string ValidateTitle(string recipeTitle)
        {
            if (recipeTitle.Length < 100)
                return Constants.allOk;
            else
                return "Title" + Constants.paramterTooLong;
        }

        string ValidateRecipeText(string text)
        {
            if (string.IsNullOrEmpty(text.Trim()))
                return "Text" + Constants.stringIsEmpty;
            else
                return Constants.allOk;
        }

        public string ValidateRecipeIngridients(string ingridients)
        {
            if (string.IsNullOrEmpty(ingridients.Trim()))
                return "Ingridients" + Constants.stringIsEmpty;
            else
                return Constants.allOk;
        }
    }
}
