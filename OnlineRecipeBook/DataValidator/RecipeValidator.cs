namespace DataValidator
{
    public class RecipeValidator
    {
        public string ValidateData(string recipeTitle, string recipeText, string recipeIngridients) // these should be in a model. Imagine you have 30 properties, will you maintain them in every method? (title, text, ingredients, preparations, warnings, difficultyLevel, timeToCook, category, neededTools, rating, mainImage, cookingSteps, author, tags, moderationStatus, displaySettings, locale, creationTime, areCommentsAllowed, isDraft, cuisineCountry, etc)
        {
            // This kind of nesting is evil. You could invert ifs to stay on one level (search for 1 fail, instead of all successes).
            // This way 22 checks will still fit the screen
            if (!ValidateTitle(recipeTitle).ValidationPassed())
                return ValidateTitle(recipeTitle);

            if (!ValidateRecipeIngridients(recipeIngridients).ValidationPassed())
                return ValidateRecipeIngridients(recipeIngridients);
            
            if (!ValidateRecipeText(recipeText).ValidationPassed())
                return ValidateRecipeText(recipeText);

            return Constants.allOk;
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
                return "Ingridients" + Constants.stringIsEmpty; // This can be done clearer if such constant was actually a generator (a method taking strings that it will insert in the right place)
            else
                return Constants.allOk;
        }
    }
}
