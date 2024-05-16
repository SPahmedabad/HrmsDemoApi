using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Feedback_API.Controllers
{
    //Author -> Hemangini
    public class EmployeeController : ApiController
    {

        Operation info = new Operation();

        [HttpPost]
        [Route("api/Employee/GetData")]
        //method to get data of employee table
        public HttpResponseMessage GetData()
        {
            EmployeeEntity obj_emp = new EmployeeEntity();
            DataSet get_data = info.Emp_get(obj_emp);
            return Request.CreateResponse(HttpStatusCode.OK, get_data);
        }



        [HttpPost]
        [Route("api/Employee/SaveData")]
        //method to save data into employee table
        public HttpResponseMessage SaveData(EmployeeMainData obj_emp)
        {
        
            DataTable dt = info.EmpCheck(obj_emp);
            if (dt.Rows.Count == 0)
            {
                string save_data = info.Emp_save(obj_emp);
                return Request.CreateResponse(HttpStatusCode.OK, save_data);
            }
            else
            {
                string message = "Employee Already Present";
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
           
        }

        //[HttpPost]
        //[Route("api/Employee/SaveData")]
        ////method to save data into employee table
        //public HttpResponseMessage SaveData(EmployeeEntity obj_emp)
        //{

        //    string save_data = info.Emp_save(obj_emp);
        //    return Request.CreateResponse(HttpStatusCode.OK, save_data);
        //}


        //Author ---> Satyam
        /// <summary>
        /// API Method For Getting DataTable
        /// Of Employee Mail List With Dept_Name,Email,Joining Date
        /// </summary>


        //Author ---> Satyam
        /// <summary>
        /// API Method For Getting DataTable
        /// Of Employee Mail List With Dept_Name,Email,Joining Date
        /// </summary>
        [HttpPost]
        [Route("api/Employee/api_EmpMailDT")]
        public HttpResponseMessage api_EmpMailDT()
        {
            DataTable ds = new DataTable();
            ds = BL.Operation.Emp_MailList();

            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

    }
}
