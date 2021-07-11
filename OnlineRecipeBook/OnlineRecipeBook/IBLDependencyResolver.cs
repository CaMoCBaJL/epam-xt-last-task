namespace BLInterfaces
{
    public interface IBLDependencyResolver
    {
        IUserLogic UserLogic { get; }

        IRecipeLogic RecipeLogic { get; }

        ICommentLogic CommentLogic { get; }
    }
}
