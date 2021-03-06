using System.Collections.Generic;
using Entities;

namespace DALInterfaces
{
    public interface IRecipeDAL : IDataLayer
    {
        bool CreateRecipe(int userId,
            string title, string ingridients, string cookingProcess);

        bool RateTheRecipe(int recipeId, int userId, int award);

        IEnumerable<Comment> GetRecipeCommentaries(int recipeId);

        double GetRecipeAward(int recipeId);

        bool UpdateRecipe(int recipeId,
            string title, string ingridients, string cookingProcess);

        IEnumerable<Recipe> GetRecipes();

        int GetRecipeId(string recipeTitle);

        int GetUserAward(int userId, int recipeId);

        Recipe GetRecipe(int recipeId);

        int GetRecipeAuthor(int recipeId);
    }
}
