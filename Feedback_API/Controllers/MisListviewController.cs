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
    public class MisListviewController : ApiController
    {
        [HttpPost]
        [Route("api/MisListview/DeptListview")]

        public HttpResponseMessage DeptListview(AdminEntity en)
        {
            DataTable dt = new DataTable();
            try
            {
                Operation getliv = new Operation();
                dt = getliv.Get_Mis_Listview_Data(en);
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Controller : MisListviewController : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }
    }
}
