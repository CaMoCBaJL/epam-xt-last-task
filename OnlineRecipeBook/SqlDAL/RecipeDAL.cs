using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DALInterfaces;
using Entities;

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
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

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
                }
                catch (Exception ex)
                {
                    //todo Add logger to each try-catch block.

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
                        return Math.Round(result, 2);

                    return 0;
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

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
                }
                catch (Exception ex)
                {
                    //todo Add logger to each try-catch block.

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
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

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

                command.Parameters.AddWithValue("@RecipeTitle", title);

                command.Parameters.AddWithValue("@Ingridients", ingridients);

                command.Parameters.AddWithValue("@CookingProcess", cookingProcess);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

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

                //todo add recipe award validation to recipe logic.

                SqlCommand command = new SqlCommand("RateRecipe", connection);

                command.Parameters.AddWithValue("@RecipeId", recipeId);

                command.Parameters.AddWithValue("@UserId", userId);

                command.Parameters.AddWithValue("@Award", award);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return false;
                }
            }

            return true;
        }

        public int GetUserAward(int userId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetUserAward", connection);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                        return (int)reader["AwardValue"];

                    return 0;
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return 0;
                }
            }
        }

        public Recipe GetRecipe(int recipeId)
        {
            var result = GetRecipes().ToList().Find(recipe => recipe.Id == recipeId);

            if (result == null)
                return new Recipe();

            return result;
        }
    }
}
