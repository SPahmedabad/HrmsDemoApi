using BL;
using Entity;
using Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GetDataFromAPI
    {
        //Author -> Hemanigini
        //This method will help us get all the employees present in employee table
        public DataTable  Get_Employee(EmployeeEntity entity)
        {
            //string msg = "";
            DataSet dt = new DataSet();
            
            try
            {
                using (var client = new HttpClient())
                {
                    
                    //HttpResponseMessage response = client.PostAsync(url, content).Result;
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_GetEmployee, entity).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dt = JsonConvert.DeserializeObject<DataSet>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in GetDataFromAPI/Get_Employee() (get employee): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt.Tables[0];
        }
        
        //Author -> Hemanigini
        //This method will save all the new employees into employee table 
        public string Save_Data(EmployeeEntity obj_emp)
        {
            string data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(ApiBaseAddress.baseAddress);
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //HttpResponseMessage response = client.PostAsJsonAsync("api/Employee/SaveData", obj_emp).Result;
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_AddEmployee, obj_emp).Result;
                    string API_result = response.Content.ReadAsStringAsync().Result;                  

                    if (response.IsSuccessStatusCode)
                    {
                        data = API_result;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in GetDataFromAPI/Save_Data() (save employee): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return data;
        }

        //Author -> Deeksha
        //This method will fetch the data from the module table (get method)
        public DataTable getData(FeedbackFormEntity en_mod)
        {
            DataTable dt = new DataTable();
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    //string url = Api_URL.baseAddress + "api/module/getData";
                    //string data = JsonConvert.SerializeObject(en_mod);
                    //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = client.PostAsync(url, content).Result;
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_ModuleGetdata, en_mod).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dt = JsonConvert.DeserializeObject<DataTable>(result);
                    }                   
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in GetDataFromAPI/getData() (get module): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //Author -> Deeksha
        //This method will insert the values into the module table (set method)
        public DataTable saveData(FeedbackFormEntity en_mod)
        {
            DataTable dt = new DataTable();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string url = Api_URL.baseAddress + "api/module/saveData";
                    //string data = JsonConvert.SerializeObject(en_mod);
                    //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = client.PostAsync(url, content).Result;
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_ModuleInsertData, en_mod).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dt = JsonConvert.DeserializeObject<DataTable>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in GetDataFromAPI/saveData() (save module): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //Author -> Deeksha
        //This method will fetch the data from the Department table (get method)
        public static DataTable getDept(FeedbackFormEntity en_dept)
        {
            DataTable dt = new DataTable();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string url = Api_URL.baseAddress + "api/Training/getDept";
                    //string data = JsonConvert.SerializeObject(en_dept);
                    //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = client.PostAsync(url, content).Result;
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_Training_GetDept, en_dept).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dt = JsonConvert.DeserializeObject<DataTable>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in GetDataFromAPI/getDept(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return dt;
        }

        //Author -> Deeksha
        //This method will insert the values into the department table (set method)
        public string saveDept(FeedbackFormEntity en_dept)
        {
            string result = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string url = Api_URL.baseAddress + "api/Training/InsertDept";
                    //string data = JsonConvert.SerializeObject(en_dept);
                    //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = client.PostAsync(url, content).Result;
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_Training_InsertDept, en_dept).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                                             
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in GetDataFromAPI/saveDept(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return result;
        }

        //Author -> Chirag Dhruv
        /// <summary>
        /// here we fetch trainee_id from FeedbackForm.
        /// after that pass that id to controller.
        /// here we get result in Json format so we convert result into dataset.
        /// after that we send dataset to FeedBackForm
        /// </summary>
        public DataSet FillFormData(FeedbackFormEntity obj)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_getProfile, obj).Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    DataSet data = JsonConvert.DeserializeObject<DataSet>(result);
                    if (response.IsSuccessStatusCode)
                    {
                        ds = data;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in SendToApi/FillFormData() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }

            return ds;
        }

        //Author -> Saddam Shaikh
        public static int saveFeedbackFormData(FeedbackFormEntity entity)
        {
            //string msg = "";
            int rowsInserted = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    //string msg = "";
                    string url = Api_URL.baseAddress + Api_URL.subURL_save_Feedback;
                    string data = JsonConvert.SerializeObject(entity);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        rowsInserted = Convert.ToInt32(JsonConvert.DeserializeObject<string>(result));
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in GetDataFromAPI/saveFeedbackFormData() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return rowsInserted;
        }

        //Authoe --> Satyam
        /// <summary>
        /// BL API CALL Method For Call API And Get
        /// based On Factor
        public static DataSet get_feed_data(FeedbackFormEntity en)
        {
            DataSet ds_dep = new DataSet();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = client.PostAsJsonAsync(Api_URL.subURL_get_Afeed_data, en).Result;
                    var data = Response.Content.ReadAsStringAsync().Result;

                    if (Response.IsSuccessStatusCode)
                    {
                        ds_dep = (DataSet)JsonConvert.DeserializeObject(data, typeof(DataSet));
                    }
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("GetDataFromAPI : get_feed_data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds_dep;
        }
        //Author --> Chirag
        /// <summary>
        /// here we fetch data from frontend and send to controller.
        /// this api is use for binding Module of ddl
        /// </summary>
        public DataTable getModule(FeedbackFormEntity en)
        {
            DataTable ds = new DataTable();
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_ModuleGetdata, en).Result;

                    string result = response.Content.ReadAsStringAsync().Result;
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(result.Trim());

                    if (response.IsSuccessStatusCode)
                    {
                        ds = data;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in FetDataFromAPI/getModule() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }

            return ds;
        }
        //Author --> Chirag
        /// <summary>
        /// here we fetch data from frontend and send to controller.
        /// this api is use for binding employee of ddl
        /// </summary>

        public DataTable getEmployee()
        {
            DataTable ds = new DataTable();
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_GetEmployee, "").Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    DataSet data = JsonConvert.DeserializeObject<DataSet>(result.Trim());

                    if (response.IsSuccessStatusCode)
                    {
                        ds = data.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in FetDataFromAPI/getEmployee() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }

            return ds;
        }
        //Author --> Chirag
        /// <summary>
        /// here we fetch data from frontend and send to controller.
        /// this api is use for binding Department of ddl
        /// </summary>

        public DataTable getDept_new(FeedbackFormEntity en)
        {
            DataTable ds = new DataTable();
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_Training_GetDept, en).Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(result.Trim());
                    if (response.IsSuccessStatusCode)
                    {
                        ds = data;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in FetDataFromAPI/getDept_new() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }

            return ds;
        }
        //Author -----> Jignesh
        //same listview show for department and module
        public static DataTable Get_Listview_Mis_Data(AdminEntity En)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = client.PostAsJsonAsync(Api_URL.subURL_MisDeptListviee, En).Result;
                    var data = Response.Content.ReadAsStringAsync().Result;

                    if (Response.IsSuccessStatusCode)
                    {
                        dt = (DataTable)JsonConvert.DeserializeObject(data, typeof(DataTable));
                    }
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("DeptGetDataListviewAPI : Get_Listview_Mis_Data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return dt;
        }
        //Author -----> Jignesh
        //same dropdown show for department and module method
        public static DataTable Get_Dropdown_Mis_Data(AdminEntity En)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = client.PostAsJsonAsync(Api_URL.subURL_Mis, En).Result;
                    var data = Response.Content.ReadAsStringAsync().Result;

                    if (Response.IsSuccessStatusCode)
                    {
                        dt = (DataTable)JsonConvert.DeserializeObject(data, typeof(DataTable));
                    }
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("MisGetDataFromAPI : Get_Dept_Mis_Data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return dt;
        }

        //Author -> Satyam
        /// <summary>
        /// BL API CALL Method For Get
        /// Employee Data With Email , Dept_Name , Joining DAte
        /// based On Factor
        public static DataTable get_Emp_Mail_DT()
        {
            DataTable ds_dep = new DataTable();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = client.PostAsJsonAsync(Api_URL.subURL_get_Emp_Mail_DT, "").Result;
                    var data = Response.Content.ReadAsStringAsync().Result;

                    if (Response.IsSuccessStatusCode)
                    {
                        ds_dep = (DataTable)JsonConvert.DeserializeObject(data, typeof(DataTable));
                    }
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("GetDataFromAPI : get_Emp_Mail_DT : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds_dep;
        }

        //author chirag
        /// <summary>
        /// here we fetch data in the form of Entity.
        /// and send entity to Controller.
        /// this api is use for send mail of FeedBack Form link to New trainee(employee).
        /// </summary>
        public static string SendMail(List<EmployeeEntity> obj_Send_Mail)
        {
            string msg = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.sendmail, obj_Send_Mail).Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    string data = JsonConvert.DeserializeObject<string>(result.Trim());
                    if (response.IsSuccessStatusCode)
                    {
                        msg = data;


                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in getDataFrom/SendMail() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return msg;
        }

        //Author -----> Jay Patel
        /// This method return all employee who submitted form data or not.
        public static DataSet Get_mis_formstatus_data(EmployeeEntity log_entity)
        {
            DataSet ds_dep = new DataSet();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = client.PostAsJsonAsync(Api_URL.subURL_Mis_form_status, log_entity).Result;
                    var data = Response.Content.ReadAsStringAsync().Result;

                    if (Response.IsSuccessStatusCode)
                    {
                        ds_dep = (DataSet)JsonConvert.DeserializeObject(data, typeof(DataSet));
                    }
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("GetDataFromAPI : Get_mis_formstatus_data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return ds_dep;
        }

      
        //author deeksha
        public static DataTable chk_log_exists(EmployeeEntity ent)
        {
            DataTable ds = new DataTable();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Api_URL.baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(Api_URL.subURL_FeedbackExist,ent).Result;
                    string str1 = response.Content.ReadAsStringAsync().Result;
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(str1);

                    if (response.IsSuccessStatusCode)
                    {
                        ds = dt;
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("ERROR in calling chk_log_exists(API)" + ex.Message + "Inner Exception" + ex.InnerException);
            }
            return ds;
        }

    }
}
