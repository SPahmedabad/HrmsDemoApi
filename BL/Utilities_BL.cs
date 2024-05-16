using Entity;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Utilities_BL
    {
        //Author --> Yaksh Maishery
        // BL for Apply Leave Form
        public static string ApplyLeave(leave_entity EN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ins_emp_leave", conn))
                    {
                        DateTime from_date = DateTime.ParseExact(EN.From_Date, "dd/MM/yyyy", null);
                        DateTime to_date = DateTime.ParseExact(EN.To_Date, "dd/MM/yyyy", null);


                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", EN.ID);
                        cmd.Parameters.AddWithValue("@emp_name", EN.Name);
                        cmd.Parameters.AddWithValue("@from_date", from_date);
                        cmd.Parameters.AddWithValue("@to_date", to_date);
                        cmd.Parameters.AddWithValue("@reason", EN.Reason);
                        cmd.Parameters.AddWithValue("@depart", EN.Department);
                        cmd.Parameters.AddWithValue("@email", EN.Email);
                        cmd.Parameters.AddWithValue("@report_manger", EN.Report_manger);
                        cmd.Parameters.AddWithValue("@emp_code", EN.EmployeeCode);
                        cmd.Parameters.AddWithValue("@status", EN.Status);

                        conn.Open();
                        int k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            DataTable dt = Utilities_BL.Emp_Heirarcy(EN);
                            if (dt.Rows.Count > 0)
                            {
                                Resignationentity en = new Resignationentity();
                                en.reason_of_leave = EN.Reason;
                                sendmaientityl objsend = new sendmaientityl();
                                objsend.subject = "Leave application";
                                objsend.body = $"<p style='text-align:left;font-weight: normal'>Dear <i>{Convert.ToString(dt.Rows[0]["manager_name"])}</i> <br>From - {EN.Email}<br><pre style='text-align:left;width:600px;white-space: normal'>{en.reason_of_leave}</pre></p>";
                                Utilities_BL.sendMail(dt, objsend);
                            }
                            return "success";
                        }
                        else
                        {
                            return "failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> utilities, method -> ApplyLeave \nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
                return "failed";
            }
        }

        //Author --> Yaksh Maishery
        // BL for Apply Leave Form
        public static DataTable EmpLeave(leave_entity EN)
        {
            DataTable DT = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_get_emp_leave", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", EN.ID);
                        cmd.Parameters.AddWithValue("@flag", EN.flag);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(DT);
                        return DT;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> utilities, method -> ApplyLeave \nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
                return DT;
            }
        }

        //Author --> Yaksh Maishery
        // BL for Apply Leave Form
        public static DataTable SubordinateLeave(leave_entity EN)
        {
            DataTable DT = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_get_emp_leave", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", EN.ID);
                        cmd.Parameters.AddWithValue("@flag", EN.flag);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(DT);
                        return DT;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> utilities, method -> SubordinateLeave \nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
                return DT;
            }
        }

        //Author --> Yaksh Maishery
        // BL for Apply Leave Form
        public static string UpdateLeave(leave_entity EN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_emp_leave_upd_status", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", EN.ID);
                        cmd.Parameters.AddWithValue("@from", EN.From_Date);
                        cmd.Parameters.AddWithValue("@to", EN.To_Date);
                        cmd.Parameters.AddWithValue("@status", EN.Status);
                        conn.Open();
                        int k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Email");
                            DataRow row_data = dt.NewRow();
                            row_data["Email"] = EN.Email;
                            dt.Rows.Add(row_data);

                            if (dt.Rows.Count > 0)
                            {
                                string from_date = EN.From_Date.Split('T')[0];
                                string to_date = EN.To_Date.Split('T')[0];
                                Resignationentity en = new Resignationentity();
                                en.reason_of_leave = $@"<p style='text-align:left;font-weight: normal'>Dear <b>{dt.Rows[0]["Email"]}</b><br>Your leave application is <b>{EN.Status}</b><br><br>from - <b>{from_date}</b><br>To - <b>{to_date}</b></p>";
                                sendmaientityl objsend = new sendmaientityl();
                                objsend.subject = $"Leave application - {EN.Status}";
                                objsend.body = $"{en.reason_of_leave}";
                                Utilities_BL.sendMail(dt, objsend);
                            }
                            return "success";
                        }
                        else
                        {
                            return "failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> utilities, method -> UpdateLeave \nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
                return "failed";
            }
        }

        //author Dinesh
        /// <summary>
        /// this method is for grievance_form table insertation or get form data
        /// </summary>
        /// <param name="obj_in"></param>
        /// <returns></returns>
        public DataTable sp_grievance_form(GrievanceForm obj_in)
        {
            SqlConnection conn = new SqlConnection(Sql_Connection.connString);
            DataTable dt = new DataTable();
            try
            {
                int i = 0;
                SqlCommand comm = new SqlCommand("sp_grievance_form", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@flag", obj_in.flag); //'insert' or 'get'

                if (obj_in.flag == "insert")
                {
                    comm.Parameters.AddWithValue("@victim_id", obj_in.VictimId);
                    comm.Parameters.AddWithValue("@DateOfIncident", obj_in.DateOfIncident);
                    comm.Parameters.AddWithValue("@TimeOfIncident", obj_in.TimeOfIncident);
                    comm.Parameters.AddWithValue("@LocationOfIncident", obj_in.LocationOfIncident);
                    comm.Parameters.AddWithValue("@ComplaintAgainst", obj_in.ComplaintAgainst);
                    comm.Parameters.AddWithValue("@IncidentDetails", obj_in.IncidentDetails);
                    comm.Parameters.AddWithValue("@WitnessEmployeeName", obj_in.WitnessEmployeeName);
                    comm.Parameters.AddWithValue("@WitnessContactNumber", obj_in.witnessContactNumber);
                    comm.Parameters.AddWithValue("@WitnessEmailId", obj_in.WitnessEmailId);
                    comm.Parameters.AddWithValue("@FirstTimeIssues", obj_in.FirstTimeIssues);
                    comm.Parameters.AddWithValue("@SuggestionsForResolve", obj_in.SuggestionsForResolve);
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
                else
                {
                    //this condition is for perticular victim form
                    if (obj_in.flag == "victimform")
                    {
                        comm.Parameters.AddWithValue("@id", obj_in.id);
                    }
                    //here i get datatable so i fill it directly in dt 
                    SqlDataAdapter data = new SqlDataAdapter(comm);
                    data.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL => method - sp_grievance_form() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //this method is for binding the profile ddl dynamically
        //Author -> Deeksha
        public DataTable refer_ddl(Refer_Emp_Entity en)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_refer_ddl", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL => method - refer_ddl() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //this method is for employees to add refernces
        //Author -> Deeksha
        public int ins_refernce(Refer_Emp_Entity en)
        {
            int i = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("reference_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@add_time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@emp_id", en.emp_id);
                    cmd.Parameters.AddWithValue("@flag", "add");
                    cmd.Parameters.AddWithValue("@profile_id", en.profile_id);
                    cmd.Parameters.AddWithValue("@name", en.name);
                    cmd.Parameters.AddWithValue("@mob_no", en.mob_no);
                    cmd.Parameters.AddWithValue("@upload_cv", en.upload_cv);
                    conn.Open();
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL => method - ins_refernce() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return i;
        }


        //this method is for showing refernces in a table to the admin
        //Author -> Deeksha
        public DataTable show_refer(Refer_Emp_Entity en)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("reference_sp", conn);
                    cmd.Parameters.AddWithValue("@add_time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@flag", "show");
                    cmd.Parameters.AddWithValue("@emp_id", en.emp_id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL => method - show_refer() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }


        //this method is for admin for deleting the references
        //Author -> Deeksha
        public int del_refer(Refer_Emp_Entity en)
        {
            int i = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd = new SqlCommand("reference_sp", conn);
                    cmd.Parameters.AddWithValue("@flag", "delete");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@candidate_id", en.candidate_id);
                    conn.Open();
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL => method - del_refer() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return i;
        }

        //Author --> Jay Patel
        //This method is used for insert employee resignation data in database.
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
                    mainstdata_cmd.Parameters.AddWithValue("@flag", "");
                    con.Open();
                    str = mainstdata_cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL => method - resing_emp() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return str;
        }

        //Author --->Jay Patel
        public static DataTable resingemplist(Resignationentity en)
        {
            DataTable ds_dep = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("resign_emp", conn); //new_emp_joindata
                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd_status.Parameters.AddWithValue("@employee_code", en.Employee_code);
                    cmd_status.Parameters.AddWithValue("@flag", en.flag);
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    dt_adp.Fill(ds_dep);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL =>  method - resingemplist(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds_dep;
        }

        //Author --> Jay Patel and Yaksh
        public static DataTable Emp_Heirarcy(leave_entity en)
        {
            DataTable dsemp = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("SP_Emp_Heirarcy", conn); //new_emp_joindata
                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd_status.Parameters.AddWithValue("@EMP_ID", en.ID);
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    dt_adp.Fill(dsemp);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Error in BL => class - Utilities_BL =>  method - Emp_Heirarcy(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return dsemp;
        }

        //Author --> Jay Patel and Yaksh
        public static void sendMail(DataTable dt, sendmaientityl en)
        {
            string To = Convert.ToString(dt.Rows[0]["Email"]);
            string cc = "";

            foreach (DataRow dr in dt.Rows)
            {
                if (To != Convert.ToString(dr["Email"]))
                {
                    if (cc == "")
                    {
                        cc = Convert.ToString(dr["Email"]);
                    }
                    else
                    {
                        cc = cc + "," + Convert.ToString(dr["Email"]);
                    }
                }
            }
            //string localPath = new Uri(Api_URL.SendEmphirarchy).AbsoluteUri;
            string path = AppDomain.CurrentDomain.BaseDirectory + Api_URL.SendEmphirarchy;
            StreamReader read = new StreamReader(path);
            var htmlbody = read.ReadToEnd();
            System.Net.Mail.MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dineshgalani42@gmail.com");
            mail.To.Add(To);
            if (cc != "")
            {
                mail.CC.Add(cc);
            }
            mail.Subject = en.subject;
            string htr = htmlbody.Replace("[digit]", en.body.ToString());
            mail.Body = htr;

            mail.IsBodyHtml = true;
            // mail.Attachments.Add(new Attachment(new MemoryStream(Attachment), "PrintedForm.pdf"));
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

                //if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == smtp.)
                //{
                //}
                //emp_ent.IsSend = 'Y';

                //GetDataFromAPI.Get_mis_formstatus_data(emp_ent);
                //  IsSend = true;

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Operation/sendMail() when sending mail to Trainee: : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
                // IsSend = false;

                //GetDataFromAPI.Get_mis_formstatus_data(emp_ent);
            }

        }

    }
}
