
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    //Get Connection String From Web Config
    public class Sql_Connection
    {
        public static string connString = ConfigurationManager.ConnectionStrings["FeedBackConectionstr"].ConnectionString;
        public static string GetModule = ConfigurationManager.AppSettings["GetModule"];
        public static string getEmployee = ConfigurationManager.AppSettings["GetEmployee"];
        public static string getDept = ConfigurationManager.AppSettings["GetDept"];
        public static string listviewurl = ConfigurationManager.AppSettings["iistview"];
        public static string dropdownurl = ConfigurationManager.AppSettings["dropdown"];
        public static string newempjoin = ConfigurationManager.AppSettings["subURL_newempjoin"];
    }
}
