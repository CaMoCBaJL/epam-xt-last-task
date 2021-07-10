using DALInterfaces;

namespace OnlineRecipeBook
{
    public interface ICommentaryLogic : ILogicLayer
    {
        ICommentaryDAL _DAO { get; }


        bool CreateCommentary(int recipeId, int userId, string text);

        bool UpdateRecipe(int commentaryId, string text);
    }
}
