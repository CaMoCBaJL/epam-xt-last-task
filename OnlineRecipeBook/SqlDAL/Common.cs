using System.Configuration;

namespace SqlDAL
{
    class Common
    {
        internal static string _connectionString = 
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}
