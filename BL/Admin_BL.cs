using entity;
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
    public class Admin_BL
    {
        SqlConnection conn = new SqlConnection(Sql_Connection.connString);
        //Author -- Dinesh
        /// <summary>
        /// this method do opration on important notice table base on flag
        /// </summary>
        /// <param name="obj_in"></param>
        /// <returns></returns>
        public DataTable sp_imp_notice(ImportantNoticeEntity obj_in)
        {
            
            DataTable dt = new DataTable();
            try
            {
                int i = 0;
                SqlCommand comm = new SqlCommand("sp_imp_notice", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@flag", obj_in.flag); //'insert' or 'update' or 'delete' or 'getall' or 'lastnotice'

                if (obj_in.flag == "insert")
                {
                    comm.Parameters.AddWithValue("@notice", obj_in.notice);
                    comm.Parameters.AddWithValue("@start_date", obj_in.start_date);
                    comm.Parameters.AddWithValue("@end_date", obj_in.end_date);
                    conn.Open();
                    i = comm.ExecuteNonQuery();
                    conn.Close();

                    //for api user i add custmize status in datatable 
                    if (i > 0)
                    {
                        dt.Columns.Add("status");
                        dt.Rows.Add("Inserted Successfully");
                    }
                    else
                    {
                        dt.Columns.Add("status");
                        dt.Rows.Add("Data Not Inserted");
                    }
                }
                else if (obj_in.flag == "update")
                {
                    comm.Parameters.AddWithValue("@notice", obj_in.notice);
                    comm.Parameters.AddWithValue("@start_date", obj_in.start_date);
                    comm.Parameters.AddWithValue("@end_date", obj_in.end_date);
                    comm.Parameters.AddWithValue("@id", obj_in.id);
                    conn.Open();
                    i = comm.ExecuteNonQuery();
                    conn.Close();

                    //for api user i add custmize status in datatable 
                    if (i > 0)
                    {
                        dt.Columns.Add("status");
                        dt.Rows.Add("Updated Successfully");
                    }
                    else
                    {
                        dt.Columns.Add("status");
                        dt.Rows.Add("Data Not Updated");
                    }
                }
                else if (obj_in.flag == "delete")
                {
                    comm.Parameters.AddWithValue("@id", obj_in.id);
                    conn.Open();
                    i = comm.ExecuteNonQuery();
                    conn.Close();

                    //for api user i add custmize status in datatable 
                    if (i > 0)
                    {
                        //DataRow toInsert = dt.NewRow();
                        dt.Columns.Add("status");
                        dt.Rows.Add("Deleted Successfully");
                    }
                    else
                    {
                        dt.Columns.Add("status");
                        dt.Rows.Add("Data Not Deleted");
                    }
                }
                else
                {
                    //here i get datatable so i fill it directly in dt 
                    SqlDataAdapter data = new SqlDataAdapter(comm);
                    data.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Error in Adimn_bL => class - Admin_bl => method - sp_imp_notice() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //method to insert events 
        //author -> Deeksha
        public int insertevent(CalEvent_entity en)
        {
            int i = 0;
            try
            {
                DateTime event_date = DateTime.ParseExact(en.event_date, "dd/MM/yyyy", null);
                SqlCommand cmd = new SqlCommand("event_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "insert");
                cmd.Parameters.AddWithValue("@event_date", event_date);

                cmd.Parameters.AddWithValue("@event_name", en.event_name);
                conn.Open();
                i = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Error in Adimn_bL => class - Admin_bl => method - insertevent() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return i;
        }

        /// Author -> Saddam Shaikh
        public static int addEmpOfMon(Emp_Of_Month_Entity entity)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("spEmpOfMonth", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@title", entity.title);
                        cmd.Parameters.AddWithValue("@emp_name", entity.emp_name);
                        cmd.Parameters.AddWithValue("@dept_name", entity.dept_name);
                        cmd.Parameters.AddWithValue("@designation", entity.designation);
                        cmd.Parameters.AddWithValue("@file_path", entity.img_path);
                        cmd.Parameters.AddWithValue("@eom_date", entity.eom_date);
                        cmd.Parameters.AddWithValue("@flag", "insert");
                        conn.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL -> addEmpOfMon() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return result;
        }

        /// Author -> Saddam Shaikh
        public static int removeEmpOfMon(Emp_Of_Month_Entity entity)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("spEmpOfMonth", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@emp_id", entity.id);
                        cmd.Parameters.AddWithValue("@flag", "delete");
                        conn.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL -> removeEmpOfMon() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return result;
        }

        
        //Author --> Jignesh Panchal
        /// InsertVacancyData() :- Admin can insert vacancy data
        public int InsertVacancyData(VacancyEntity vac_obj)
        {
            int value = 0;
           
            string location = vac_obj.checkArray;

            
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_vaccancy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@position_name", vac_obj.position_name);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.Parameters.AddWithValue("@experience", vac_obj.experience);
                        cmd.Parameters.AddWithValue("@flag", vac_obj.flag);
                        conn.Open();
                        value = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived Admin_BL In InsertVacancyData(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return value;
        }

        //Author --> Jignesh Panchal
        /// ShowVacancyData():- Admin can show inserted vacancy data
        public DataTable ShowVacancyData(VacancyEntity vac_obj)

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
                InsertLog.WriteErrorLog("Error Arrived Admin_BL In ShowVacancyData(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //Author --> Jignesh Panchal
        /// InactiveVacancyData() :- Admin can inactive vacancy data if nedded
        public int InactiveVacancyData(VacancyEntity vac_obj)
        {
            int value = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_vaccancy", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", vac_obj.flag);
                        cmd.Parameters.AddWithValue("@id", vac_obj.id);
                        conn.Open();
                        value = cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived Admin_BL In InactiveVacancyData(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return value;
        }

        //Author --> Jignesh Panchal
        /// activeVacancyData() :- Admin can active vacancy data if vacancy data are inactive
        public int activeVacancyData(VacancyEntity vac_obj)
        {
            int value = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_vaccancy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", vac_obj.flag);
                        cmd.Parameters.AddWithValue("@id", vac_obj.id);
                        conn.Open();
                        value = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived Admin_BL In activeVacancyData(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return value;
        }

        //Author --> Jignesh Panchal
        /// DeleteVacancyData() :- Admin can delete particular vacancy data if needed
        public int DeleteVacancyData(VacancyEntity vac_obj)
        {
            int value = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_vaccancy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", vac_obj.flag);
                        cmd.Parameters.AddWithValue("@id", vac_obj.id);
                        conn.Open();
                        value = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived Admin_BL In DeleteVacancyData(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return value;
        }

        //Author - > ravi vaghela
        //add event gallery data to database
        public int add_event_gallery(Event_Gallery_Entity obj)
        {
            int response = 0;
            try
            {
                SqlConnection conn = new SqlConnection(Sql_Connection.connString);
                using (SqlCommand cmd = new SqlCommand("sp_event_gallery", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@event_image", obj.event_image);
                    cmd.Parameters.AddWithValue("@image_caption", obj.caption);
                    cmd.Parameters.AddWithValue("@flag", obj.flag);
                    conn.Open();
                    response = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exe)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL/add_event_gallery(): Message:" + exe.Message + "stacktrace:" + exe.StackTrace);
            }
            return response;
        }

        //Author - > ravi vaghela
        //Delete event gallery data from database
        public int delete_event_gallery(Event_Gallery_Entity obj)
        {
            int result = 0;
            try
            {
                SqlConnection conn = new SqlConnection(Sql_Connection.connString);
                using (SqlCommand cmd = new SqlCommand("sp_event_gallery", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", obj.flag);
                    cmd.Parameters.AddWithValue("@id", obj.id);

                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exe)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL / delete_event_gallery(): Message:" + exe.Message + "stacktrace:" + exe.StackTrace);

            }
            return result;
        }

        //Author --> Adarsh
        //Get Monthly leave Report
        public DataTable Monthly_Report(leave_entity lr)
        {

            DataTable dt = new DataTable();
            try
            {
                DateTime From_Date = new DateTime();
                DateTime To_Date = new DateTime();

                using (SqlConnection con = new SqlConnection(Sql_Connection.connString))
                {
                    if (!(string.IsNullOrWhiteSpace(lr.From_Date) &&  string.IsNullOrWhiteSpace(lr.To_Date)))
                    {
                    From_Date = DateTime.ParseExact(lr.From_Date, "dd/MM/yyyy", null);
                    To_Date = DateTime.ParseExact(lr.To_Date, "dd/MM/yyyy", null);
                    }
                    SqlCommand cmd = new SqlCommand("sp_mothlyLeaveReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", lr.flag);

                    if (lr.flag == "getempname")
                    {
                        cmd.Parameters.AddWithValue("@Employee_Name", lr.Employee_Name);

                    }
                    if (lr.flag == "getempdate")
                    {
                        cmd.Parameters.AddWithValue("@From_Date", From_Date);
                        cmd.Parameters.AddWithValue("@To_Date", To_Date);
                        cmd.Parameters.AddWithValue("@Employee_Name", lr.Employee_Name);
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }  
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL in Monthly_Report(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace + "InnerException" + ex.InnerException);
            }
            return dt;
        }

        //Author - > Adarsh
        //binding Reporting person dropdown/select for mapping
        public DataTable Select_MapReporter()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_selectReportManager", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL in Select_MapReporter(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace + "InnerException" + ex.InnerException);
            }
            return dt;

        }

        //Author - > Adarsh
        //get and display mapping data  
        public DataTable get_Mapping_data()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_mappingReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL in get_Mapping_data(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace + "InnerException" + ex.InnerException);
            }
            return dt;
        }

        //Author - > Adarsh
        //updating mapping report into mst_tbl
        public int Update_Mapping_Reporting(leave_entity mre)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Update_Mapping_", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", mre.ID);
                    cmd.Parameters.AddWithValue("@Reporting_Manager", mre.Report_manger);
                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }  
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL in Update_Mapping_Reporting(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace + "InnerException" + ex.InnerException);
            }
            return result;
        }

        //Author -> Adarsh
        // bind Leave_emp_name in monthly leave report
        public DataTable get_Leave_Emp_name()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_getLeave_EmployeeName", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    con.Close();
                }
                    
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admin_BL in get_Leave_Emp_name(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace + "InnerException" + ex.InnerException);
            }
            return dt;
        }

        //Author- Deeksha'
        //method to delete event
        public int delevent(CalEvent_entity en)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("event_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "delete");
                    cmd.Parameters.AddWithValue("@event_id", en.event_id);
                    conn.Open();
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Error in BL => class - Admin_bl => method - delevent() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return i;
        }

        public int ins_document(Document en)
        {
            int i = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_emp_doc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@doc_type_id", en.doc_type_id);
                    cmd.Parameters.AddWithValue("@emp_id", en.emp_id);
                    cmd.Parameters.AddWithValue("@filepath", en.filepath);
                    cmd.Parameters.AddWithValue("@uploadedtime", "");
                    cmd.Parameters.AddWithValue("@flag", "add");
                    conn.Open();
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL => method - ins_document() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return i;
        }

    }
}






