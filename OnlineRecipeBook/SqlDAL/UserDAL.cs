using System;
using System.Collections.Generic;
using DALInterfaces;
using Entities;

namespace SqlDAL
{
    public class UserDAL : IUserDAL
    {
        public void CreateAdmin()
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(string userName, int age, string login, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetEntities()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Commentary> GetUserCommentaries()
        {
            throw new NotImplementedException();
        }

        public int GetUserId(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> GetUserRecipes()
        {
            throw new NotImplementedException();
        }

        public bool RemoveEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserIdentity(int userId, string login, string password)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserInfo(int userId, string userName, int age)
        {
            throw new NotImplementedException();
        }
    }
}
