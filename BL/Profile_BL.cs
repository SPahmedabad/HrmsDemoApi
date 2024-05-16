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
    public class Profile_BL
    {
        //  Author ---> Satyam
        //  Sql Operation Method For Get Specified USer Details
        public static DataSet get_oper_User_Records(UserDetails en)
        {
            DataSet ds_dep = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("sp_emp_main_data", conn);
                    cmd_status.Parameters.AddWithValue("@Official_Email", "");
                    cmd_status.Parameters.AddWithValue("@password", "");
                    cmd_status.Parameters.AddWithValue("@flag", "");
                    cmd_status.Parameters.AddWithValue("@id", en.id);
                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    dt_adp.Fill(ds_dep);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Profile_BL : get_oper_User_Records : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds_dep;
        }



        //Author --> Satyam
        // Sql Operation Method For Update Specified User Details
        public static string update_Oper_User_Records(EmployeeMainData en)
        {
            string message = "";
            DateTime DOB = DateTime.ParseExact(en.DOB, "dd/MM/yyyy", null);
            DateTime DOJ = DateTime.ParseExact(en.DOJ, "dd/MM/yyyy", null);
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("sp_update_empmain_data", conn);
                    cmd_status.Parameters.AddWithValue("@Employee_Name", en.Employee_Name);
                    cmd_status.Parameters.AddWithValue("@DOJ", DOJ);
                    cmd_status.Parameters.AddWithValue("@Employee_Code", en.Employee_Code);
                    cmd_status.Parameters.AddWithValue("@Desigination", en.Desigination);
                    cmd_status.Parameters.AddWithValue("@Working_Status", en.Working_Status);
                    cmd_status.Parameters.AddWithValue("@Location", en.Location);
                    cmd_status.Parameters.AddWithValue("@Total_Experience", en.Total_Experience);
                    cmd_status.Parameters.AddWithValue("@DOB", DOB);
                    cmd_status.Parameters.AddWithValue("@Blood_Group", en.Blood_Group);
                    cmd_status.Parameters.AddWithValue("@FatherHusband_Name", en.FatherHusband_Name);
                    cmd_status.Parameters.AddWithValue("@Mother_Name", en.Mother_Name);
                    cmd_status.Parameters.AddWithValue("@Highest_Qualification", en.Highest_Qualification);
                    cmd_status.Parameters.AddWithValue("@Emerg_ConatactPerson", en.Emerg_ConatactPerson);
                    cmd_status.Parameters.AddWithValue("@Emerg_ConatactNumber", en.Emerg_ConatactNumber);
                    cmd_status.Parameters.AddWithValue("@Email_id", en.Email_id);
                    cmd_status.Parameters.AddWithValue("@PanCard_No", en.PanCard_No);
                    cmd_status.Parameters.AddWithValue("@Adhar_No", en.Adhar_No);
                    cmd_status.Parameters.AddWithValue("@Curr_Address", en.Curr_Address);
                    cmd_status.Parameters.AddWithValue("@Perma_Addresss", en.Perma_Addresss);
                    cmd_status.Parameters.AddWithValue("@Official_EmaildID", en.Official_EmaildID);
                    cmd_status.Parameters.AddWithValue("@UAN_Number", en.UAN_Number);
                    cmd_status.Parameters.AddWithValue("@ESIC_Number", en.ESIC_Number);
                    cmd_status.Parameters.AddWithValue("@Mediclam_ID", en.Mediclaim_ID);
                    cmd_status.Parameters.AddWithValue("@role", en.role);
                    cmd_status.Parameters.AddWithValue("@Image", en.Image);
                    cmd_status.Parameters.AddWithValue("@Gender", en.Gender);
                    cmd_status.Parameters.AddWithValue("@Department", en.Department);
                    cmd_status.Parameters.AddWithValue("@flag", en.flag);
                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    conn.Open();
                    int k = cmd_status.ExecuteNonQuery();
                    if (k > 0)
                    {
                        message = "Success";
                    }
                    else
                    {
                        message = "Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Operation : get_Dep_Data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return message;
        }
    }
}

