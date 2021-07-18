using System.Collections.Generic;
using BLInterfaces;
using DALInterfaces;
using DataValidator;

namespace BL
{
    class RecipeLogic : IRecipeLogic
    {
        IRecipeDAL _DAO;


        public RecipeLogic(IRecipeDAL recipeDAL) => _DAO = recipeDAL;

        public string CreateRecipe(int userId, string title, string ingridients, string cookingProcess)
        {
            var validator = new RecipeValidator();

            if (validator.ValidateData(title, cookingProcess, ingridients).ValidationPassed())
            {
                if (_DAO.CreateRecipe(userId, title, ingridients, cookingProcess))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateData(title, cookingProcess, ingridients);
        }

        public List<string> GetEntities()
        {
            List<string> result = new List<string>();

            foreach (var recipe in _DAO.GetRecipes())
            {
                result.Add(recipe.ToString());
            }

            return result;
        }

        public string GetRecipe(int recipeId)
               => _DAO.GetRecipe(recipeId).ToString();

        public double GetRecipeAward(int recipeId)
        => _DAO.GetRecipeAward(recipeId);

        public List<string> GetRecipeCommentaries(int recipeId)
        {
            List<string> result = new List<string>();

            foreach (var commment in _DAO.GetRecipeCommentaries(recipeId))
            {
                result.Add(commment.ToString());
            }

            return result;
        }

        public int GetRecipeId(string recipeTitle)
                => _DAO.GetRecipeId(recipeTitle) + 1;

        public int GetUserAward(int userId)
                => _DAO.GetUserAward(userId);
        
        public bool RateTheRecipe(int recipeId, int userId, int recipeAward)
                => _DAO.RateTheRecipe(recipeId, userId, recipeAward);

        public bool RemoveEntity(int entityId)
                => _DAO.RemoveEntity(entityId);

        public string UpdateRecipe(int recipeId, string title, string ingridients, string cookingProcess)
        {
            var validator = new RecipeValidator();

            if (validator.ValidateData(title, cookingProcess, ingridients).ValidationPassed())
            {
                if (_DAO.UpdateRecipe(recipeId, title, ingridients, cookingProcess))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateData(title, cookingProcess, ingridients);
        }
    }
}
