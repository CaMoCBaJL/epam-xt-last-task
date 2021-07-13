using System.Collections.Generic;

namespace BLInterfaces
{
    public interface IRecipeLogic : ILogicLayer
    {
        bool RateTheRecipe(int recipeId, int userId, double recipeAward);

        string CreateRecipe(int userId, string title, string ingridients, string cookingProcess);

        List<string> GetRecipeCommentaries(int recipeId);

        double GetRecipeAward(int recipeId);

        string UpdateRecipe(int recipeId, string title, string ingridients, string cookingProcess);

        int GetUserAward(int userId);
    }
}
