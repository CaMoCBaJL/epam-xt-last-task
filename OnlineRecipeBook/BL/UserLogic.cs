using System.Collections.Generic;
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

        public List<string> GetEntities()
        {
            List<string> result = new List<string>();

            foreach (var item in _DAO.GetEntities())
            {
                result.Add(item.ToString());
            }

            return result;
        }

        public List<string> GetUserCommentaries()
        {
            List<string> result = new List<string>();

            foreach (var item in _DAO.GetUserCommentaries())
            {
                result.Add(item.ToString());
            }

            return result;
        }

        public List<string> GetUserRecipes()
        {
            List<string> result = new List<string>();

            foreach (var item in _DAO.GetUserRecipes())
            {
                result.Add(item.ToString());
            }

            return result;
        }

        public bool RemoveEntity(int entityId)
                => _DAO.RemoveEntity(entityId);

        public string UpdateUserIdentity(int userId, string login, string password)
        {
            var validator = new UserValidator();

            if (validator.ValidateUserIdentity(login, password).StartsWith(Constants.sucResult))
            {
                if (_DAO.UpdateUserIdentity(userId, login, password))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateUserIdentity(login, password);
        }

        public string UpdateUserInfo(int userId, string userName, int age)
        {
            var validator = new UserValidator();

            if (validator.ValidateUserInfo(userName, age).StartsWith(Constants.sucResult))
            {
                if (_DAO.UpdateUserInfo(userId, userName, age))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateUserInfo(userName, age);
        }
    }
}
