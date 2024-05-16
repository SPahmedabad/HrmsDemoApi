using Entity;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Reports_BL
    {
        //Author --> Jignesh && chirag
        //method show Employee Documents Uploaded by Admin
        public DataTable ShowEmpDoc(Document obj)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_emp_doc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "show");
                    cmd.Parameters.AddWithValue("@emp_id", obj.emp_id);
                    cmd.Parameters.AddWithValue("@doc_type_id", "");
                    cmd.Parameters.AddWithValue("@filepath","");
                    cmd.Parameters.AddWithValue("@uploadedtime","");
                    cmd.Parameters.AddWithValue("@From_Date", obj.From_Date);
                    cmd.Parameters.AddWithValue("@To_Date", obj.To_Date);
                    SqlDataAdapter data = new SqlDataAdapter(cmd);
                    data.Fill(dt);        
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Reports_BL/GetEmpContactData() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //Author --> Hemangini && chirag
        //method to get employee data in datatable format 
        public DataTable EmpInfo()
        {
            DataTable dt_contact = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_empContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "emp");
                    conn.Open();
                    SqlDataAdapter data = new SqlDataAdapter(cmd);
                    data.Fill(dt_contact);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Reports_BL/GetEmpContactData() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt_contact;
        }

        //Author --> Hemangini && chirag
        public DataTable EmpContactDetails(UserDetails obj)
        {
            DataTable dt_contact = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_empContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", obj.id);
                    cmd.Parameters.AddWithValue("@flag", "empcontact");
                    conn.Open();
                    SqlDataAdapter data = new SqlDataAdapter(cmd);
                    data.Fill(dt_contact);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Reports_BL/GetEmpContactData() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt_contact;
        }

        //Author --> Hemangini
        //method to get employee on-leave data in datatable format
        public DataTable GetEmpLeaveData()
        {
            DataTable dt_onleave = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_get_emp_leave", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag","approved");
                    conn.Open();
                    SqlDataAdapter data = new SqlDataAdapter(cmd);
                    data.Fill(dt_onleave);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Reports_BL/GetEmpLeaveData() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace); 
            }
            return dt_onleave;
        }

        /// Author -> Saddam Shaikh
        /// Upload new daily sheet and details
        public static int addDailyTaskSheet(Daily_Task_Entity entity)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("spDailyTaskSheet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@emp_id", entity.emp_id);
                        cmd.Parameters.AddWithValue("@emp_name", entity.emp_name);
                        cmd.Parameters.AddWithValue("@file_path", entity.file_path);
                        cmd.Parameters.AddWithValue("@flag", "CRUD");

                        conn.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL -> addDailyTaskSheet() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return result;
        }

        /// Author -> Saddam Shaikh
        /// Get Employee details for binding DDL
        public static DataTable getEmpDetails()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("spDailyTaskSheet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", "all");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL -> getEmpDetails() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return dt;
        }

        /// Author -> Saddam Shaikh
        /// Get Daily task sheet file and details about it.
        public static DataTable getDailyTaskSheet(Daily_Task_Entity entity)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("spDailyTaskSheet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@emp_id", entity.emp_id);
                        cmd.Parameters.AddWithValue("@flag", "GETALL");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL -> addDailyTaskSheet() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return dt;
        }

        /// Author --> Satyam
        /// Get Employee details for binding DDL
        public static DataTable get_EmpTasksheet_Date(Daily_Task_Entity en)
        {
            DataTable dt = new DataTable();
            DateTime DOB = DateTime.ParseExact(en.Selected_Date, "yyyy/mm/dd", null);
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("spDailyTaskSheet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@emp_id", en.emp_id);
                        cmd.Parameters.AddWithValue("@date", en.Selected_Date);
                        cmd.Parameters.AddWithValue("@flag", "DATESHEET");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL -> getEmpDetails() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return dt;
        }

        // Author -> Yaksh Maishery
        // Fetch the neccessary Data from the Attend Table
        public static DataTable getAttends(attend_entity EN)
        {
            DataTable DT = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Attend_Report", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", EN.flag);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(DT);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> attend_bl, method -> getAttends\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return DT;
        }
        
        
        // Author -> chirag dhruv
        // bind document type
        public DataTable binddoctype()
        {
            DataTable dt_doc = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_emp_doc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "binddoc");
                    cmd.Parameters.AddWithValue("@doc_type_id", 0);
                    cmd.Parameters.AddWithValue("@filepath", "");
                    cmd.Parameters.AddWithValue("@emp_id", 0);
                    cmd.Parameters.AddWithValue("@uploadedtime", "");
                    SqlDataAdapter data = new SqlDataAdapter(cmd);
                    data.Fill(dt_doc);
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Reports_BL/GetEmpContactData() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt_doc;
        }
    }
}
