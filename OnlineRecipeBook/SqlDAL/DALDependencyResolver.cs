using CommonInterfaces;
using DALInterfaces;

namespace SqlDAL
{
    public class DALDependencyResolver : IDALDependencyResolver
    {
        public ICommentDAL CommentDAL => new CommentDAL();

        public IRecipeDAL RecipeDAL => new RecipeDAL();

        public IUserDAL UserDAL => new UserDAL();

        public IAuthentificator Authentificator => new SQLAuthentificator(UserDAL);
    }
}
