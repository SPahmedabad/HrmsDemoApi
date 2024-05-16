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
    public class ProfileController : ApiController
    {
        //Author -> Chirag Dhruv
        /// <summary>
        /// here we send trainee_id to FillForm method for fetch record from table.
        /// we get response in dataset and we pass to api.
        /// </summary>
        [HttpPost]
        [Route("api/Profile/getProfile")]
        public HttpResponseMessage getProfile(FeedbackFormEntity obj)
        {
            DataSet ds = new DataSet();
            try
            {
                Operation GetProfile = new Operation();
                ds = GetProfile.FillFormData(obj.Trainee_Id);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in Profile/getProfile...message:" + ex.Message + "StackTrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }



        [HttpPost]
        [Route("api/ProfileController/get_Api_User_Records")]
        public HttpResponseMessage get_Api_User_Records(UserDetails en)
        {
            DataSet ds_details = new DataSet();
            Api_Resoponse_Entity obj_resEntity = new Api_Resoponse_Entity();
            try
            {
                ds_details = Profile_BL.get_oper_User_Records(en);

                if (ds_details.Tables[0].Rows.Count > 0)
                {
                    string Emp_Image = "";
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~" + BL.Api_URL.empimgpath + ds_details.Tables[0].Rows[0]["Employee_Code"] + "/" + ds_details.Tables[0].Rows[0]["Image"].ToString());
                    if (File.Exists(path))
                    {
                        byte[] readText = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~" + BL.Api_URL.empimgpath + ds_details.Tables[0].Rows[0]["Employee_Code"] + "/" + ds_details.Tables[0].Rows[0]["Image"].ToString()));
                        Emp_Image = Convert.ToBase64String(readText);
                    }
                    else
                    {
                        Emp_Image = "";
                    }

                    ds_details.Tables[0].Rows[0]["Image"] = Emp_Image;
                    obj_resEntity.status = "Success";
                    obj_resEntity.message = "SuccessFull Get Data";
                    obj_resEntity.ArrayOfResponse = ds_details;
                }
                else
                {
                    obj_resEntity.status = "Failed";
                    obj_resEntity.message = "Error";
                }
            }
            catch (Exception ex)
            {
                obj_resEntity.status = "error";
                obj_resEntity.message = "Exception occured please check error log";
                InsertLog.WriteErrorLog("Error in get_Api_User_Records/get_Api_User_Records...message:" + ex.Message + "StackTrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_resEntity);

        }



        ///Author -->Satyam
        [HttpPost]
        [Route("api/ProfileController/update_Api_User_Records")]
        public HttpResponseMessage update_Api_User_Records(EmployeeMainData en)
        {
            DataTable ds = new DataTable();
            Response_entity obj_resEntity = new Response_entity();
            if (en.Image != "")
            {
                en.flag = "Insert";
                string[] file_extension = en.Image.Split('.');
                en.Image = en.Employee_Code + "." + file_extension[file_extension.Length - 1];

            }
            else if (en.Updated_Image != "")
            {
                en.Image = "";
                string[] file_extension = en.Updated_Image.Split('.');
                en.Image = en.Employee_Code + "." + file_extension[file_extension.Length - 1];
            }
            else
            {
                en.flag = "Update";
            }
            try
            {
                //en.DOB = DOB;
                //en.DOJ = DOJ;
                String msg = BL.Profile_BL.update_Oper_User_Records(en);
                if (msg == "Success")
                {
                    var request = HttpContext.Current.Request;
                    obj_resEntity.status = "Success";
                    obj_resEntity.message = "Data Updated";
                }
                else
                {
                    obj_resEntity.status = "Failed";
                    obj_resEntity.message = "Error";
                }
            }
            catch (Exception ex)
            {
                obj_resEntity.status = "error";
                obj_resEntity.message = "Exception occured please check error log";
                InsertLog.WriteErrorLog("Error in update_Api_User_Records/update_Api_User_Records...message:" + ex.Message + "StackTrace:" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj_resEntity);
        }





        ///Author -->Satyam
        [HttpPost]
        [Route("api/ProfileController/upload_Image_User")]
        public HttpResponseMessage upload_Image_User()
        {

            Response_entity response = new Response_entity();
            try
            {
                var request = HttpContext.Current.Request;

                //get image from request parameters
                string caption = request.Form["ID"];
                string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + BL.Api_URL.empimgpath) + caption;
                string fileName = request.Files["Image"].FileName;
                string FolderName = caption;
                string[] file_extension = fileName.Split('.');
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                request.Files["Image"].SaveAs(Path.Combine(rootPath, caption + "." + file_extension[file_extension.Length - 1]));

                int result = 1;

                if (result > 0)
                {
                    response.status = "Success";
                    response.message = "Data Added Succesfully";
                }
                else
                {
                    response.status = "Failed";
                    response.message = "Data Not Added Succesfully";
                }

            }
            catch (Exception ex)
            {
                response.status = "error";
                response.message = "Exception occured please check error log";
                InsertLog.WriteErrorLog("Error in upload_Image_User/upload_Image_User...message:" + ex.Message + "StackTrace:" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }

}

