using Entity;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL;

namespace Feedback_API.Controllers
{
    public class MisController : ApiController
    {
        [HttpPost]
        [Route("api/Mis/MisDeptListviee")]

        public HttpResponseMessage MisDeptListviee(AdminEntity en)
        {
            DataTable dt = new DataTable();
            try
            {
                Operation deptlv = new Operation();
                dt = deptlv.MisDropdown(en);
            }
            catch(Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Controller : MisController : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        // Author -> Yaksh Maishery
        // Bind Department Data with DDL
        [HttpPost]
        [Route("api/Mis/MisDdlData")]
        public HttpResponseMessage MisDdlData(AdminEntity admin_entity)
        {
            DataTable DT = new DataTable();
            Response_entity res = new Response_entity();
            try{
                DataTable dt = GetDataFromAPI.Get_Dropdown_Mis_Data(admin_entity);

                if (dt.Rows.Count > 0)
                {
                    DT = dt;
                    res.status = "success";
                    res.message = "Data is Found";
                    
                }
                else
                {
                    res.status = "failed";
                    res.message = "Data not Found";
                }
                    res.ArrayOfResponse = DT;
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Controller : MisController : " + ex.Message + "InnserException : " + ex.InnerException + "StackTrace : " + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }

}
