using BL;
using entity;
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
    public class AdminController : ApiController
    {
        Response_entity res_obj = new Response_entity();
        //Author : Dinesh Galani
        //method for opration on table important notice
        [HttpPost]
        [Route("api/Admin/sp_imp_notice")]
        public HttpResponseMessage imp_notice(ImportantNoticeEntity gen_obj_ent)
        {
            Response_entity response = new Response_entity();
            try
            {
                Admin_BL gen_obj_sp = new Admin_BL();
                DataTable dt = gen_obj_sp.sp_imp_notice(gen_obj_ent);
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
                InsertLog.WriteErrorLog("Controller : utilities : in imp_notice" + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        //method for admin to insert events
        //Author- Deeksha
        [HttpPost]
        [Route("api/Admin/ins_event")]
        public HttpResponseMessage ins_event(CalEvent_entity en)
        {
            Response_entity res_obj = new Response_entity();

            Admin_BL obj = new Admin_BL();
            int result = obj.insertevent(en);
            if (result > 0)
            {
                res_obj.status = "success";
                res_obj.message = "Event added successfully";
            }
            else
            {
                res_obj.status = "failure";
                res_obj.message = "Event not added";
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_obj);
        }

        /// Author -> Saddam Shaikh
        [HttpPost]
        [Route("api/Admin/add_emp_of_month")]
        public HttpResponseMessage add_emp_of_month()
        {
            DataTable dt = new DataTable();
            Response_entity res_entity = new Response_entity();

            try
            {
                var request = HttpContext.Current.Request;
                Emp_Of_Month_Entity entity = new Emp_Of_Month_Entity();

                //get image from request parameters
                string imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.img_path);
                string fileName = request.Files["img_path"].FileName;
                string emp_name = request.Form["emp_name"];
                string dept_name = request.Form["dept_name"];
                string title = request.Form["title"];
                string designation = request.Form["designation"];
                DateTime eom_date = DateTime.ParseExact(request.Form["eom_date"], "dd/MM/yyyy", null);

                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }


                request.Files["img_path"].SaveAs(Path.Combine(imgPath, fileName));

                entity.title = title;
                entity.emp_name = emp_name;
                entity.dept_name = dept_name;
                entity.designation = designation;
                entity.img_path = fileName;
                entity.eom_date = eom_date;

                int result = Admin_BL.addEmpOfMon(entity);

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
                InsertLog.WriteErrorLog("Error in AdminController -> add_emp_of_month() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, res_entity);
        }

        /// Author -> Saddam Shaikh
        [HttpPost]
        [Route("api/Admin/remove_emp_of_month")]
        public HttpResponseMessage remove_emp_of_month(Emp_Of_Month_Entity entity)
        {
            int rowsDeleted = 0;
            Response_entity res_entity = new Response_entity();
            try
            {
                rowsDeleted = Admin_BL.removeEmpOfMon(entity);
                if (rowsDeleted > 0)
                {
                    res_entity.message = "Data Removed Successfully !!!";
                    res_entity.status = "success";
                }
                else
                {
                    res_entity.message = "Data not removed !!!";
                    res_entity.status = "error";
                }

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in AdminController -> remove_emp_of_month() : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, res_entity);
        }

        //Author --> Jignesh Panchal
        ///ins_vacancy_data() :- This controller for insert vacancy data
        [Route("api/Admin/ins_vacancy_data")]
        public HttpResponseMessage ins_vacancy_data(VacancyEntity vac_obj)
        {
            Response_entity res = new Response_entity();
            try
            {
                Admin_BL db = new Admin_BL();
                int data = db.InsertVacancyData(vac_obj);

                if (data > 0)
                {
                    res.status = "success";
                    res.message = "Data Inserted Successfully";
                }
                else
                {
                    res.status = "error";
                    res.message = "Data Not Inserted";
                }

            }

            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived In Admincontroller: InsertVacancyData() Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);

        }

        //Author --> Jignesh Panchal
        ///show_vacancy_data() :- This controller for admin can show vacancy data
        [Route("api/Admin/show_vacancy_data")]
        public HttpResponseMessage show_vacancy_data(VacancyEntity vac_obj)
        {
            Response_entity res = new Response_entity();
            try
            {
                DataTable dt = new DataTable();
                Admin_BL db = new Admin_BL();
                dt = db.ShowVacancyData(vac_obj);

                if (dt.Rows.Count > 0)
                {
                    res.status = "success";
                    res.message = "Data Show";
                    res.ArrayOfResponse = dt;
                }
                else
                {
                    res.status = "error";
                    res.message = "Data Not Display";
                }

            }

            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived In Admincontroller: show_vacancy_data()  Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);

        }

        //Author --> Jignesh Panchal
        ///inactive_vacancy_data() :- This controller for admin can inactive vacancy data
        [Route("api/Admin/inactive_vacancy_data")]
        public HttpResponseMessage inactive_vacancy_data(VacancyEntity vac_obj)
        {
            Response_entity res = new Response_entity();
            try
            {
                Admin_BL db = new Admin_BL();
                int data = db.InactiveVacancyData(vac_obj);

                if (data > 0)
                {
                    res.status = "success";
                    res.message = "Data Inactive Successfully";
                }
                else
                {
                    res.status = "error";
                    res.message = "Data Not Inactive";
                }

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived In Admincontroller: inactive_vacancy_data() Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);

        }

        //Author --> Jignesh Panchal
        ///active_vacancy_data() :- This controller for admin can active vacancy data
        [Route("api/Admin/active_vacancy_data")]
        public HttpResponseMessage active_vacancy_data(VacancyEntity vac_obj)
        {
            Response_entity res = new Response_entity();
            try
            {
                Admin_BL db = new Admin_BL();
                int data = db.activeVacancyData(vac_obj);

                if (data > 0)
                {
                    res.status = "success";
                    res.message = "Data Activated Successfully";
                }
                else
                {
                    res.status = "error";
                    res.message = "Data Not Activated";
                }

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived In Admincontroller:  active_vacancy_data() Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);

        }

        //Author --> Jignesh Panchal
        ///delete_vacancy_data() :- This controller for admin can delete vacancy data
        [HttpPost]
        [Route("api/Admin/delete_vacancy_data")]
        public HttpResponseMessage delete_vacancy_data(VacancyEntity vac_obj)
        {
            Response_entity res = new Response_entity();
            try
            {
                Admin_BL db = new Admin_BL();
                int data = db.DeleteVacancyData(vac_obj);

                if (data > 0)
                {
                    res.status = "success";
                    res.message = "Data Deleted Successfully";
                }
                else
                {
                    res.status = "error";
                    res.message = "Data Not Deleted";
                }

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error Arrived In Admincontroller : delete_vacancy_data() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);

        }

        //Author:Ravi Vaghela
        //This method is for add new images of event with caption
        [Route("api/Admin/add_event_gallery")]
        [HttpPost]
        public HttpResponseMessage add_event_gallery()
        {
            Admin_BL dashboard = new Admin_BL();
            Response_entity response = new Response_entity();
            try
            {
                var request = HttpContext.Current.Request;
                Event_Gallery_Entity event_obj = new Event_Gallery_Entity();

                //get image from request parameters
                string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.event_images);
                string fileName = request.Files["event_image"].FileName;
                string caption = request.Form["caption"];

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                if (File.Exists(Path.Combine(rootPath, fileName)))
                {
                    //fileName = fileName + "1";
                    string[] tokens = fileName.Split('.');
                    fileName = tokens[0] + "1" + tokens[1];
                }
                request.Files["event_image"].SaveAs(Path.Combine(rootPath, fileName));

                event_obj.event_image = fileName;
                event_obj.caption = caption;
                event_obj.flag = "add";

                int result = dashboard.add_event_gallery(event_obj);

                if (result > 0)
                {
                    response.status = "success";
                    response.message = "Data Added Succesfully";
                }
                else
                {
                    response.status = "error";
                    response.message = "Data Not Added Succesfully";
                }

            }
            catch (Exception exe)
            {
                response.status = "error";
                response.message = "Exception occured please check error log";
                InsertLog.WriteErrorLog("Error in Dashboard/add_event_gallery() : Message:" + exe.Message + "stacktrace:" + exe.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Author:Ravi Vaghela
        //this method is for delete data of uploded event_image from storage and database
        [Route("api/Admin/delete_event_gallery")]
        [HttpPost]
        public HttpResponseMessage delete_event_gallery(Event_Gallery_Entity obj)
        {
            //get single event_gallery data for delete
            Dashboard_BL dashboard = new Dashboard_BL();
            Admin_BL admin_dashboard = new Admin_BL();
            Response_entity response = new Response_entity();
            try
            {
                DataTable responseData = dashboard.get_event_gallery(obj);
                //delete file from storage 
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.event_images + "/" + responseData.Rows[0]["event_image"].ToString());
                File.Delete(filePath);
                if (!File.Exists(filePath))
                {
                    obj.flag = "delete";
                    int result = admin_dashboard.delete_event_gallery(obj);
                    if (result > 0)
                    {
                        response.status = "success";
                        response.message = "Data Deleted Successfully";
                    }
                    else
                    {
                        response.status = "error";
                        response.message = "Data Not Deleted";
                    }
                }
            }
            catch (Exception exe)
            {
                response.status = "error";
                response.message = "Exception occured please check error log";
                InsertLog.WriteErrorLog("Error in Dashboard/delete_event_gallery() : Message:" + exe.Message + "stacktrace:" + exe.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Author:Adarsh 
        //Admin Mapping
        [Route("api/Admin/select_Report")]
        [HttpPost]
        public HttpResponseMessage select_Report()
        {
            Response_entity resp = new Response_entity();
            DataTable dt = new DataTable();
            try
            {
                Admin_BL bl = new Admin_BL();
                dt = bl.Select_MapReporter();
                if (dt.Rows.Count > 0)
                {
                    resp.status = "success";
                    resp.message = "Binded Successfully";
                    resp.ArrayOfResponse = dt;
                }
                else
                {
                    resp.status = "Failed";
                    resp.message = "Binded Failed";
                    resp.ArrayOfResponse = dt;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in AdminController in  select_Report(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace + "innerException");
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        //Author:Adarsh 
        //Admin Mapping
        [Route("api/Admin/get_mappingdata")]
        [HttpPost]
        public HttpResponseMessage get_mappingdata()
        {
            Response_entity resp = new Response_entity();
            DataTable dt = new DataTable();
            try
            {
                Admin_BL bl = new Admin_BL();
                dt = bl.get_Mapping_data();
                if (dt.Rows.Count > 0)
                {
                    resp.status = "success";
                    resp.message = "Successfully Get Data";
                    resp.ArrayOfResponse = dt;
                }
                else
                {
                    resp.status = "Failed";
                    resp.message = "Data Not Found";
                    resp.ArrayOfResponse = dt;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in AdminController in get_mappingdata(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        //Author:Adarsh 
        //Admin Mapping
        [Route("api/Admin/updating_mappingdata")]
        [HttpPost]
        public HttpResponseMessage updating_mappingdata(leave_entity mre)
        {
            int result = 0;
            Response_entity resp = new Response_entity();
            try
            {
                Admin_BL bl = new Admin_BL();
                result = bl.Update_Mapping_Reporting(mre);
                if (result > 0)
                {
                    resp.status = "success";
                    resp.message = "Successfully Updated";
                }
                else
                {
                    resp.status = "Failed";
                    resp.message = "update Failed";

                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in AdminController in updating_mappingdata(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        //Author:Adarsh 
        //Monthly leave report admin view 
        [Route("api/Admin/Monthly_LeaveReport")]
        [HttpPost]
        public HttpResponseMessage Monthly_LeaveReport(leave_entity lr)
        {
            Response_entity resp = new Response_entity();
            DataTable dt = new DataTable();
            try
            {
                Admin_BL bl = new Admin_BL();
                dt = bl.Monthly_Report(lr);
                if (dt.Rows.Count > 0)
                {
                    resp.status = "success";
                    resp.message = "Successfully Get Data";
                    resp.ArrayOfResponse = dt;
                }
                else
                {
                    resp.status = "Failed";
                    resp.message = "Data Not Found";
                    resp.ArrayOfResponse = dt;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in AdminController in Monthly_Report(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace + "InnerException" + ex.InnerException);
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        //Author:Adarsh 
        //Monthly leave report admin view 
        [Route("api/Admin/get_Leave_emp_name")]
        [HttpPost]
        public HttpResponseMessage get_Leave_emp_name(leave_entity lr)
        {
            Response_entity resp = new Response_entity();
            DataTable dt = new DataTable();
            try
            {
                Admin_BL bl = new Admin_BL();
                dt = bl.get_Leave_Emp_name();
                if (dt.Rows.Count > 0)
                {
                    resp.status = "success";
                    resp.message = "Successfully Get Data";
                    resp.ArrayOfResponse = dt;
                }
                else
                {
                    resp.status = "Failed";
                    resp.message = "Data Not Found";
                    resp.ArrayOfResponse = dt;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Admincontroller in get_Leave_emp_name(): Message:" + ex.Message + "stacktrace:" + ex.StackTrace + "InnerException" + ex.InnerException);
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        //method for admin to delete events
        //Author- Deeksha
        [HttpPost]
        [Route("api/Admin/del_event")]
        public HttpResponseMessage del_event(CalEvent_entity en)
        {
            Response_entity res_obj = new Response_entity();
            Admin_BL obj = new Admin_BL();
            int result = obj.delevent(en);
            if (result > 0)
            {
                res_obj.status = "success";
                res_obj.message = "Event deleted successfully";
            }
            else
            {
                res_obj.status = "failure";
                res_obj.message = "Event not deleted";
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_obj);
        }

        //Author -> chirag
        //for uploading new Document
        [HttpPost]
        [Route("api/Admin/uploaddoc")]
        public HttpResponseMessage uploaddoc()
        {
            Response_entity obj_response = new Response_entity();
            try
            {
                var request = HttpContext.Current.Request;
                string emp_name = request.Form["emp_name"];
                string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + Api_URL.uploaddocpath +"/"+ emp_name.ToString());
                string doc_type_id = request.Form["doctype_id"].ToString();
                string fileName = request.Files["filepath"].FileName.ToString();
                int emp_id = Convert.ToInt32(request.Form["emp_id"]);
                string doc_path = rootPath+"\\" + fileName;
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                request.Files["filepath"].SaveAs(Path.Combine(rootPath, fileName));

                Document obj_doc = new Document();
                obj_doc.emp_id = emp_id;
                obj_doc.doc_type_id = doc_type_id;
                obj_doc.filepath = doc_path;
                Admin_BL obj = new Admin_BL();
                int result = obj.ins_document(obj_doc);
                if (result > 0)
                {
                    obj_response.status = "success";
                    obj_response.message = "success";
                }
                else
                {
                    obj_response.status = "failure";
                    obj_response.message = "Reference not added";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Controller : AdminController in uploaddoc(): " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_response);
        }
    }
}
