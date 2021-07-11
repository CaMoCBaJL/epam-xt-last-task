namespace DALInterfaces
{
    public interface IDALDependencyResolver
    {
        ICommentaryDAL CommentaryDAL { get; }

        IRecipeDAL RecipeDAL { get; }

        IUserDAL UserDAL { get; }
    }
}
