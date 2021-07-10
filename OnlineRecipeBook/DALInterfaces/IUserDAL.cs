﻿using System.Collections.Generic;
using Entities;

namespace DALInterfaces
{
    public interface IUserDAL : IDataLayer
    {
        bool CreateUser(string userName, int age, string login, string password);

        IEnumerable<Commentary> GetUserCommentaries();

        IEnumerable<Recipe> GetUserRecipes();

        bool UpdateUserInfo(int userId, string userName, int age);

        bool UpdateUserIdentity(int userId, string login, string password);


    }
}
