using System.Collections.Generic;
using DALInterfaces;

namespace BLInterfaces
{
    public interface IRecipeLogic
    {
        string CreateRecipe(int userId, string title, string ingridients, string cookingProcess);

        List<string> GetRecipeCommentaries();

        int GetRecipeAward();

        string UpdateRecipe(int recipeId, string title, string ingridients, string cookingProcess);
    }
}
