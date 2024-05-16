using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library;

namespace FeedBackForm_GroupProject
{
    public partial class OptimumPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //author dinesh
            if (Convert.ToBoolean(Session["Login"]) == true)
            {
                try
                {
                    btn_home.Visible = true;
                    btn_add_department.Visible = true;
                    btn_add_module.Visible = true;
                    btn_view_mis_report.Visible = true;
                    btn_mis_submit_report.Visible = true;
                    btn_log_out.Visible = true;
                    btn_semd_feedback_link.Visible = true;
                }
                catch (Exception ex)
                {
                    InsertLog.WriteErrorLog("On page load of master error : " + ex.Message + "StackTrac : " + ex.StackTrace);
                } 
            }

        }

        protected void btn_add_module_Click(object sender, EventArgs e)
        {

            Response.Redirect("Module.aspx", false);
        }

        protected void btn_add_department_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Department_Employees.aspx", false);
        }

        protected void btn_log_out_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("LoginAdmin.aspx", false);
        }

        protected void btn_view_mis_report_Click(object sender, EventArgs e)
        {
            Response.Redirect("MIS_Report.aspx", false);
        }

        protected void btn_mis_submit_report_Click(object sender, EventArgs e)
        {
            Response.Redirect("MIS_Check_Status.aspx", false);
        }

        protected void btn_semd_feedback_link_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feedback_send_mail.aspx", false);
        }

        protected void btn_home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_ViewData.aspx",false);
        }
    }
}