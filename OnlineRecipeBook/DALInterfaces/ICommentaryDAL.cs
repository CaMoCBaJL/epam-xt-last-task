using Entities;
using System.Collections.Generic;

namespace DALInterfaces
{
    public interface ICommentaryDAL
    {
        bool CreateCommentary(int recipeId, int userId, string text);

        bool UpdateRecipe(int commentaryId, string text);

        IEnumerable<Commentary> GetEntities();
    }
}
