using Entities;
using System.Collections.Generic;

namespace DALInterfaces
{
    public interface ICommentDAL : IDataLayer
    {
        bool CreateComment(int recipeId, int userId, string text);

        bool UpdateComment(int commentId, string text);

        IEnumerable<Comment> GetEntities();

        int GetCommentLikesCounter(int commentId);

        int GetCommentDislikesCounter(int commentId);

        bool LikeTheComment(int commentId, int userId);

        bool DislikeTheComment(int commentId, int userId);

        int FindCommentLocation(int commentId);
    }
}
