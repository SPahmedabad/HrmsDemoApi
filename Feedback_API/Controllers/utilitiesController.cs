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
    public class utilitiesController : ApiController
    {
        //Author --> Yaksh Maishery
        // Apply for Leave
        [Route("api/utilities/applyLeave")]
        [HttpPost]
        public HttpResponseMessage ApplyLeave(leave_entity EN)
        {
            DataSet DS = new DataSet();
            Response_entity res_en = new Response_entity();
            try
            {
                res_en.status = BL.Utilities_BL.ApplyLeave(EN);
                if (res_en.status == "success")
                {
                    res_en.message = "Apply Leave Successfull!";
                }
                else
                {
                    res_en.message = "Apply Leave Failed!";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> utilitiesController, method -> ApplyLeave\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_en);
        }

        //Author --> Yaksh Maishery
        // get Employee Leaves
        [Route("api/utilities/EmpLeave")]
        [HttpPost]
        public HttpResponseMessage EmpLeave(leave_entity EN)
        {
            //DataSet DS = new DataSet();
            Response_entity res_en = new Response_entity();
            try
            {
                DataTable DT = BL.Utilities_BL.EmpLeave(EN);
                if (DT.Rows.Count > 0)
                {
                    res_en.status = "success";
                    res_en.message = "Successfull Employee Leave";
                    res_en.ArrayOfResponse = DT;
                }
                else
                {
                    res_en.status = "failed";
                    res_en.message = "There is no Leave";
                }
                //DS.Tables.Add(DT);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> utilitiesController, method -> EmpLeave\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_en);
        }

        //Author --> Yaksh Maishery
        // get Employee Leaves
        [Route("api/utilities/SubordinateLeave")]
        [HttpPost]
        public HttpResponseMessage SubordinateLeave(leave_entity EN)
        {
            Response_entity res_en = new Response_entity();
            try
            {
                DataTable DT = BL.Utilities_BL.SubordinateLeave(EN);
                if (DT.Rows.Count > 0)
                {
                    res_en.status = "success";
                    res_en.message = "Successfull Subordinates Leave";
                    res_en.ArrayOfResponse = DT;
                }
                else
                {
                    res_en.status = "failed";
                    res_en.message = "There is no Subordinates Leave";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> utilitiesController, method -> SubordinateLeave\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_en);
        }

        //Author --> Yaksh Maishery
        // Update status leave
        [Route("api/utilities/updateLeave")]
        [HttpPost]
        public HttpResponseMessage UpdateLeave(leave_entity EN)
        {
            Response_entity res_en = new Response_entity();
            try
            {
                res_en.status = BL.Utilities_BL.UpdateLeave(EN);
                if (res_en.status == "success")
                {
                    res_en.message = "Update Leave Status Successfull!";
                }
                else
                {
                    res_en.message = "Update Leave Status Failed!";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> utilitiesController, method -> updateLeave\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_en);
        }

        //AUTHOR -----> DINESH GALANI 
        //GRIEVANCE FORM OPRATIONS
        [HttpPost]
        [Route("api/utilities/grievance_form")]
        public HttpResponseMessage grievance_form(GrievanceForm en)
        {

            DataTable dt = new DataTable();
            //create responce for api 
            Response_entity response = new Response_entity();
            try
            {
                Utilities_BL obj = new Utilities_BL();
                dt = obj.sp_grievance_form(en);
                response.ArrayOfResponse = dt;
                if (dt.Rows.Count > 0)
                {
                    response.status = "SUCCESS";
                    response.message = "DATA FOUND";
                }
                else
                {
                    response.status = "ERROR";
                    response.message = "DATA NOT FOUND";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Controller : utilities : in sp_grievance_form" + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //for binding ddl 
        //Author -> Deeksha
        [HttpPost]
        [Route("api/utilities/refer")]
        public HttpResponseMessage refer(Refer_Emp_Entity en)
        {
            Response_entity obj_response = new Response_entity();
            try
            {
                Utilities_BL obj = new Utilities_BL();
                DataTable dt = obj.refer_ddl(en);
                if (dt.Rows.Count > 0)
                {
                    obj_response.status = "success";
                    obj_response.message = "Dropdown binded";
                    obj_response.ArrayOfResponse = dt;
                }
                else
                {
                    obj_response.status = "failure";
                    obj_response.message = "Dropdown not binded";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Controller : utilities in refer(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj_response);
        }

        //for adding/uploading new references
        //Author -> Deeksha
        [HttpPost]
        [Route("api/utilities/set_refer")]
        public HttpResponseMessage set_refer()
        {
            Response_entity obj_response = new Response_entity();
            Refer_Emp_Entity en = new Refer_Emp_Entity();
            try
            {
                var request = HttpContext.Current.Request;
                string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.refer_cv + request.Form["name"] + "\\");
                string name = request.Form["name"];
                string fileName = request.Files["upload_cv"].FileName;
                int profile_id = Convert.ToInt32(request.Form["profile_id"]);
                double mob_no = Convert.ToDouble(request.Form["mob_no"]);
                int emp_id = Convert.ToInt32(request.Form["emp_id"]);
                    string cv_path = rootPath + fileName;

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                request.Files["upload_cv"].SaveAs(Path.Combine(rootPath, fileName));
                en.profile_id = profile_id;
                en.mob_no = mob_no;
                en.name = name;
                en.upload_cv = cv_path;
                en.emp_id = emp_id;

                Utilities_BL obj = new Utilities_BL();
                int result = obj.ins_refernce(en);
                if (result > 0)
                {
                    obj_response.status = "success";
                    obj_response.message = "Reference added successfully";
                }
                else
                {
                    obj_response.status = "failure";
                    obj_response.message = "Reference not added";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Controller : utilities in set_refer(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_response);
        }


        //Author -> Deeksha
        //for showing references to admin
        [HttpPost]
        [Route("api/utilities/show_references")]
        public HttpResponseMessage show_references(Refer_Emp_Entity en)
        {
            Response_entity obj_response = new Response_entity();
            try
            {
                Utilities_BL obj = new Utilities_BL();
                DataTable dt = obj.show_refer(en);

                if (dt.Rows.Count > 0)
                {
                    obj_response.status = "success";
                    obj_response.message = "Reference found";
                    obj_response.ArrayOfResponse = dt;
                }
                else
                {
                    obj_response.status = "failure";
                    obj_response.message = "Reference not founded";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Controller : utilities in show_references(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_response);
        }


        //deleting refernces from the table
        //Author -> Deeksha
        [HttpPost]
        [Route("api/utilities/del_references")]
        public HttpResponseMessage del_references(Refer_Emp_Entity en)
        {
            Response_entity obj_response = new Response_entity();
            try
            {
                Utilities_BL obj = new Utilities_BL();
                int result = obj.del_refer(en);
                if (result > 0)
                {
                    obj_response.status = "success";
                    obj_response.message = "Reference deleted successfully";
                }
                else
                {
                    obj_response.status = "failure";
                    obj_response.message = "Reference not deleted";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Controller : utilities in del_references(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_response);
        }


        //download file 
        //Author -> Deeksha
        [HttpPost]
        [Route("api/utilities/GetFile")]
        public HttpResponseMessage GetFile(Refer_Emp_Entity en)
        {
            byte[] bytes;
            DataTable dt = new DataTable();
            Response_entity obj_response = new Response_entity();
            try
            {
                string filePath = en.filePath;

                if (File.Exists(filePath))
                {
                bytes = File.ReadAllBytes(en.filePath);
                string file = Convert.ToBase64String(bytes);
               
                dt.Columns.Add("File");
                dt.Columns.Add("Type");
                string[] filetype = filePath.Split('.');

                DataRow dt_row = dt.NewRow();
                dt_row["File"] = file;
                dt_row["Type"] = filetype[1];

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
                InsertLog.WriteErrorLog("Controller : utilities in GetFile(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_response); 
        }

        //Author --> Jay Patel
        //this method is used to insert employee resignation data in database.
        [HttpPost]
        [Route("api/utilities/resign_emp")]
        public HttpResponseMessage resign_emp(Resignationentity en)
        {
            Response_entity res_obj = new Response_entity();
            int count = 0;
            try
            {
                count = Utilities_BL.resing_emp(en);
                if (count >= 1)
                {
                    leave_entity objen = new leave_entity();
                    objen.ID = en.ID;
                    DataTable dt = Utilities_BL.Emp_Heirarcy(objen);
                    if(dt.Rows.Count >0)
                    {
                        sendmaientityl objsend = new sendmaientityl();
                        objsend.subject = "Resign Application";
                        objsend.body = $"Resigned by {en.Employee_Name},<br>{en.reason_of_leave}";
                        Utilities_BL.sendMail(dt,objsend);
                        res_obj.status = "Success";
                        res_obj.message = "Inserted Successfull";
                    }
                   
                }
                else
                {
                    res_obj.status = "Failed";
                    res_obj.message = "Not Inserted";
                }
            }
            catch (Exception ex)
            {
                res_obj.status = "errror";
                res_obj.message = "Not Inserted error ";
                Library.InsertLog.WriteErrorLog("Dashboard : newempjoindata : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_obj);
        }

        //Author --> Jay Patel
        //this method is used to get all details whose resign.
        [HttpPost]
        [Route("api/utilities/resingemplist")]
        public HttpResponseMessage resingemplist(Resignationentity en)
        {
            Response_entity res_obj = new Response_entity();
            try
            {
                DataTable dt = BL.Utilities_BL.resingemplist(en);
                if (dt.Rows.Count > 0)
                {
                    res_obj.status = "success";
                    res_obj.message = "data found successfully";
                    res_obj.ArrayOfResponse = dt;
                }
                else
                {
                    res_obj.status = "success";
                    res_obj.message = "There is no Resgination";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("controller -> utilitiesController, method -> resingemplist:" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_obj);
        }

    }
}
