using Entities;
using System.Collections.Generic;

namespace DALInterfaces
{
    public interface ICommentaryDAL : IDataLayer
    {
        bool CreateCommentary(int recipeId, int userId, string text);

        bool UpdateCommentary(int commentaryId, string text);

        IEnumerable<Commentary> GetEntities();

        int GetRecipeLikesCounter(int recipeId);

        int GetRecipeDislikesCounter(int recipeId);
    }
}
