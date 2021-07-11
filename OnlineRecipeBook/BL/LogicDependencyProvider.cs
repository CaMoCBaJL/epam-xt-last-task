using BLInterfaces;
using DALInterfaces;

namespace BL
{
    public class LogicDependencyProvider : IBLDependencyResolver
    {
        IDALDependencyResolver _DAO;


        public LogicDependencyProvider(IDALDependencyResolver dependencyResolver) => _DAO = dependencyResolver;

        public IUserLogic UserLogic => new UserLogic(_DAO.UserDAL);

        public IRecipeLogic RecipeLogic => new RecipeLogic(_DAO.RecipeDAL);

        public ICommentLogic CommentLogic => new CommentLogic(_DAO.CommentDAL);
    }
}
