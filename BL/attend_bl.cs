using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Entity;
using Library;

namespace BL
{
    public class attend_bl
    {
        // Author -> Yaksh Maishery
        // Fetch the neccessary Data from the Attend Table
        public static DataTable getAttends(attend_entity EN)
        {
            DataTable DT = new DataTable();
            try
            {
                using(SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using(SqlCommand cmd = new SqlCommand("SP_Attend_Report", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", EN.flag);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(DT);
                    }
                }
            }catch(Exception ex)
            {
                InsertLog.WriteErrorLog("class -> attend_bl, method -> getAttends\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return DT;
        }
    }
}
