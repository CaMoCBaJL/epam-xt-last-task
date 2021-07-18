using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DALInterfaces;
using Entities;
using NLog;

namespace SqlDAL
{
    public class CommentDAL : ICommentDAL
    {
        public bool CreateComment(int recipeId, int userId, string text)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddCommentary", connection);

                command.Parameters.AddWithValue("@RecipeId", recipeId);

                command.Parameters.AddWithValue("@Text", text);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();

                    LogManager.GetCurrentClassLogger().Info($"User (ID:{userId}) created a comment in recipe (ID:{recipeId}).");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"User (ID:{userId}) got this error while he tried to add a recipe(ID:{recipeId}).");

                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Comment> GetEntities()
        {
            List<Comment> result = new List<Comment>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetComments", connection);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(
                             new Comment(
                                 id: (int)reader["ID"],
                                 text: reader["Text"] as string,
                                 likesNum: GetCommentLikesCounter((int)reader["ID"]),
                                 dislikesNum: GetCommentDislikesCounter(
                                     (int)reader["ID"])));
                    }

                    LogManager.GetCurrentClassLogger().Info($"All DB comments have been received.");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive the all comments from DB.");

                    return new List<Comment>();
                }
            }

            return result;
        }

        public int GetCommentDislikesCounter(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetCommentDislikes", connection);

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        LogManager.GetCurrentClassLogger().Info($"Comment's (ID:{commentId}) dislikeCounter has been received.");

                        return (int)reader["DislikesCounter"];
                    }

                    LogManager.GetCurrentClassLogger().Error($"Comment's (ID:{commentId}) dislikeCounter has not been received. Find out what's wrong.");

                    return -1;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to get comment's (ID:{commentId}) dislikeCounter.");

                    return -1;
                }
            }
        }

        public int GetCommentLikesCounter(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetCommentLikes", connection);

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        LogManager.GetCurrentClassLogger().Info($"Comment's (ID:{commentId}) likeCounter has been received.");

                        return (int)reader["LikesCounter"];
                    }

                    return -1;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to get comment's (ID:{commentId}) likeCounter.");

                    return -1;
                }
            }
        }

        public bool RemoveEntity(int entityId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("RemoveComment", connection);

                command.Parameters.AddWithValue("@CommentId", entityId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();

                    LogManager.GetCurrentClassLogger().Info($"Comment(ID:{entityId}) has been deleted.");

                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to delete comment (ID:{entityId}).");

                    return false;
                }
            }

            return true;
        }

        public bool UpdateComment(int commentId, string text)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ChangeComment", connection);

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.Parameters.AddWithValue("@Text", text);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    LogManager.GetCurrentClassLogger().Info($"Comment (ID:{commentId}) has been updated.");

                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to update the comment (ID:{commentId}).");

                    return false;
                }
            }
        }

        public bool LikeTheComment(int commentId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("LikeTheComment", connection);

                command.Parameters.AddWithValue("@CommentaryId", commentId);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    LogManager.GetCurrentClassLogger().Info($"Comment (ID:{commentId}) has been liked.");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to like the comment (ID:{commentId}).");

                    return false;
                }
            }
        }

        public bool DislikeTheComment(int commentId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DislikeTheComment", connection);

                command.Parameters.AddWithValue("@CommentaryId", commentId);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    LogManager.GetCurrentClassLogger().Info($"Comment (ID:{commentId}) has been disliked.");

                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to dislike the comment (ID:{commentId}).");

                    return false;
                }
            }
        }

        public int FindCommentLocation(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("FindRecipeWithUserComment", connection);

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        LogManager.GetCurrentClassLogger().Info($"Comment's (ID:{commentId}) location (RecipeId) has been received.");

                        return (int)reader["RecipeId"];
                    }

                    LogManager.GetCurrentClassLogger().Error($"Comment's (ID:{commentId}) location (RecipeId) has not been received. Find out what's wrong.");

                    return -1;
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, $"Error occured while programm tried to receive comment's (ID:{commentId}) location.");

                    return -1;
                }
            }
        }
    }
}
