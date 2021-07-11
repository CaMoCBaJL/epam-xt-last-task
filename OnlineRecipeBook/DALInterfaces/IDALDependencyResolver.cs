using CommonInterfaces;

namespace DALInterfaces
{
    public interface IDALDependencyResolver
    {
        ICommentDAL CommentDAL { get; }

        IRecipeDAL RecipeDAL { get; }

        IUserDAL UserDAL { get; }

        IAuthentificator Authentificator { get; }
    }
}
