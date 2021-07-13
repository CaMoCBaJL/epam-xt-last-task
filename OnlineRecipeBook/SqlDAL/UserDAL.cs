using System;
using System.Collections.Generic;
using DALInterfaces;
using Entities;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using CommonLogic;

namespace SqlDAL
{
    public class UserDAL : IUserDAL
    {
        public UserDAL()
        {
            if (!AdminExists)
                CreateAdmin();
        }

        bool AdminExists { get => GetUsers().ToList().FindIndex(user => user.UserName == "Administrator") != -1; }

        public IEnumerable<User> GetUsers()
        {
            List<User> result = new List<User>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetUsers", connection);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(
                             new User(
                                 id: (int)reader["ID"],
                                 userName: reader["UserName"] as string,
                                 age: (int)reader["Age"]));
                    }
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return new List<User>();
                }
            }

            return result;
        }

        public bool CreateAdmin()
        => CreateUser("Administrator", 0, "admin", "admin");


        public bool CreateUser(string userName, int age, string login, string password)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddUser", connection);

                command.Parameters.AddWithValue("@UserName", userName);

                command.Parameters.AddWithValue("@UserAge", age);

                command.Parameters.AddWithValue("@Login", login);

                command.Parameters.AddWithValue("@PasswordHashSum",
                    new PasswordHasher().HashThePassword(password));

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //todo Add logger to each try-catch block.

                    return false;
                }
            }

            return true;
        }

        public IEnumerable<string> GetUserInfo(int userId)
        {
            List<string> result = new List<string>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetAppUser", connection);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new User(id: userId,
                            userName: reader["UserName"] as string,
                            age: (int)reader["Age"]).ToString());
                    }
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return new List<string>();
                }
            }

            result.Add(GetUserIdentity(userId).ToString());

            foreach (var recipe in GetUserRecipes(userId))
            {
                result.Add(recipe.ToString());
            }

            foreach (var comment in GetUserCommentaries(userId))
            {
                result.Add(comment.ToString());
            }

            return result;

        }

        UserIdentity GetUserIdentity(int userId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetUserIdentity", connection);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    return new UserIdentity(id: userId,
                                            login: reader["UserLogin"] as string,
                                            hashedPassword: reader["UserPassword"] as string);
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return new UserIdentity();
                }
            }
        }

        public IEnumerable<Comment> GetUserCommentaries(int userId)
        {
            List<Comment> result = new List<Comment>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetUserComments", connection);

                command.Parameters.AddWithValue("@UserId", userId);

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
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return new List<Comment>();
                }
            }

            return result;
        }

        public IEnumerable<Recipe> GetUserRecipes(int userId)
        {
            List<Recipe> result = new List<Recipe>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetUserRecipes", connection);

                command.Parameters.AddWithValue("@UserId", userId);

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
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return new List<Recipe>();
                }
            }

            return result;
        }

        public bool RemoveEntity(int entityId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("RemoveUser", connection);

                command.Parameters.AddWithValue("@UserId", entityId);

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

        public bool UpdateUserIdentity(int userId, string login, string password)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ChangeUserIdentity", connection);

                command.Parameters.AddWithValue("@UserId", userId);

                command.Parameters.AddWithValue("@Login", login);

                command.Parameters.AddWithValue("@PasswordHashSum",
                    new PasswordHasher().HashThePassword(password));

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

        public bool UpdateUserInfo(int userId, string userName, int age)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ChangeAppUser", connection);

                command.Parameters.AddWithValue("@UserId", userId);

                command.Parameters.AddWithValue("@UserName", userName);

                command.Parameters.AddWithValue("@UserAge", age);

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

        public int GetUserId(string login)
                => GetUserIdentities().ToList().FindIndex(user => user.Login == login);

        List<UserIdentity> GetUserIdentities()
        {
            List<UserIdentity> result = new List<UserIdentity>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetUserIdentities", connection);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new UserIdentity((int)reader["UserId"],
                                                    reader["UserLogin"] as string,
                                                    reader["UserPassword"] as string));
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    //todo Add logger to each try-catch block.

                    return new List<UserIdentity>();
                }
            }
        }

        public int GetCommentAuthor(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetCommentAuthor", connection);

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                        return (int)reader["UserId"];

                    return 0;
                }
                catch (Exception ex)
                {
                    //todo Add logger to each try-catch block.

                    return 0;
                }
            }
        }
    }
}

