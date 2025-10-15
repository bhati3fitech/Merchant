using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Connection
    {
        static string ConnectionType;

        static string Database;
        static string Server;
        static string User;
        static string Password;
        static string Connect_Timeout;
        static string Provider_Name;



        public static string GetExcelConnectionString(string source)
        {
            string Connection_Str = string.Empty;
            Connection_Str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + ";";
            Connection_Str = Connection_Str + "Extended Properties=Excel 12.0;Persist Security Info=False";


            return Connection_Str;
        }

        static Connection()
        {
            if (ConnectionType == "SQL")
            {
                Database = "PMS";
                Server = "192.168.1.202";
                User = "rms";
                Password = "rms123";
                Connect_Timeout = "6000";
                Provider_Name = "System.Data.SqlClient";
            }
            else if (ConnectionType == "ORACLE")
            {

            }
        }



        public static string GetConnectionString()
        {
            string Connection_Str = string.Empty;
            Connection_Str = Connection_Str + "Database=" + Database + ";";
            Connection_Str = Connection_Str + "Server=" + Server + ";";
            Connection_Str = Connection_Str + "User=" + User + ";";
            Connection_Str = Connection_Str + "password=" + Password + ";";
            Connection_Str = Connection_Str + "Connect Timeout=" + Connect_Timeout + ";";

            return Connection_Str;
        }

    }
}
