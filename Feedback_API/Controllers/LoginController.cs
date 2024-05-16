using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL;
using Entity;
using Library;
using System.Data;
using System.IO;

namespace Feedback_API.Controllers
{
    public class LoginController : ApiController
    {
      
        //author => Chirag Dhruv
        /// <summary>
        /// here we fetch data from loginPage.
        /// and pass data to LoginHRMS/UserLogin().
        /// we get response in datatable and pass it.
        /// here we fetch all data of user.
        /// </summary>
        [HttpPost]
        [Route("api/Login/LoginHrms")]
        public HttpResponseMessage LoginHrms(UserEntity obj_data)
        {
            Response_entity obj_res = new Response_entity();
            DataTable dt = new DataTable();
            try
            {
                LoginHRMS obj_empLogin = new LoginHRMS();
                dt = obj_empLogin.UserLogin(obj_data);
                if (dt.Rows.Count > 0)
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~" + BL.Api_URL.empimgpath + dt.Rows[0]["Employee_Code"] + "/" + dt.Rows[0]["Image"].ToString());
                    string Emp_Image = "";
                    string notFoundImage = "iVBORw0KGgoAAAANSUhEUgAAAV4AAAEUCAMAAABUNq4iAAAC1lBMVEXm5uYAAAD/tgD/lgD/lwD/lgD/lwD/lwD/lgD/lgD/lgD/lwD/mAD/mwD/vwD/lgD/lwD/lgD/mQD/lwD/lwD/mgD/lwD/lwD/qgD/lwD//wD/lgD/nQD/lgD/lgD/lwD/lwD/lwD/lwD/lgD/lgD/lwD/lgD/lwD/lwD/lgD/lwD/qgD/lgD/lwD/mQD/qgD/lgD/nQD/mAD/lwD/mgD/lwD/ogD/oQD/lwD/lwD/mQD/lwD/lgD/mQD/mAD/lwD/lgD/mAD/lgD/mAD/lgD/lwD/lwD/lwD/lwD/pAD/lgD/lwD/lgD/lgD/lwD/lwD/mAD/lwD/lgD/mQD/lwD/lwD/mQD/mQD/nwD/lgD/lwD/lgD/mgD/lgD/lwD/lwD/lgD/nwD/ngD/lgD/lwD/lgD/lwD/lgD//wD/lgD/lgD/mAD/mQD/qgD/lgD/mwD/mAD/mgD/mQD/lwD/lgD/lwD/mQD/lwD/lwD/mAD/lgD/nAD/lwD/mwD/lwD/mAD/lgD/lgD/lgD/lwD/lwD/mQD/lgD/lwD/lwD/lwD/lwD/lgD/mQD/mAD/lwD/nAD/lgD/lwD/mAD/mwD/lwD/lwD/mAD/lgD/lwD/lgD/lgD/lwD/lwD/mgD/ngD/mAD/mQD/mAD/mQD/lwD/mAD/lgD/lwD/mAD/lwD/lwD/lwD/lgD/lwD/lwD/lwD/lwD/mgD/lgD/lgD/lwD/mAD/lwD/lwD/lgD/lwD/lgD/lwD/mAD/lwD/lgD/lwD/lwD/mAD/mAD/lwD/lwD/lgD/lwD/mAD/lgD/lgD/lgD/lgD/lgD/mAD/nwD/lwD/lwD/lwD/lwD/lwD/lgD/lwD/lwD/lwD/mwD/lwD/lwD/lgD/mAD/lwD/lgD/lgD/lgD/lwD/lgD/mAD/lwD/lwD/lwD/mAD/nAD/lwD/mAD/lgD/lwD/mQD/mAD/lwD/lgD/lgD/lwB92HTLAAAA8nRSTlP/AAdJmMzm/P/23cF+LgRmwjgezpM16bwMsAF8GpfuQnaQ8qqSldH+pvt4CTPJQQa7DXmpMJELE9BHMrXIBWG68y/UTREbYOGsDrKATvH5iUhA7yjY4y0jEFWF6j/ibn+hCB09W/1dcAKGz1dGA4spKitQnbRYN8vVWWQSFhezXvjZJ72OD0Sf64QgIjxSViSZZXQcZ1NPqKJ6vr+aJhU0CiUU1z7l32P3oO3gUZbk0zrFt/VqjNp1Yq9sVMaN3MSDXHM7rWlo3sD0o9tvGDGCNky41qul8CH6p1+Ksbl3WnvDOejHLG0ffZTnzUtKpOyeh1ZAATMAAAkrSURBVHgB7NDFAYIAAABAW7rB3n9NJ6B56d0ItwOAX7Rnc3r16kWvXr3o1asXvXr16tWrF7169aJXr1706tWLXr3o1asXvXr1olevXvTq1atXr1706tWLXr160atXL3r1olevXvTq1YtevXrRq1evXr160atXL3r16kWvXr38U+/heDpfrsFUYRQnqd6JsvwSzFeUeqeo6mCZJtM7qu2CpW53vSOqLlju8dQ7KKuDNV56B+XBKuFb74DDJVjno3fAlz17YO4jiMM4Pr/wGdS2G9u2bdtG3bB2+5LDa3Yuk70/djPK830Jn7t1KAy7f568+sJg2gPy6guHaQ/Jqy8Cpj0ir75IuHv85Omz51EIoGjy6oOrmNg42Ss+Af73RPyLvDGJclhSMnnt88bK/1Kuk9c27+M4OSqVvLZ500SVTl7bvBmiyiSvbd5nosqyzUvebFHl2OYlb1SuHJVnm5e8yC8Qp0JY5yUvilIc3WLyWgnurqeWZGbl5AHktRICqbSsvKKy6nR4yVsdInvV1J4GL3mrz8tBt+uiyOtngek61TeY8ZK3sam5JbXVratqazfhJW9Hp+x1N/xkXTUBk9c7jW6XHNQdfrKumoDJ65leV/m6dd0TMHk90+o6lYdrdEW6eg66RF59Wl31/7p0y8Uo8ipdx7dX6UpvX78YRN6BLtHXC8QMktf/huqO5UN3r+ER8pqm1wXCR8lr3Ni4RhcwnoDJO1Y8ManRBWKmpslrpgu4fGfgajZ1rof7XhNdl28vXyvs6rp8e0Fea80Xw2niguy1sAjyBtXS8vFWVl9EQfXy1evCN/Aojbz67sO0MPLqewvThsir7xZMe0defe9hWMwaefWtx8CsZCGvR89gVj95vdp4DJM2hbyerUYh+KrWyeujhwi61nQhr6+2thFcHz4KeX2X/glBtP35i5DXr75++47AKv3xU4S8/rbw6/efv/5WFr/B14qzEHnJS15GXvKSl5GXvORl5LUfecnLyEte8jLykpe8jLzkJS8jL3kZeclLXkZe8pKXkZe85GX/pqamunbZtQcgOfIFjuP1i+a7mtuKVXHS2bNj27Y9Z3N5iG0s3xpn27ZtG2XX6+7/6+XM7F0482q/hbY+Y0QIb/KOHTt26qi3y97t7qrRZf90G689Ovz2AvsihLc5sF9HvUTggNzigLNUt20HD66ss43XWNVTA6/VPyxvGhxq4D18XjL/PW+z/5X1/8mb3VxOZ5y6KSAnM5V9bY6qdUqumawsNTcvGC8Ta/P6Z/+nfVjeQapZfqtxCttZrfqoqsDs5pHJu6ygoKBwcREUlygnLQZKyzrKrX+7VUD5hLYyXdwiCSom3mZv4F5Z29vvAN+dg4PwzvTX4L3r7lLodk+2pJEFBUPhXnsf94XmXXb/IrAeeDAgu2x73YdkN8Ie2SItc6ZbneaD4itlSn14KDR75NEI5M0DHsPt8Sdwm+Q46MokTBXJsgtcYOG06kkgVdK5N2E6kFeHl0eq8Qae8uF270lSPl5LQvLOHo4pc5SkBOBB2T0N9JM54QzcnpHTs81wGx6hvLVr2l7KngxMHwPwnOyex8vwnn8HwGSArrV4M2HD2ireF6js9H/Ce8qLeL0Ugtfr5d2S1i7AK0J5X5l6cW+AV187t8uLwHZpB1jbpNefA5ZL+Z3AuvSK5H7DMbwXAD1S228fD0mX1+R9wwdTKnlzOkHMnlFz3rSgYJB/xYrr4aYVK1Y0D8W7ETjzreTOBcDqELwDL97+9quYR8njwC3Xzn3rnUjlfVd2TYG3JV0JzJPm3F9e5j7jAVPNNXeQXWq5y3tWBrwnu+R74X3VpGoCGZs83vnAo5IZSZc0EJrWeb7eYOoltZ0MfS+RlP8BvBic1/pQUjrQUlr7ERT4Jfn7RijvRtntghjnqhqVwkmyO2WtPTFiJrBGGg/Tz5dTb5c3F9h7vtNVMLwWb8ok+NjwulvGtpddcgZcGoLXa5bUH/hETp8CbYPyJspuOfCZ9BZwn5yeiUxec7csgUly6oThVfPFn8fg9IX0ADwpt/td3pVUKyNQk1dfgPWW4XW2bCe3mZBWP++XwLlyugY4NSjvDjlNgjQzu1BOj0YT7+Vn4rTI8H4EH1fn/Yrq1X4ebVQMXyca3o+gidwyYXgo3keed1std8+vy2kh8GgY3g8gzayfLae9UcR77mQg5rOSPoZ3EWRW5x0M3DLW6/VavLoIKDW8i+A9ud0JZ4biHSSvh4ArKrmerZd3J/BN1N17D8HLg/OkZYa3DXzrl1OZy3sX8Kjc/MGo7sbtLHfLO+XUaCj0lPQdzAzDuwXYK6evgBx9D3wlu8XBeTtXPlf/EEW80+Eq2X1oeHuCeUY+f5bL28eC3gbhlrVBqK61DK/ZcoXsdgKvSfoMrD6heVOAW2TnHwOdpHygTHZXB+ddDnwnp8wo4i2HOwdJKQMM72wfxNy4TD++g8urq4BdATWfAHcuD0L1vsc7OwmKVrT3PzoUOuVLagk0nbIkFK/OAXqOU2oa8KakbpDx0+xtYwnOq3uArLlaeztRxOswxjzWNwPDq58BPmoNGN6UD4B7nwCwJgahal6K4VVXgA9iAJ733kmDFZJ398uA9S1AxSmS9mMKxXuqBVi/WEQTb0IGpm8N78hETBWGV293wuR7RMGoNnq8a88EU1lATpnheXXRUEy/bJFd9ke4xYbg1U9GlmZRxKsVvwJMX9zT8CrQIQa495P7MbxafqYF8NizCsrb9hcMr9ov7uZi7ZXpkh/ixvjC8GrzSxbg695Hbm85sFx1bShe/dYasNJaRRBv/bW/+e3RhY6Plz/h6W8GqQcknSEj+Ozgfdv1Dwpkb9v3o/5Faws/uTZPXv7vb/g9R+HaPK/xyOj+pbjPVQboHShQlBX5vL/9wq+nSHrWB+818B7t9gOd3vvpj48gKbeB92j3egVeT6mB96iXOgS3P3cGGniPRQnxfz33d3pyw3/M/tsOHRMBAAAgEOrf2g6OfxAB9OrVi1696NWrF7169aJXr1706tWrV69e9OrVi169etGrVy969aJXr1706tWLXr160av3oRe9evWiV69e9OpFr1696NWrF7169aJXL3r16kWvXr3o1asXvXr1AkDIAJeoFje3HAjXAAAAAElFTkSuQmCC";

                    if (File.Exists(path))
                    {
                        byte[] readText = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~" + BL.Api_URL.empimgpath + dt.Rows[0]["Employee_Code"] + "/" + dt.Rows[0]["Image"].ToString()));
                        Emp_Image = Convert.ToBase64String(readText);
                    }
                    else
                    {
                        Emp_Image = notFoundImage;
                    }

                    dt.Rows[0]["Image"] = Emp_Image;
                    obj_res.status = "success";
                    obj_res.message = "data Found";
                    obj_res.ArrayOfResponse = dt;
                }
                else
                {
                    obj_res.status = "Error";
                    obj_res.message = "No data Found";
                }
            }
            catch (Exception ex)
            {
                obj_res.status = "Error";
                obj_res.message = "Exception";
                InsertLog.WriteErrorLog("Error in  LoginHrms()/LoginController.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj_res);
        }
        //author => Chirag Dhruv
        /// <summary>
        /// here we get Employee id used to generate log.
        /// here in this method we get response in int value 
        /// and pass entity to response.
        /// </summary>
        /// <param name="obj_log"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("api/Login/EmpLog")]
        public HttpResponseMessage EmpLog(logEntity obj_log)
        {
            Response_entity obj_res = new Response_entity();
            int result = 0;
            try
            {
                LoginHRMS obj_empLog = new LoginHRMS();
                 result= obj_empLog.emplog(obj_log);
                if(result > 0)
                {
                    obj_res.status = "success";
                    obj_res.message = "data Found";
                }
                else
                {
                    obj_res.status = "Error";
                    obj_res.message = "No data Found";
                }
            }
            catch(Exception ex)
            {
                obj_res.status = "Error";
                obj_res.message = "No data Found";
                InsertLog.WriteErrorLog("Error in  EmpLog()/LoginController.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_res);
        }

        //author => Chirag Dhruv
        /// <summary>
        /// here we fetch otp based on email 
        /// we got response in datatable.
        /// and send Otp to Employee.
        /// </summary>
        [HttpPost]
        [Route("api/Login/getotp")]
        public HttpResponseMessage getotp(logEntity Obj)
        {
            Response_entity obj_res = new Response_entity();
            DataTable dt = new DataTable();
            string message = "";
            int otp = 0;
            string Email = "";
            try
            {
                LoginHRMS getotp = new LoginHRMS();
                dt = getotp.getotp(Obj.email_id);
                if(dt.Rows.Count > 0)
                {
                    Email = Convert.ToString(dt.Rows[0]["email_id"]);
                    otp = Convert.ToInt32(dt.Rows[0]["otp_digit"]);
                    if(Email== "OTP limit exists")
                    {
                        obj_res.status = "success";
                        obj_res.message = "data Found";
                        obj_res.ArrayOfResponse = dt;
                    }
                    else
                    {
                        getotp.sendMail(Email, otp);
                        obj_res.status = "success";
                        obj_res.message = "data Found";
                        obj_res.ArrayOfResponse = dt;
                    } 
                }
                else
                {
                    obj_res.status = "Error";
                    obj_res.message = "No data Found";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in  getotp()/LoginController.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_res);
        }
        //author => Chirag Dhruv
        /// <summary>
        /// here we verify otp and send response to api.
        /// here response is in datatable.
        /// </summary>
        [HttpPost]
        [Route("api/Login/verifyotp")]
        public HttpResponseMessage verifyotp(logEntity obj)
        {
            Response_entity obj_res = new Response_entity();
            DataTable verify_dt = new DataTable();
            try
            {
                LoginHRMS verifyotp = new LoginHRMS();
                verify_dt = verifyotp.verifyotp(obj);
                if(verify_dt.Rows.Count > 0)
                {
                    obj_res.status = "success";
                    obj_res.message = "data Found";
                    obj_res.ArrayOfResponse = verify_dt;
                }
                else
                {
                    obj_res.status = "Error";
                    obj_res.message = "No data Found";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in  verifyotp()/LoginController.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_res);
        }

        //author => Chirag Dhruv
        //here we reset password of employee on tbl_employee_master table.
        [HttpPost]
        [Route("api/Login/ResetEmployeePassword")]
        public HttpResponseMessage ResetEmployeePassword(logEntity obj_log)
        {
            Response_entity obj_res = new Response_entity();
            int result = 0;
            try
            {
                LoginHRMS objReset = new LoginHRMS();
                result = objReset.resetPass(obj_log);
                if (result > 0)
                {
                    obj_res.status = "success";
                    obj_res.message = "data Found";
                }
                else
                {
                    obj_res.status = "Error";
                    obj_res.message = "No data Found";
                }
            }
            catch (Exception ex)
            {
                obj_res.status = "Error";
                obj_res.message = "No data Found";
                InsertLog.WriteErrorLog("Error in  ResetEmployeePassword()/LoginController.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_res);
        }

        //author => Chirag Dhruv
        /// <summary>
        /// here we get Employee id used to generate log.
        /// here in this method we get response in int value 
        /// and pass entity to response.
        /// </summary>
        [HttpPost]
        [Route("api/Login/EmpLogOut")]
        public HttpResponseMessage EmpLogOut(logEntity obj)
        {
            Response_entity obj_res = new Response_entity();
            int result = 0;
            try
            {
                LoginHRMS obj_empLogOut = new LoginHRMS();
                result = obj_empLogOut.emplogOut(obj);
                if (result > 0)
                {
                    obj_res.status = "success";
                    obj_res.message = "data Found";
                }
                else
                {
                    obj_res.status = "Error";
                    obj_res.message = "No data Found";
                }
            }
            catch (Exception ex)
            {
                obj_res.status = "Error";
                obj_res.message = "No data Found";
                InsertLog.WriteErrorLog("Error in  EmpLogOut()/LoginController.cs: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_res);
        }


        // Author -> Yaksh Maishery
        // Sign Out
        [Route("api/Login/SignOut")]
        [HttpPost]
        public HttpResponseMessage SignOut(leave_entity EN)
        {
            DataSet DS = new DataSet();
            Response_entity res_en = new Response_entity();
            try
            {
                res_en.message = BL.LoginHRMS.SignOut(EN);
                if (res_en.message == "Signout Successfull!" || res_en.message == "You already signout!")
                {
                    res_en.status = "success";
                }
                else
                {
                    res_en.status = "failed";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> , method -> ApplyLeave\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_en);
        }

        [Route("api/Login/checkemailexists")]
        [HttpPost]
        public HttpResponseMessage checkemailexists(logEntity EN)
        {
            DataTable DT = new DataTable();
            Response_entity res_en = new Response_entity();
            try
            {
                DT = BL.LoginHRMS.checkemail(EN);

                if (DT.Rows.Count > 0)
                {
                    res_en.message = "Data Found";
                    res_en.status = "success";
                    res_en.ArrayOfResponse = DT;
                }
                else
                {
                    res_en.message = "Email Id not Exists";
                    res_en.status = "error";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Controller -> Login , method -> checkemailexists() :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_en);
        }

    }
}