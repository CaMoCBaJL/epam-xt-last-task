using System;
using System.Collections.Generic;
using DALInterfaces;
using Entities;

namespace SqlDAL
{
    public class RecipeDAL : IRecipeDAL
    {
        public bool CreateRecipe(int userId, string title, string ingridients, string cookingProcess)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> GetEntities()
        {
            throw new NotImplementedException();
        }

        public int GetRecipeAward(int recipeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Commentary> GetRecipeCommentaries(int recipeId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecipe(int recipeId, string title, string ingridients, string cookingProcess)
        {
            throw new NotImplementedException();
        }
    }
}
