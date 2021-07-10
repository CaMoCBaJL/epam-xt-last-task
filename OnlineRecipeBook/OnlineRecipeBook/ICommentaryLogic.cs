using DALInterfaces;

namespace BLInterfaces
{
    public interface ICommentaryLogic : ILogicLayer
    {
        string CreateCommentary(int recipeId, int userId, string text);

        string UpdateCommentary(int commentaryId, string text);
    }
}
