using System.Collections.Generic;
using DALInterfaces;

namespace OnlineRecipeBook
{
    public interface IRecipeLogic
    {
        IRecipeDAL _DAO { get; }


        bool CreateRecipe(int userId, string title, string ingridients, string cookingProcess);

        List<string> GetRecipeCommentaries();

        int GetRecipeAward();

        bool UpdateRecipe(int recipeId, string title, string ingridients, string cookingProcess);
    }
}
