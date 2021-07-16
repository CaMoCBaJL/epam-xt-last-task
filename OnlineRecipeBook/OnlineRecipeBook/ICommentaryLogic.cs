namespace BLInterfaces
{
    public interface ICommentLogic : ILogicLayer
    {
        string CreateComment(int recipeId, int userId, string text);

        string UpdateComment(int commentId, string text);

        bool LikeTheComment(int commentId, int userId);

        bool DislikeTheComment(int commentId, int userId);

        int FindCommentLocation(int commentId);
    }
}
