﻿using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DALInterfaces
{
    public interface IRecipeDAL : IDataLayer
    {
        bool CreateRecipe(int userId, string title, string ingridients, string cookingProcess);

        IEnumerable<Commentary> GetRecipeCommentaries(int recipeId);

        int GetRecipeAward(int recipeId);

        bool UpdateRecipe(int recipeId, string title, string ingridients, string cookingProcess);

        IEnumerable<Recipe> GetRecipes();
        int GetRecipeId(string recipeTitle);
    }
}
