using System.Collections.Generic;
using DALInterfaces;

namespace BLInterfaces
{
    public interface IUserLogic : ILogicLayer
    {
        string CreateUser(string userName, int age, string login, string password);

        List<string> GetUserCommentaries();

        List<string> GetUserRecipes();

        string UpdateUserInfo(int userId, string userName, int age);

        string UpdateUserIdentity(int userId, string login, string password);
    }
}
