using System.Collections.Generic;
using DALInterfaces;

namespace OnlineRecipeBook
{
    public interface IUserLogic : ILogicLayer
    {
        IUserDAL _DAO { get; }


        bool CreateUser(string userName, int age, string login, string password);

        List<string> GetUserCommentaries();

        List<string> GetUserRecipes();

        bool UpdateUserInfo(int userId, string userName, int age);

        bool UpdateUserIdentity(int userId, string login, string password);
    }
}
