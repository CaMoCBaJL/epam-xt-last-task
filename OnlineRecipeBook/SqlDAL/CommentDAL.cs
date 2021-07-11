using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonLogic;
using DALInterfaces;
using Entities;

namespace SqlDAL
{
    public class CommentDAL : ICommentaryDAL
    {
        public bool CreateCommentary(int recipeId, int userId, string text)
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
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Commentary> GetEntities()
        {
            List<Commentary> result = new List<Commentary>();

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
                             new Commentary(
                                 id: (int)reader["ID"],
                                 text: reader["Text"] as string,
                                 likesNum: GetCommentLikesCounter((int)reader["ID"]),
                                 dislikesNum: GetCommentDislikesCounter(
                                     (int)reader["ID"])));
                    }
                }
                catch (Exception)
                {
                    return new List<Commentary>();
                }
            }

            return result;
        }

        public int GetCommentDislikesCounter(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetCommentaryDislikes", connection);

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return (int)reader["DislikesCounter"];
                    }

                    return -1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public int GetCommentLikesCounter(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetCommentaryLikes", connection);

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return (int)reader["LikesCounter"];
                    }

                    return -1;
                }
                catch (Exception)
                {
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
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return false;
                }
            }

            return true;
        }

        public bool UpdateCommentary(int commentId, string text)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ChangeComment", connection);

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.Parameters.AddWithValue("@Text", commentId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    return true;
                }
                catch (Exception)
                {
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

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    return true;
                }
                catch (Exception)
                {
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

                command.Parameters.AddWithValue("@CommentId", commentId);

                command.Parameters.AddWithValue("@UserId", userId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
