namespace BLInterfaces
{
    public interface IBLDependencyResolver
    {
        IUserLogic UserLogic { get; }

        IRecipeLogic RecipeLogic { get; }

        ICommentaryLogic CommentaryLogic { get; }
    }
}
