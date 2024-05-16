
using BL;
using Entity;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Feedback_API.Controllers
{
    public class ReportsController : ApiController
    {
        //Author --> Hemangini && chirag
        //method to get employee name from main datatable
        [HttpPost]
        [Route("api/Reports/getEmpdata")]
        public HttpResponseMessage getEmpdata(UserDetails obj_id)
        {
           Response_entity obj_data = new Response_entity();
            DataTable data = new DataTable();
            try
            {
                Reports_BL obj = new Reports_BL();
                data = obj.EmpInfo();

                if (data.Rows.Count > 0)
                {
                    obj_data.status = "Success";
                    obj_data.message = "Data Found";
                    obj_data.ArrayOfResponse = data;
                }
                else
                {
                    obj_data.status = "Failure";
                    obj_data.message = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in ReportsController/getEmpdata() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_data);
        }

        //Author --> Hemangini && chirag
        //method to get employee contact data from main datatable
        [HttpPost]
        [Route("api/Reports/EmpContactDetails")]
        public HttpResponseMessage EmpContactDetails(UserDetails obj_id)
        {
            Response_entity obj_data = new Response_entity();
            DataTable data = new DataTable();
            try
            {
                Reports_BL obj = new Reports_BL();
                data = obj.EmpContactDetails(obj_id);

                if (data.Rows.Count > 0)
                {
                    obj_data.status = "Success";
                    obj_data.message = "Data Found";
                    obj_data.ArrayOfResponse = data;
                }
                else
                {
                    obj_data.status = "Failure";
                    obj_data.message = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in ReportsController/EmpContactDetails() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_data);
        }

        //Author --> Hemangini
        //method to get employee on_leave data 
        //get data of employee who have  only approved status
        [HttpPost]
        [Route("api/Reports/EmpLeavedata")]
        public HttpResponseMessage EmpLeavedata()
        {
            Response_entity obj_data = new Response_entity();
            DataTable data = new DataTable();
            try
            {
                Reports_BL obj = new Reports_BL();
                data = obj.GetEmpLeaveData();


                if (data.Rows.Count > 0)
                {
                    obj_data.status = "Success";
                    obj_data.message = "Data Found";
                    obj_data.ArrayOfResponse = data;
                }
                else
                {
                    obj_data.status = "Failure";
                    obj_data.message = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in ReportsController/EmpLeavedata() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_data);
        }


        /// Author -> Saddam Shaikh  && Satyam 
        /// Upload daily task sheet and its details
        [HttpPost]
        [Route("api/Reports/add_daily_task_sheet")]
        public HttpResponseMessage add_daily_task_sheet()
        {
            DataTable dt = new DataTable();
            Response_entity res_entity = new Response_entity();

            try
            {
                var request = HttpContext.Current.Request;
                Daily_Task_Entity entity = new Daily_Task_Entity();

                //get image from request parameters
                string storedFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.file_path + "\\" + request.Form["emp_id"] + "_" + request.Form["emp_name"]);
                int emp_id = Convert.ToInt32(request.Form["emp_id"]);
                string emp_name = request.Form["emp_name"].ToString();
                string file_Name = request.Files["file_path"].FileName;
                string[] file_extension = file_Name.Split('.');

                if (!Directory.Exists(storedFilePath))
                {
                    Directory.CreateDirectory(storedFilePath);
                }

                string fileName = emp_name + "_" + DateTime.Now.ToString("MM-dd-yyyy") + "_Tasksheet_" + DateTime.Now.ToString("HH-mmtt") + "." + file_extension[file_extension.Length - 1];
                fileName = fileName.Trim();
                request.Files["file_path"].SaveAs(Path.Combine(storedFilePath, fileName));

                entity.emp_id = emp_id;
                entity.emp_name = emp_name;
                entity.file_path = fileName;

                int result = Reports_BL.addDailyTaskSheet(entity);

                if (result > 0)
                {
                    res_entity.message = "Data Added Successfully !!!";
                    res_entity.status = "success";
                }
                else
                {
                    res_entity.message = "Data not added !!!";
                    res_entity.status = "error";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in ReportsController -> add_daily_task_sheet() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, res_entity);
        }


        /// Author -> Saddam Shaikh && Satyam 
        /// Get daily task sheet details
        [HttpPost]
        [Route("api/Reports/get_daily_task_sheet")]
        public HttpResponseMessage get_daily_task_sheet(Daily_Task_Entity entity)
        {
            DataTable dt = new DataTable();
            Response_entity response = new Response_entity();

            try
            {
                DataTable dt_emp = Reports_BL.getDailyTaskSheet(entity);

                DataTable datawithfile = new DataTable();

                datawithfile.Columns.Add("id");
                datawithfile.Columns.Add("emp_id");
                datawithfile.Columns.Add("emp_name");
                datawithfile.Columns.Add("file_path");
                datawithfile.Columns.Add("uploaded_date");
                datawithfile.Columns.Add("file_type");

                datawithfile.Clear();
                foreach (DataRow row in dt_emp.Rows)
                {
                    DataRow row_data = datawithfile.NewRow();

                    var file = System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.file_path + "/" + row["file_path"].ToString());

                    row_data["emp_id"] = row["emp_id"].ToString();
                    row_data["emp_name"] = row["emp_name"].ToString();
                    if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.file_path + "/" + row["emp_id"] + "_" + row["emp_name"] + "/" + row["file_path"].ToString())))
                    {
                        byte[] readText = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.file_path + "/" + row["emp_id"] + "_" + row["emp_name"] + "/" + row["file_path"].ToString()));
                        row_data["file_path"] = Convert.ToBase64String(readText);
                    }
                    else
                    {
                        row_data["file_path"] = "";
                    }
                    string[] types = row["file_path"].ToString().Split('.');
                    row_data["file_type"] = types[types.Length - 1];
                    row_data["uploaded_date"] = Convert.ToDateTime(row["uploaded_date"]);

                    datawithfile.Rows.Add(row_data);
                }


                if (dt_emp.Rows.Count > 0)
                {
                    response.status = "success";
                    response.message = "Data Found";
                    response.ArrayOfResponse = datawithfile;
                }
                else
                {
                    response.status = "error";
                    response.message = "Data Not Found";
                    response.ArrayOfResponse = datawithfile;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in ReportsController -> add_daily_task_sheet() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// Author -> Saddam Shaikh  && Satyam 
        /// Get Details about employee
        [HttpPost]
        [Route("api/Reports/get_emp_details")]
        public HttpResponseMessage get_emp_details(Daily_Task_Entity entity)
        {
            DataTable dt = new DataTable();
            Response_entity response = new Response_entity();
            try
            {
                dt = Reports_BL.getEmpDetails();

                if (dt.Rows.Count > 0)
                {
                    response.status = "success";
                    response.message = "Data Found";
                    response.ArrayOfResponse = dt;
                }
                else
                {
                    response.status = "error";
                    response.message = "Data Not Found";
                    response.ArrayOfResponse = dt;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in ReportsController -> get_emp_details() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Author --> Satyam
        // Get Employee Tasksheet On Based On Selected Date
        [HttpPost]
        [Route("api/Reports/get_date_task_sheet")]
        public HttpResponseMessage get_date_task_sheet(Daily_Task_Entity en)
        {
            DataTable dt = new DataTable();
            Response_entity response = new Response_entity();
            try
            {
                dt = Reports_BL.get_EmpTasksheet_Date(en);
                DataTable datawithfile = new DataTable();

                datawithfile.Columns.Add("id");
                datawithfile.Columns.Add("emp_id");
                datawithfile.Columns.Add("emp_name");
                datawithfile.Columns.Add("file_path");
                datawithfile.Columns.Add("uploaded_date");
                datawithfile.Columns.Add("file_type");

                datawithfile.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    DataRow row_data = datawithfile.NewRow();

                    var file = System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.file_path + "/" + row["file_path"].ToString());
                    row_data["emp_id"] = row["id"].ToString();
                    row_data["emp_id"] = row["emp_id"].ToString();
                    row_data["emp_name"] = row["emp_name"].ToString();
                    if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.file_path + "/" + row["emp_id"] + "_" + row["emp_name"] + "/" + row["file_path"].ToString())))
                    {
                        byte[] readText = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.file_path + "/" + row["emp_id"] + "_" + row["emp_name"] + "/" + row["file_path"].ToString()));
                        row_data["file_path"] = Convert.ToBase64String(readText);
                    }
                    else
                    {
                        row_data["file_path"] = "";
                    }
                    string[] types = row["file_path"].ToString().Split('.');
                    row_data["file_type"] = types[types.Length - 1];
                    row_data["uploaded_date"] = Convert.ToDateTime(row["uploaded_date"]);

                    datawithfile.Rows.Add(row_data);
                }
                if (datawithfile.Rows.Count > 0)
                {
                    response.status = "success";
                    response.message = "Data Found";
                    response.ArrayOfResponse = dt;
                }
                else
                {
                    response.status = "error";
                    response.message = "Data Not Found";
                    response.ArrayOfResponse = dt;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in ReportsController -> get_date_task_sheet() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // Author -> Yaksh Maishery
        // Fetch Data from the tbl_attend_table
        [Route("api/Reports/getAttends")]
        [HttpPost]
        public HttpResponseMessage getAttends(attend_entity en)
        {
            Response_entity res_en = new Response_entity();
            try
            {
                res_en.ArrayOfResponse = Reports_BL.getAttends(en);
                if (res_en.ArrayOfResponse.Rows.Count > 0)
                {
                    res_en.status = "success";
                    res_en.message = "There is some data";
                }
                else
                {
                    res_en.status = "failed";
                    res_en.message = "There is no data";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> AttendReportsController, method -> getAttends\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_en);
        }

        // Author -> chirag dhruv
        // show document of employee
        [HttpPost]
        [Route("api/Reports/ShowEmpDoc")]
        public HttpResponseMessage ShowEmpDoc(Document en) 
        {
            Response_entity res_en = new Response_entity();
            try
            {
                Reports_BL obj = new Reports_BL();
                DataTable dt = obj.ShowEmpDoc(en);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string filePath = Convert.ToString(dt.Rows[0]["filepath"]);
                        char[] spearator = { '\\'};
                        string[] path = filePath.Split(spearator);
                        string filename = path[9];
                        if (File.Exists(filePath))
                        {
                            byte[] readText = File.ReadAllBytes(filePath);
                            row["base64string"] = Convert.ToBase64String(readText);
                            row["fileName"] = filename;
                        }
                        else
                        {
                            row["base64string"] = "";
                        }
                    }
                        res_en.ArrayOfResponse = dt;
                        res_en.status = "success";
                        res_en.message = "Documents Data Show";
                }
                else
                {
                    res_en.status = "failed";
                    res_en.message = "Documents Data Not Display";
                }

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("controller => ReportController , method -> ShowEmpDoc()\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, res_en);

        }

        // Author -> chirag dhruv
        // Download document of employee
        [HttpPost]
        [Route("api/Reports/DownloadDoc")]
        public HttpResponseMessage DownloadDoc(Document en)
        {
            byte[] bytes;
            DataTable dt = new DataTable();
            Response_entity obj_response = new Response_entity();
            try
            {
                string filePath = en.filepath;
                char[] spearator = { '\\' };
                string[] path = filePath.Split(spearator);
                string filename = path[9];
                if (File.Exists(filePath))
                {
                    bytes = File.ReadAllBytes(en.filepath);
                    string file = Convert.ToBase64String(bytes);

                    dt.Columns.Add("File");
                    dt.Columns.Add("Type");
                    dt.Columns.Add("fileName");
                    string[] filetype = filePath.Split('.');

                    DataRow dt_row = dt.NewRow();
                    dt_row["File"] = file;
                    dt_row["Type"] = filetype[1];
                    dt_row["fileName"] = filename;
                    dt.Rows.Add(dt_row);
                }


                if (dt.Rows.Count > 0)
                {
                    obj_response.status = "success";
                    obj_response.message = "File found";
                    obj_response.ArrayOfResponse = dt;
                }
                else
                {
                    obj_response.status = "failure";
                    obj_response.message = "File not found";
                    obj_response.ArrayOfResponse = dt;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Controller : utilities in DownloadDoc(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_response);
        }

        // Author -> chirag dhruv
        // bind document dropdown list
        [HttpPost]
        [Route("api/Reports/bindDocType")]
        public HttpResponseMessage bindDocType()
        {
            Response_entity obj_data = new Response_entity();
            DataTable data = new DataTable();
            try
            {
                Reports_BL obj = new Reports_BL();
                data = obj.binddoctype();
                if (data.Rows.Count > 0)
                {
                    obj_data.status = "Success";
                    obj_data.message = "Data Found";
                    obj_data.ArrayOfResponse = data;
                }
                else
                {
                    obj_data.status = "Failure";
                    obj_data.message = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in ReportsController/bindDocType() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_data);
        }
    }
}
