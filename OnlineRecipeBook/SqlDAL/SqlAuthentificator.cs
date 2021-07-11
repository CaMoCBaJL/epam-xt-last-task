using CommonInterfaces;
using DALInterfaces;
using System.Data.SqlClient;
using CommonLogic;
using System.Data;
using System;

namespace SqlDAL
{
    class SQLAuthentificator : IAuthentificator
    {
        IUserDAL _DAO;

        public SQLAuthentificator(IUserDAL dataLayer)
        => _DAO = dataLayer;



        public bool CheckUserIdentity(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("CheckIdentity", connection);

                command.Parameters.AddWithValue("@UserId", _DAO.GetUserId(userName));
                command.Parameters.AddWithValue("@PasswordHashSum",
                    new PasswordHasher().HashThePassword(password));

                SqlParameter returnValue = new SqlParameter("@value", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add(returnValue);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.ExecuteReader();

                    if ((int)returnValue.Value == 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

    }
}
