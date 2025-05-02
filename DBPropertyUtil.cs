using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;

namespace CourierManagementUtilLibrary
{
    public class DBPropertyUtil
    {
        public static SqlConnection AppConnection()
        {
            string cnstring = GetConnectionString();
            SqlConnection cn = new SqlConnection(cnstring);
            return cn;

        }

        public static string GetConnectionString()
        {
            return "server=DESKTOP-0N7CP4U\\SQLEXPRESS;database=couriermanagement;integrated security=true;trust server certificate=true";


        }

    }
}