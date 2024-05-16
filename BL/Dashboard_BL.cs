using Entity;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dashboard_BL
    {
        SqlConnection conn = new SqlConnection(Sql_Connection.connString);
        //Author : Jay Patel
        //This method return new Employee join data with 7 days condition
        //if datatable is null then no employee join in company in this month and day.
        //if datatable return value then employee join in company
        public static DataTable newempjoindata()
        {   
            DataTable ds_dep = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("new_emp_joindata", conn); //new_emp_joindata
                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    dt_adp.Fill(ds_dep);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Dashboard : newempjoindata : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds_dep;
        }

        //Author- Deeksha
        public DataTable Showevents(CalEvent_entity en)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("show_event_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", en.flag);
                if (en.flag != "default")
                {
                    cmd.Parameters.AddWithValue("@event_date", en.event_date);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Dashboard : Showevents() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return dt;
        }

        /// Author -> Saddam Shaikh
        /// Get Emp of month details
        public static DataTable getEmpOfMonth(Emp_Of_Month_Entity entity)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("spEmpOfMonth", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        cmd.Parameters.AddWithValue("@flag", entity.flag);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Dashboard_BL -> getEmpOfMonth() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

            return dt;
        }


        /// Author -> Jignesh Panchal
        /// ShowEmpVacancyData():- If Employee Login then they can show Active vacancy data
        public DataTable ShowEmpVacancyData(VacancyEntity vac_obj)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_vaccancy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", vac_obj.flag);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived Dashboard_BL In ShowEmpVacancyData(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //Author --> Ravi Vaghela
        //get event_gallery data from database 
        public DataTable get_event_gallery(Event_Gallery_Entity obj)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(Sql_Connection.connString);
                using (SqlCommand cmd = new SqlCommand("sp_event_gallery", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", obj.flag);
                    if (obj.id != 0)
                    {
                        cmd.Parameters.AddWithValue("@id", obj.id);
                    }
                    /*using sql adapter fill data into datatable*/
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (dt = new DataTable())
                        {
                            sda.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception exe)
            {
                InsertLog.WriteErrorLog("Error in Dashboard/get_event_gallery(): Message:" + exe.Message + "stacktrace:" + exe.StackTrace);
            }
            return dt;
        }
        
    }
}
