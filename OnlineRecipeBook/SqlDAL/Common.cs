using System.Configuration;

namespace SqlDAL
{
    class Common // Common is a name that means almost nothing
    {
        internal static string _connectionString = 
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}
