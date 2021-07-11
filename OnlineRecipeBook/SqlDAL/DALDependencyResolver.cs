using DALInterfaces;

namespace SqlDAL
{
    public class DALDependencyResolver : IDALDependencyResolver
    {
        public ICommentaryDAL CommentaryDAL => new CommentDAL();

        public IRecipeDAL RecipeDAL => new RecipeDAL();

        public IUserDAL UserDAL => new UserDAL();
    }
}
