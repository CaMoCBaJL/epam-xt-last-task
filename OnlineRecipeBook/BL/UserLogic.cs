using System.Collections.Generic;
using System.Linq;
using BLInterfaces;
using DALInterfaces;
using DataValidator;

namespace BL
{
    class UserLogic : IUserLogic
    {
        IUserDAL _DAO;


        public UserLogic(IUserDAL userDAL) => _DAO = userDAL;

        public string CreateUser(string userName, int age, string login, string password)
        {
            var validator = new UserValidator();

            if (validator.ValidateData(userName, login, password, age).ValidationPassed())
            {
                if (_DAO.CreateUser(userName, age, login, password))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateData(userName, login, password, age);
        }

        public string GetCommentAuthorName(int commentId)
        {
            if (GetCommentAuthor(commentId) > 0)
                return _DAO.GetUsers().ToList().Find(user => user.Id == _DAO.GetCommentAuthor(commentId)).UserName;

            return string.Empty;
        }

        public int GetCommentAuthor(int commentId)
               => _DAO.GetCommentAuthor(commentId);

        public List<string> GetEntities()
        {
            List<string> result = new List<string>();

            foreach (var item in _DAO.GetUsers())
            {
                result.Add(item.ToString());
            }

            return result;
        }

        public List<string> GetUserCommentaries(int userId) // this is more of a comments logic
        {
            List<string> result = new List<string>();

            foreach (var item in _DAO.GetUserCommentaries(userId))
            {
                result.Add(item.ToString());
            }

            return result;
        }

        public int GetUserId(string login)
               => _DAO.GetUserId(login);

        public List<string> GetUserInfo(int userId)
               => _DAO.GetUserInfo(userId).ToList();

        public List<string> GetUserRecipes(int userId) // and this is more of a recipe logic
        {
            List<string> result = new List<string>();

            foreach (var item in _DAO.GetUserRecipes(userId))
            {
                result.Add(item.ToString());
            }

            return result;
        }

        public bool RemoveEntity(int entityId)
                => _DAO.RemoveEntity(entityId);

        public string UpdateUser(int userId, string userName, int age, string login, string password)
        {
            string validationResult = new UserValidator().ValidateData(userName, login, password, age);

            if (validationResult.ValidationPassed())
            {
                if (password == Constants.emptyPasswordConstant)
                    password = null;

                if (_DAO.UpdateUser(userId, login, password, userName, age))
                    return Constants.allOk;

                return Constants.serverError;
            }

            return validationResult;
        }
    }
}
