using System.Collections.Generic;
using Entities;

namespace DALInterfaces
{
    public interface IUserDAL : IDataLayer
    {
        int GetUserId(string login);

        IEnumerable<User> GetUsers();

        IEnumerable<string> GetUserInfo(int userId);

        bool CreateUser(string userName, int age, string login, string password);

        IEnumerable<Comment> GetUserCommentaries(int userId);

        IEnumerable<Recipe> GetUserRecipes(int userId);

        int GetCommentAuthor(int commentId);

        bool UpdateUser(int userId, string login, string password, string userName, int age);
    }
}
