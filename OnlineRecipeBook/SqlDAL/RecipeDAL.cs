using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DALInterfaces;
using Entities;
using NLog;

namespace SqlDAL
{
    public class RecipeDAL : IRecipeDAL
    {
        public bool CreateRecipe(int userId,
            string title, string ingridients, string cookingProcess)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddRecipe", connection);

                command.Parameters.AddWithValue("@RecipeTitle", title);

                command.Parameters.AddWithValue("@Ingridients", ingridients);

                command.Parameters.AddWithValue("@CookingProcess", cookingProcess);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();

                    LogManager.GetCurrentClassLogger().Info($"User (ID:{userId}) created new recipe.");

                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while user(ID:{userId}) tried to create a new comment.");

                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            List<Recipe> result = new List<Recipe>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetRecipes", connection);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Recipe(
                            id: (int)reader["Id"],
                            title: reader["Title"] as string,
                            ingridients: reader["Ingridients"] as string,
                            cookingProcess: reader["CookingProcess"] as string,
                            recipeAward: new RecipeDAL().GetRecipeAward((int)reader["Id"])));
                    }

                    LogManager.GetCurrentClassLogger().Info($"All DB recipes have been received.");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive all DB recipes.");

                    return new List<Recipe>();
                }
            }

            return result;
        }

        public double GetRecipeAward(int recipeId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetRecipeAward", connection);

                command.Parameters.AddWithValue("@RecipeId", recipeId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    reader.Read();

                    if (double.TryParse(reader["Award"].ToString(), out double result))
                    {
                        LogManager.GetCurrentClassLogger().Info($"Received recip's (ID:{recipeId}) award.");

                        return Math.Round(result, 2);
                    }

                    LogManager.GetCurrentClassLogger().Warn($"Receiving recipe's (ID:{recipeId}) award ({result}) led to worng result. Please find out what's wrong.");

                    return 0;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive recipe's (ID:{recipeId}) award.");

                    return 0;
                }
            }
        }

        public IEnumerable<Comment> GetRecipeCommentaries(int recipeId)
        {
            List<Comment> result = new List<Comment>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetRecipeComments", connection);

                command.Parameters.AddWithValue("@RecipeId", recipeId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Comment(
                            id: (int)reader["Id"],
                            text: reader["Text"] as string,
                            likesNum: new CommentDAL().GetCommentLikesCounter((int)reader["Id"]),
                            dislikesNum: new CommentDAL().GetCommentDislikesCounter((int)reader["Id"]))
                            );
                    }

                    LogManager.GetCurrentClassLogger().Info($"Recipe's (ID:{recipeId}).");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to delete comment (ID:{recipeId}).");

                    return new List<Comment>();
                }
            }

            return result;
        }

        public int GetRecipeId(string recipeTitle)
            => GetRecipes().ToList().FindIndex(recipe => recipe.Title == recipeTitle);

        public bool RemoveEntity(int entityId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("RemoveRecipe", connection);

                command.Parameters.AddWithValue("@RecipeId", entityId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();

                    LogManager.GetCurrentClassLogger().Info($"Recipe (ID:{entityId}) has been deleted.");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to delete recipe (ID:{entityId}).");

                    return false;
                }
            }

            return true;
        }

        public bool UpdateRecipe(int recipeId,
            string title, string ingridients, string cookingProcess)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ChangeRecipe", connection);

                command.Parameters.AddWithValue("@RecipeId", recipeId);

                command.Parameters.AddWithValue("@RecipeTitle", title);

                command.Parameters.AddWithValue("@Ingridients", ingridients);

                command.Parameters.AddWithValue("@CookingProcess", cookingProcess);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();

                    LogManager.GetCurrentClassLogger().Info($"Recipe (ID:{recipeId}) has been updated.");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to update the recipe (ID:{recipeId}).");

                    return false;
                }
            }

            return true;
        }

        public bool RateTheRecipe(int recipeId, int userId, int award)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("RateRecipe", connection);

                command.Parameters.AddWithValue("@RecipeId", recipeId);

                command.Parameters.AddWithValue("@UserId", userId);

                command.Parameters.AddWithValue("@Award", award);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();

                    LogManager.GetCurrentClassLogger().Info($"Recipe (ID:{recipeId}) has been awarded by user (ID:{userId}) with mark(Value:{award}).");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while user (ID:{userId}) tried to rate the recipe (ID:{recipeId}) with mark(Value:{award}).");

                    return false;
                }
            }

            return true;
        }

        public int GetUserAward(int userId, int recipeId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetUserAward", connection);

                command.Parameters.AddWithValue("@UserId", userId);

                command.Parameters.AddWithValue("@RecipeId", recipeId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        LogManager.GetCurrentClassLogger().Info($"User's (ID:{userId}) award for recipe (ID:{recipeId}) has been received.");

                        return (int)reader["AwardValue"];
                    }

                    LogManager.GetCurrentClassLogger().Error($"User's (ID:{userId}) award for recipe (ID:{recipeId}) has not been received. Find out what's wrong.");

                    return 0;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive user's (ID:{userId}) award for recipe (ID:{recipeId}).");

                    return 0;
                }
            }
        }

        public Recipe GetRecipe(int recipeId)
        {
            var result = GetRecipes().ToList().Find(recipe => recipe.Id == recipeId);

            if (result == null)
            {
                LogManager.GetCurrentClassLogger().Error($"Recipe (ID:{recipeId}) has not been received. Find out what's wrong.");

                return new Recipe();
            }

            LogManager.GetCurrentClassLogger().Info($"Recipe (ID:{recipeId}) has been received.");

            return result;
        }

        public int GetRecipeAuthor(int recipeId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetRecipeAuthor", connection);

                command.Parameters.AddWithValue("@RecipeId", recipeId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        LogManager.GetCurrentClassLogger().Info($"Recipe's (ID:{recipeId}) author has been received.");


                        return (int)reader["UserId"];
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to recieve recipe's (ID:{recipeId}) author.");

                    return 0;
                }
            }
        }
    }
}
