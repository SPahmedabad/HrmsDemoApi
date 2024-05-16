using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BL;
using Library;
using Entity;
using System.IO;
using System.Net.Mail;

namespace BL
{
    public class LoginHRMS
    {
        //author => Chirag Dhruv
        /// <summary>
        /// this method checks username and password of user.
        /// and in response we get user values and response to Logincontroller.
        /// </summary>
        /// <returns></returns>
        public DataTable UserLogin(UserEntity obj_data )
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using(SqlCommand cmd = new SqlCommand("sp_emp_main_data",conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Official_Email", obj_data.username);
                        cmd.Parameters.AddWithValue("@password",obj_data.password);
                        cmd.Parameters.AddWithValue("@flag",obj_data.role);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in UserLogin()/LoginHRMS.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //author =>  Chirag Dhruv
        /// <summary>
        /// this method is use for add log of employee in log table.
        /// here we get response in int.
        /// here we pass it to LoginController.
        /// </summary>
        public int emplog(logEntity objlog)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("manageLog", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OfficialEmailId", objlog.email_id);
                        cmd.Parameters.AddWithValue("@flag", "ins");
                        conn.Open();
                        result=cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                InsertLog.WriteErrorLog("Error in emplog()/LoginHRMS.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return result;
        }


        //author =>  Chirag Dhruv
        //here we get otp from database on button click.
        public DataTable getotp(string emailid)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd_otp_obj = new SqlCommand("get_otp_validate", con))
                    {

                        cmd_otp_obj.CommandType = CommandType.StoredProcedure;
                        cmd_otp_obj.Parameters.AddWithValue("@Email", emailid);
                        cmd_otp_obj.Parameters.AddWithValue("@flag", "get");
                        cmd_otp_obj.Parameters.AddWithValue("@otp", 0);
                        cmd_otp_obj.Parameters["@otp"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd_otp_obj);
                        sda.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in getotp()/LoginHRMS.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }

            return ds.Tables[0];
        }

        //author Chirag dhruv
        // here we send mail to Employee...      
        public void sendMail(string Email, int otp)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + Api_URL.SendOTPTemplate;
            StreamReader read = new StreamReader(path);
            var htmlbody = read.ReadToEnd();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dineshgalani42@gmail.com");
            mail.To.Add(Email);
            mail.Subject = "HRMS Reset Password";
            string htr = htmlbody.Replace("[digit]", otp.ToString());
            mail.Body = htr;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("dineshgalani42@gmail.com", "tezlujempzgqeunq");
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in sendMail()/LoginHRMS.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);             
            }
        }

        //author Chirag dhruv
        //here we verifys otp and store in int and return otp.
        public DataTable verifyotp(logEntity obj)
        {
            int otp = 0;
            DataSet ds_otp = new DataSet();
            try
            {
                using (SqlConnection con_obj = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd_otp_obj = new SqlCommand("get_otp_validate", con_obj))
                    {

                        cmd_otp_obj.CommandType = CommandType.StoredProcedure;
                        cmd_otp_obj.Parameters.AddWithValue("@Email", obj.email_id);
                        cmd_otp_obj.Parameters.AddWithValue("@flag", "verify");
                        cmd_otp_obj.Parameters.AddWithValue("@otp", obj.otp);
                        con_obj.Open();
                        SqlDataAdapter sda_otp = new SqlDataAdapter(cmd_otp_obj);
                        sda_otp.Fill(ds_otp);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in varifyotp()/LoginHRMS.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }

            return ds_otp.Tables[0];
        }

        //author Chirag dhruv
        /// <summary>
        /// in this method we update password of employee in tbl_employee_master.
        /// here we get response in integer value 0 or 1.
        /// </summary>
        /// <param name="objreset"></param>
        /// <returns></returns>
        public int resetPass(logEntity objreset)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_forget_pass", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", objreset.email_id);
                        cmd.Parameters.AddWithValue("@password", objreset.Password);
                        conn.Open();
                        result = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in resetPass()/LoginHRMS.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return result;
        }


        // Author -> Yaksh Maishery
        // Sign Out Method
        public static string SignOut(leave_entity EN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SignOut", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", EN.Email);
                        cmd.Parameters.AddWithValue("@flag", EN.flag);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable DT = new DataTable();
                        sda.Fill(DT);
                        return Convert.ToString(DT.Rows[0]["Result"]);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> , method -> SignOut \nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
                return "failed";
            }
        }

         //author =>  Chirag Dhruv
        /// <summary>
        /// this method is use for update log of employee in log table.
        /// here we get response in int.
        /// here we pass it to LoginController.
        /// </summary>
        public int emplogOut(logEntity objlog)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("manageLog", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OfficialEmailId", objlog.email_id);
                        cmd.Parameters.AddWithValue("@flag", "upd");
                        conn.Open();
                        result = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in emplogOut()/LoginHRMS.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return result;
        }

        public static DataTable checkemail(logEntity obj)
        {
            DataTable ds_otp = new DataTable();
            try
            {
                using (SqlConnection con_obj = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd_otp_obj = new SqlCommand("resign_emp", con_obj))
                    {

                        cmd_otp_obj.CommandType = CommandType.StoredProcedure;
                        cmd_otp_obj.Parameters.AddWithValue("@flag", "checkemail");
                        cmd_otp_obj.Parameters.AddWithValue("@email", obj.email_id);
                        con_obj.Open();
                        SqlDataAdapter sda_otp = new SqlDataAdapter(cmd_otp_obj);
                        sda_otp.Fill(ds_otp);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in BL --> LoginHRMS  Error in checkemail(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return ds_otp;
        }

    }
}

