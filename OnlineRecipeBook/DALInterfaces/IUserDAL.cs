using System.Collections.Generic;
using Entities;

namespace DALInterfaces
{
    public interface IUserDAL : IDataLayer
    {
        int GetUserId(string userName);

        IEnumerable<User> GetUsers();

        IEnumerable<string> GetUserInfo(int userId);

        bool CreateUser(string userName, int age, string login, string password);

        IEnumerable<Comment> GetUserCommentaries(int userId);

        IEnumerable<Recipe> GetUserRecipes(int userId);

        bool UpdateUserInfo(int userId, string userName, int age);

        bool UpdateUserIdentity(int userId, string login, string password);
    }
}
