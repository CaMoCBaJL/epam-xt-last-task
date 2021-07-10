using System.Collections.Generic;
using DALInterfaces;

namespace BLInterfaces
{
    public interface IRecipeLogic : ILogicLayer
    {
        string CreateRecipe(int userId, string title, string ingridients, string cookingProcess);

        List<string> GetRecipeCommentaries(int recipeId);

        int GetRecipeAward(int recipeId);

        string UpdateRecipe(int recipeId, string title, string ingridients, string cookingProcess);
    }
}
