using System.Collections.Generic;

namespace BLInterfaces
{
    public interface IUserLogic : ILogicLayer
    {
        List<string> GetUserInfo(int userId);

        string CreateUser(string userName, int age, string login, string password);

        List<string> GetUserCommentaries(int userId);

        List<string> GetUserRecipes(int userId);

        string UpdateUser(int userId, string userName, int age, string login, string password);

        string GetCommentAuthorName(int commentId);

        int GetCommentAuthor(int commentId);

        int GetUserId(string login);
    }
}
