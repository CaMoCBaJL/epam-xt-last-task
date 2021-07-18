using System;
using System.Collections.Generic;
using DALInterfaces;
using Entities;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using CommonLogic;
using NLog;

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

                    LogManager.GetCurrentClassLogger().Info($"All useres have been received from DB.");

                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to all users from DB.");

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

                    LogManager.GetCurrentClassLogger().Info($"New user has been created.");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to create a new user.");

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
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive full user data (ID:{userId}).");

                    return new List<string>();
                }
            }

            result.Add(GetUserIdentity(userId).ToString());

            foreach (var recipe in GetUserRecipes(userId))
            {
                result.Add(recipe.ToString());
            }

            if (GetUserRecipes(userId).Count() == 0)
                result.Add("User has 0 recipes.");

            foreach (var comment in GetUserCommentaries(userId))
            {
                result.Add(comment.ToString());
            }

            if (GetUserCommentaries(userId).Count() == 0)
                result.Add("User has 0 comments.");

            LogManager.GetCurrentClassLogger().Info($"Full user's (ID:{userId}) data has been received.");

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

                    reader.Read();

                    LogManager.GetCurrentClassLogger().Info($"User's (ID:{userId}) identity has been received.");

                    return new UserIdentity(id: userId,
                                            login: reader["UserLogin"] as string,
                                            hashedPassword: reader["UserPassword"] as string);


                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive user's (ID:{userId}) identity.");

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

                        LogManager.GetCurrentClassLogger().Info($"All user's (ID:{userId}) comments have been received.");

                    }
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive all user's (ID:{userId}) comments.");

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

                    LogManager.GetCurrentClassLogger().Info($"User's (ID:{userId}) recipes has been received.");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive all user's (ID:{userId}) recipes.");

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

                    LogManager.GetCurrentClassLogger().Info($"User (ID:{entityId}) has been deleted.");

                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to delete user (ID:{entityId}).");

                    return false;
                }
            }

            return true;
        }

        bool UpdateUserIdentity(int userId, string login, string password)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ChangeUserIdentity", connection);

                string passwordHashSum = new PasswordHasher().HashThePassword(password);

                if (passwordHashSum == null)
                    command.Parameters.AddWithValue("@Password", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Password", passwordHashSum);

                command.Parameters.AddWithValue("@UserId", userId);

                command.Parameters.AddWithValue("@Login", login);


                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();

                    LogManager.GetCurrentClassLogger().Info($"User's (ID:{userId}) identity has been changed.");

                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to change user's (ID:{userId}) identity.");

                    return false;
                }
            }

            return true;
        }

        bool UpdateUserInfo(int userId, string userName, int age)
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

                    LogManager.GetCurrentClassLogger().Info($"User's (ID:{userId}) additional data has been received.");

                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to recieve user's (ID:{userId}) additional data.");

                    return false;
                }
            }

            return true;
        }

        public int GetUserId(string login)
        {
            int indx = GetUserIdentities().ToList().FindIndex(user => user.Login == login);

            if (indx > -1)
            {
                LogManager.GetCurrentClassLogger().Info($"User's (Login:{login}) ID has been received.");

                return GetUserIdentities().ElementAt(indx).Id;
            }

            LogManager.GetCurrentClassLogger().Error($"User's (Login:{login}) ID has not been received. Find out what's wrong.");

            return indx;
        }

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


                    LogManager.GetCurrentClassLogger().Info($"All user identities have been received.");

                    return result;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive all user identities.");

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
                    {
                        LogManager.GetCurrentClassLogger().Info($"Comment's(ID:{commentId}) author has been received.");

                        return (int)reader["UserId"];
                    }

                        LogManager.GetCurrentClassLogger().Error($"Comment's(ID:{commentId}) author has not been received.");
                    return 0;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Info($"Error occured while programm tried to receive comment's(ID:{commentId}) author.");

                    return 0;
                }
            }
        }

        public bool UpdateUser(int userId, string login, string password, string userName, int age)
        {
            if (UpdateUserIdentity(userId, login, password))
                return UpdateUserInfo(userId, userName, age);

            return false;
        }
    }
}

