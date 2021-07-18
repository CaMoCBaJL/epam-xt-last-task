using System.Collections.Generic;

namespace BLInterfaces
{
    public interface IRecipeLogic : ILogicLayer
    {
        bool RateTheRecipe(int recipeId, int userId, int recipeAward);

        string CreateRecipe(int userId, string title, string ingridients, string cookingProcess);

        List<string> GetRecipeCommentaries(int recipeId);

        double GetRecipeAward(int recipeId);

        string UpdateRecipe(int recipeId, string title, string ingridients, string cookingProcess);

        int GetUserAward(int userId, int recipeId);

        int GetRecipeId(string recipeTitle);

        string GetRecipe(int recipeId);

        int GetRecipeAuthor(int recipeId);
    }
}
