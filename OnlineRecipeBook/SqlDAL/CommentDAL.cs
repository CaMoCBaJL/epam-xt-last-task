using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DALInterfaces;
using Entities;

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
                }
                catch (Exception ex)
                {
                    //todo Add logger to each try-catch block.

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
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

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
                        return (int)reader["DislikesCounter"];
                    }

                    return -1;
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

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
                        return (int)reader["LikesCounter"];
                    }

                    return -1;
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

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

        public bool UpdateComment(int commentId, string text)
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
                    //todo Add logger to each try-catch block.

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

                    return true;
                }
                catch (Exception ex)
                {
                    //todo Add logger to each try-catch block.

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

                    return true;
                }
                catch (Exception ex)
                {
                    //todo Add logger to each try-catch block.

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
                        return (int)reader["RecipeId"];

                    return -1;
                    
                }
                catch (Exception)
                {
                    //todo Add logger to each try-catch block.

                    return -1;
                }
            }
        }
    }
}
