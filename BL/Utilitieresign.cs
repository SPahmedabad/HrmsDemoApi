using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Utilitieresign
    {
        public static int resing_emp(Resignationentity obj)
        {
            var str = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand mainstdata_cmd = new SqlCommand("resign_emp", con);
                    mainstdata_cmd.CommandType = CommandType.StoredProcedure;
                    mainstdata_cmd.Parameters.AddWithValue("@dateofresing", obj.date_of_resign);
                    mainstdata_cmd.Parameters.AddWithValue("@lastdatework", obj.last_day_work);
                    mainstdata_cmd.Parameters.AddWithValue("@reason_leave", obj.reason_of_leave);
                    mainstdata_cmd.Parameters.AddWithValue("@employee_code", obj.Employee_code);
                    con.Open();
                    str = mainstdata_cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Utilitieresign : resing_emp : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return str;
        }
    }
}
