using Entities;
using System.Collections.Generic;

namespace DALInterfaces
{
    public interface ICommentaryDAL : IDataLayer
    {
        bool CreateCommentary(int recipeId, int userId, string text);

        bool UpdateCommentary(int commentId, string text);

        IEnumerable<Commentary> GetEntities();

        int GetCommentLikesCounter(int commentId);

        int GetCommentDislikesCounter(int commentId);

        bool LikeTheComment(int commentId, int userId);

        bool DislikeTheComment(int commentId, int userId);
    }
}
