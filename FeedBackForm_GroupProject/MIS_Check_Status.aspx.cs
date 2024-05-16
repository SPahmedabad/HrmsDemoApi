using BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;

namespace FeedBackForm_GroupProject
{
    public partial class MIS_Check_Status : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("LoginAdmin.aspx", false);
            }
            else
            {
                listviewbind();
            }

        }

        public void listviewbind()
        {
            try
            {
                EmployeeEntity emp_ent = new EmployeeEntity();      
                ds = GetDataFromAPI.Get_mis_formstatus_data(emp_ent);        
                lv_subdata.Visible = false;
                lv_notsubdata.Visible = false;
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("MIS_Check_Status : listviewbind : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
        }



        protected void ddlcheck_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Here we selecte submit ddl and return all employee whose have submited data.

            if (ddlcheck_status.SelectedValue == "0")
            {
                lv_subdata.Visible = false;
                lv_notsubdata.Visible = false;
            }

            if (ddlcheck_status.SelectedValue == "submit")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lv_subdata.DataSource = ds.Tables[0];
                    lv_subdata.DataBind();
                    lv_notsubdata.Visible = false;
                    lv_subdata.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('No data found')</script>");
                }

            }

            //Here we selecte not_submit ddl and return all employee whose have not_submited data.
            if (ddlcheck_status.SelectedValue == "not_submit")
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    lv_notsubdata.DataSource = ds.Tables[1];
                    lv_notsubdata.DataBind();
                    lv_subdata.Visible = false;
                    lv_notsubdata.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('No data found')</script>");

                }
            }

        }
    }
}