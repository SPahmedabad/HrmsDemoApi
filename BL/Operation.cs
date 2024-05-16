using Entity;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    public class Operation
    {
        string connStr = Sql_Connection.connString;

        //Author -> Ravi
        //call sp for get all questions
        //author ravi vaghela
        //add in operation class
        public DataTable getQuestions()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(Sql_Connection.connString);
                using (SqlCommand cmd = new SqlCommand("sp_get_questions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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
                InsertLog.WriteErrorLog(exe.Message);

            }
            return dt;
        }


        //Author -> Hemangini
        //Method for getting data from employee Table
        EmployeeEntity obj_emp = new EmployeeEntity();

        public DataSet Emp_get(EmployeeEntity entity)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(Sql_Connection.connString);
                SqlCommand comm = new SqlCommand("sp_emp_data", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@flag", "get");
                SqlDataAdapter data = new SqlDataAdapter(comm);
                data.Fill(ds);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Emp_get(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return ds;
        }


        public DataTable EmpCheck(EmployeeMainData obj)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(Sql_Connection.connString);
                SqlCommand comm = new SqlCommand("checkRecord", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Official_EmaildID", obj.Official_EmaildID);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in EmpCheck(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);

            }
            return ds.Tables[0];
        }

        //Author -> Hemangini
        //method for inserting data into the employee table.

        public string Emp_save(EmployeeMainData obj_emp)
        {
            //DataSet ds = new DataSet();
            string msg = "";
            try
            {
                SqlConnection conn = new SqlConnection(Sql_Connection.connString);
                SqlCommand comm = new SqlCommand("sp_emp_data", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@dept_id", obj_emp.dept_id);
                comm.Parameters.AddWithValue("@emp_name", obj_emp.Employee_Name);
                comm.Parameters.AddWithValue("@DOJ", obj_emp.DOJ);
                comm.Parameters.AddWithValue("@Email_id", obj_emp.Email_id);
                comm.Parameters.AddWithValue("@Official_EmaildID", obj_emp.Official_EmaildID);
                comm.Parameters.AddWithValue("@PanCard_No", obj_emp.PanCard_No);
                comm.Parameters.AddWithValue("@Adhar_No", obj_emp.Adhar_No);
                comm.Parameters.AddWithValue("@ESIC_Number", obj_emp.ESIC_Number);
                comm.Parameters.AddWithValue("@Mediclam_ID", obj_emp.Mediclaim_ID);
                comm.Parameters.AddWithValue("@Contact_Number", obj_emp.Emerg_ConatactNumber);
                comm.Parameters.AddWithValue("@UAN_Number", obj_emp.UAN_Number);
                comm.Parameters.AddWithValue("@password", obj_emp.password);
                comm.Parameters.AddWithValue("@flag", "save");
                //SqlDataAdapter data = new SqlDataAdapter(comm);
                //data.Fill(ds);
                conn.Open();
                int i = comm.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    msg = "Employee Added Successfully";
                }
                else if (i == 0)
                {
                    msg = "Employee Not Added";
                }
                else
                {
                    msg = "Employee Already Present!";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Emp_save(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return msg;
        }

        //Author -> Deeksha
        //Method for getting data from module Table

        public DataTable getData(FeedbackFormEntity en)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(Sql_Connection.connString);
                SqlCommand cm = new SqlCommand("sp_module", con);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@mod_name", "");
                cm.Parameters.AddWithValue("@dept_id", en.ddlDepartment);
                cm.Parameters.AddWithValue("@flag", "get");
                SqlDataAdapter da = new SqlDataAdapter(cm);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in getData(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //Author -> Deeksha
        //method for inserting data into the module table.

        public string saveData(FeedbackFormEntity en)
        {
            string msg = "";
            try
            {
                SqlConnection con = new SqlConnection(Sql_Connection.connString);
                SqlCommand cm = new SqlCommand("sp_module", con);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@mod_name", en.Module_Entity.mod_name);
                cm.Parameters.AddWithValue("@dept_id", en.Department_Entity.dept_id);
                cm.Parameters.AddWithValue("@flag", "save");
                con.Open();
                int result = cm.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {
                    msg = "Module added successfully";
                }
                else if (result == 0)
                {
                    msg = "Module not added";
                }
                else
                {
                    msg = "Module Already present.";

                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in saveData(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return msg;
        }

        //Author -> Deeksha
        //Method for getting data from department Table

        public DataTable get_dept(FeedbackFormEntity en_dept)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(Sql_Connection.connString);
                SqlCommand cm = new SqlCommand("sp_dept_data", con);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@dept_name", "");
                cm.Parameters.AddWithValue("@flag", "get");
                SqlDataAdapter da = new SqlDataAdapter(cm);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in get_dept(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }
        //method for inserting data into the department table.

        //Author -> Deeksha
        //Method for saving data to department Table
        public string save_dept(FeedbackFormEntity ent)
        {
            string msg = "";
            try
            {
                SqlConnection con = new SqlConnection(Sql_Connection.connString);
                SqlCommand cm = new SqlCommand("sp_dept_data", con);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@dept_name", ent.Department_Entity.dept_name);
                cm.Parameters.AddWithValue("@flag", "save");
                con.Open();
                int result = cm.ExecuteNonQuery();
                con.Close();

                if (result == 0)
                {
                    msg = "Department Not Added";
                }   
                else if (result == -1)
                {
                    msg = "Department Already Present ";
                }
                else
                {
                    msg = "Department Added Successfully.. ";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in save_dept(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return msg;
        }

        //Author -> Satyam
        /// <summary>
        /// DataBase Connection Method To
        /// Get Data For FeedBack Data On Two Flag
        /// Department,Module
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static DataSet get_data(FeedbackFormEntity en)
        {
            DataSet ds_dep = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("sp_Feed_Data", conn);
                    cmd_status.Parameters.AddWithValue("@dept_id", en.ddlDepartment);
                    cmd_status.Parameters.AddWithValue("@mod_id", en.ddlModuleName);
                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    dt_adp.Fill(ds_dep);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Operation : get_Dep_Data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds_dep;
        }

        //Author -> Chirag Dhruv
        /// <summary>
        /// here we fetch data from table.
        /// here we pass trainee_id and based on sp perform operation.
        /// </summary>
        public DataSet FillFormData(int trainee_id)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    using (SqlCommand cmd = new SqlCommand("Trainee_Profile", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@trainee_id", trainee_id);
                        conn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in FillFormData() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return ds;
        }


        //Author - > ravi vaghela
        public int Feedback_save(FeedbackFormEntity obj)
        {

            int response = 0;
            try
            {

                SqlConnection conn = new SqlConnection(Sql_Connection.connString);
                using (SqlCommand cmd = new SqlCommand("sp_trainee_feedback", conn))
                {
                    //check mode of training


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@dept_id", obj.ddlDepartment);
                    cmd.Parameters.AddWithValue("@start_date", obj.StartDate);
                    cmd.Parameters.AddWithValue("@end_date", obj.EndDate);
                    cmd.Parameters.AddWithValue("@mod_id", obj.ddlModuleName);
                    cmd.Parameters.AddWithValue("@emp_id", obj.ddlTrainer);
                    cmd.Parameters.AddWithValue("@city", obj.City);
                    cmd.Parameters.AddWithValue("@state", obj.State);

                    cmd.Parameters.AddWithValue("@mode_of_training_is_online_ofline", obj.ModeOfTraining);

                    //training enjoyment three varaible concat using comma saprate value and create one more variable 
                    string trainee_enjoyment = obj.EnjoyMostAboutTraining1 + "," + obj.EnjoyMostAboutTraining2 + "," + obj.EnjoyMostAboutTraining3;
                    cmd.Parameters.AddWithValue("@trainee_enjoyment", trainee_enjoyment);
                    cmd.Parameters.AddWithValue("@need_improve", obj.ImproveTraining);
                    cmd.Parameters.AddWithValue("@trainee_learn ", obj.SkillsLearn);
                    cmd.Parameters.AddWithValue("@trainee_comments ", obj.Comments);
                    cmd.Parameters.AddWithValue("@mail_log_id", obj.mail_log_id);
                    cmd.Parameters.AddWithValue("@q_id_str ", "1,2,3,4,5,6,7,8,9,10");

                    //combine rating values 
                    //string ratings = obj.ddl_ques1 + "," + obj.ddl_ques2 + "," + obj.ddl_ques3 + "," + obj.ddl_ques4 + "," + obj.ddl_ques5 + "," + obj.ddl_ques6 + "," + obj.ddl_ques7 + "," + obj.ddl_ques8 + "," + obj.ddl_ques9 + "," + obj.ddl_ques10;
                    cmd.Parameters.AddWithValue("@rating_str ", obj.Answers);
                    conn.Open();
                    response = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exe)
            {
                InsertLog.WriteErrorLog(exe.Message);

            }
            return response;
        }

        //Author ----> Jiignesh
        //listview method call
        public DataTable Get_Mis_Listview_Data(AdminEntity En)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("sp_department_lv_msi", conn);
                    if (En.flag == "dept")
                    {
                        cmd_status.Parameters.AddWithValue("@dept_id", En.dpt_id);
                        cmd_status.Parameters.AddWithValue("@flag", En.flag);
                    }
                    else
                    {
                        cmd_status.Parameters.AddWithValue("@dept_id", En.dpt_id);
                        cmd_status.Parameters.AddWithValue("@mod_id", En.md_id);
                        cmd_status.Parameters.AddWithValue("@flag", En.flag);
                    }

                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    dt_adp.Fill(ds);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("BL:-DeptGetMisListView :- Get_Mis_Listview_Data(Method) : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds.Tables[0];
        }

        //Author ----> Jignesh
        //dropdown Method call
        public DataTable MisDropdown(AdminEntity En)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("sp_Fetch_DeptMod", conn);
                    if (En.flag == "Module")
                    {
                        cmd_status.Parameters.AddWithValue("@flag", En.flag);
                        cmd_status.Parameters.AddWithValue("@dept_id", En.dpt_id);

                    }
                    else
                    {
                        cmd_status.Parameters.AddWithValue("@flag", En.flag);
                    }

                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    dt_adp.Fill(ds);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("BL:-DeptMisListview :- Mislv(Method) : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds.Tables[0];
        }

        //Author -> Satyam
        //DataBase Connection Method To Get 
        //Employee With EMail And Joining Date , Dept_Name
        public static DataTable Emp_MailList()
        {
            DataTable ds_dep = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("sp_employee_mail", conn);
                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                    dt_adp.Fill(ds_dep);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Operation : Emp_MailList : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds_dep;
        }

        //author chirag
        /// <summary>
        /// 
        /// here we send data to trainee.
        /// and check mail if mail is sent successfully then show message.
        /// and if in list incorrect mail found then then we write error log..
        /// here we pass List of entity and loop it and send mail.
        /// </summary>
        public string SendMailToTrainee(List<EmployeeEntity> obj_trainee)
        {
            string msg = "";
            try
            {
              // Thread thread = null;
                int i = 0;
                for ( i = 0 ; i < obj_trainee.Count; i++)
                {
                    //sendMail(obj_trainee[i].emp_email, obj_trainee[i].querystring, obj_trainee[i].emp_name);
                  //  thread = new Thread(() => sendMail(obj_trainee[i]));
                    sendMail(obj_trainee[i]);
                   // thread.Start();
                    
                }
               // thread.Abort();
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Operation/SendMailToTrainee() when sending mail to Trainee: : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return msg = "Mail Sent Successfulllyyy";
        }

        //author chirag
        // here we send mail to traineeeeee.....      
        public void sendMail(EmployeeEntity emp_ent)
        {
            //string localPath = new Uri(Api_URL.SendMailTemplate).AbsoluteUri;
            string path = AppDomain.CurrentDomain.BaseDirectory + Api_URL.SendMailTemplate;
            StreamReader read = new StreamReader(path);
            var htmlbody = read.ReadToEnd();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dineshgalani42@gmail.com");
            mail.To.Add(emp_ent.emp_email);
            mail.Subject = "Fill FeedBack Form";
            string htr = htmlbody.Replace("[newusername]", emp_ent.emp_name);
            string mdName = htr.Replace("[modulename]", emp_ent.moduleEntity.mod_name);
            string deptName = mdName.Replace("[DeptName]", emp_ent.deptEntity.dept_name);
            string emp_Name = deptName.Replace("[trainername]",emp_ent.TrainerName);
            mail.Body = emp_Name.Replace("[query]", emp_ent.querystring);

            mail.IsBodyHtml = true;
            // mail.Attachments.Add(new Attachment(new MemoryStream(Attachment), "PrintedForm.pdf"));
            mail.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("dineshgalani42@gmail.com", "tezlujempzgqeunq");

            emp_ent.Flag = "insert";
            try
            {
                smtp.Send(mail);

                //if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == smtp.)
                //{
                //}
                emp_ent.IsSend = 'Y';

                //GetDataFromAPI.Get_mis_formstatus_data(emp_ent);
                //  IsSend = true;

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Operation/sendMail() when sending mail to Trainee: : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
                // IsSend = false;

                //GetDataFromAPI.Get_mis_formstatus_data(emp_ent);
            }

            using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
            {
                SqlCommand cmd_status = new SqlCommand("sp_mis_check_status", conn);
                cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                cmd_status.Parameters.AddWithValue("@flag", emp_ent.Flag);
                cmd_status.Parameters.AddWithValue("@mail_log_id", emp_ent.email_log_id);
                cmd_status.Parameters.AddWithValue("@quary_string", emp_ent.querystring);
                cmd_status.Parameters.AddWithValue("@isSend", emp_ent.IsSend);
                cmd_status.Parameters.AddWithValue("@dept_id", emp_ent.deptEntity.dept_id);
                cmd_status.Parameters.AddWithValue("@mod_id", emp_ent.moduleEntity.mod_id);
                cmd_status.Parameters.AddWithValue("@emp_id", emp_ent.emp_id);
                conn.Open();
                cmd_status.ExecuteNonQuery();
            }
        }



        //author hemangini
        //method for encrypt the string
        public static string encrypt(string encryptString)
        {
            try
            {
                string EncryptionKey = "01234ABCHIJPQRXYZ";
                byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptString = Convert.ToBase64String(ms.ToArray());
                    }
                }
                
            }
            catch(Exception ex)
            {
                InsertLog.WriteErrorLog("Error while calling encrypt() method : " + ex.Message);
            }
            return encryptString;
        }

        //Author : Hemangini
        //method for decrypt the string which is encrypted
        public static string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = "01234ABCHIJPQRXYZ";
                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch(Exception ex)
            {
                InsertLog.WriteErrorLog("Someone try to modify QuaryString -- Error while calling Decrypt() method : " + ex.Message + " StackTrace : " + ex.StackTrace );
                return cipherText = "NotValid";
            }
     
            return cipherText;
        }

        //Author --> Jay Patel/Dinesh
        //This method return all trainee_id who submitted data or not
        public static DataSet get_mis_fromdata(EmployeeEntity log_entity)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
                {
                    SqlCommand cmd_status = new SqlCommand("sp_mis_check_status", conn);
                    cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd_status.Parameters.AddWithValue("@flag", log_entity.Flag);
                    cmd_status.Parameters.AddWithValue("@mail_log_id", log_entity.email_log_id);
                    cmd_status.Parameters.AddWithValue("@quary_string", log_entity.querystring);
                    cmd_status.Parameters.AddWithValue("@isSend", log_entity.IsSend); 
                    cmd_status.Parameters.AddWithValue("@dept_id",log_entity.deptEntity.dept_id);
                    cmd_status.Parameters.AddWithValue("@mod_id",log_entity.moduleEntity.mod_id);
                    cmd_status.Parameters.AddWithValue("@emp_id",log_entity.emp_id);

                    if (log_entity.Flag == "insert")
                    {
                        conn.Open();
                        cmd_status.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlDataAdapter dt_adp = new SqlDataAdapter(cmd_status);
                        dt_adp.Fill(ds);
                    }                 
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Operation : get_mis_fromdata : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds;
        }


        //author deeksha
        /// <summary>
        /// this method return value which is already summited by that we make decision link is used or not 
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public DataTable log_exists(EmployeeEntity ent)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(Sql_Connection.connString);
                SqlCommand cmd_exists = new SqlCommand("log_exist", con);
                cmd_exists.CommandType = CommandType.StoredProcedure;
                cmd_exists.Parameters.AddWithValue("@mail_log_id", ent.email_log_id);
                SqlDataAdapter da = new SqlDataAdapter(cmd_exists);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in mail log id check:" + ex.Message + "Stack Trace" + ex.StackTrace + "Inner Exception" + ex.InnerException);
            }
            return dt;
        }


        public void sendCustomMail(EmployeeEntity emp_ent)
        {
            //string localPath = new Uri(Api_URL.SendMailTemplate).AbsoluteUri;
            string path = AppDomain.CurrentDomain.BaseDirectory + Api_URL.SendMailTemplate;
            StreamReader read = new StreamReader(path);
            var htmlbody = read.ReadToEnd();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dineshgalani42@gmail.com");
            mail.To.Add(emp_ent.emp_email);
            mail.Subject = "Fill FeedBack Form";
            string htr = htmlbody.Replace("[newusername]", emp_ent.emp_name);
            string mdName = htr.Replace("[modulename]", emp_ent.moduleEntity.mod_name);
            string deptName = mdName.Replace("[DeptName]", emp_ent.deptEntity.dept_name);
            string emp_Name = deptName.Replace("[trainername]", emp_ent.TrainerName);
            mail.Body = emp_Name.Replace("[query]", emp_ent.querystring);

            mail.IsBodyHtml = true;
            // mail.Attachments.Add(new Attachment(new MemoryStream(Attachment), "PrintedForm.pdf"));
            mail.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("dineshgalani42@gmail.com", "tezlujempzgqeunq");

            emp_ent.Flag = "insert";
            try
            {
                smtp.Send(mail);

                //if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == smtp.)
                //{
                //}
                emp_ent.IsSend = 'Y';

                //GetDataFromAPI.Get_mis_formstatus_data(emp_ent);
                //  IsSend = true;

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Operation/sendMail() when sending mail to Trainee: : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
                // IsSend = false;

                //GetDataFromAPI.Get_mis_formstatus_data(emp_ent);
            }

            using (SqlConnection conn = new SqlConnection(Sql_Connection.connString))
            {
                SqlCommand cmd_status = new SqlCommand("sp_mis_check_status", conn);
                cmd_status.CommandType = System.Data.CommandType.StoredProcedure;
                cmd_status.Parameters.AddWithValue("@flag", emp_ent.Flag);
                cmd_status.Parameters.AddWithValue("@mail_log_id", emp_ent.email_log_id);
                cmd_status.Parameters.AddWithValue("@quary_string", emp_ent.querystring);
                cmd_status.Parameters.AddWithValue("@isSend", emp_ent.IsSend);
                cmd_status.Parameters.AddWithValue("@dept_id", emp_ent.deptEntity.dept_id);
                cmd_status.Parameters.AddWithValue("@mod_id", emp_ent.moduleEntity.mod_id);
                cmd_status.Parameters.AddWithValue("@emp_id", emp_ent.emp_id);
                conn.Open();
                cmd_status.ExecuteNonQuery();
            }
        }

    }
}



