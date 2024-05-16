using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FeedBackForm_GroupProject
{
    public partial class Admin_ViewData : System.Web.UI.Page
    {
        //Author -> Satyam
        /// <summary>
        /// Page Load For Bind Department DDl 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// At page load we bind departmet DDL so user can select and base on dep selection we will bing module DDl
        /// for spacific search 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Login"]==null)
                {
                    Response.Redirect("LoginAdmin.aspx",false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        DataTable ds_dept = new DataTable();

                        //no need to use blank entity.
                        FeedbackFormEntity obj = new FeedbackFormEntity();
                        GetDataFromAPI get_dept = new GetDataFromAPI();
                        DataTable dt_dept = get_dept.getDept_new(obj);
                        ddl_department.DataSource = dt_dept;
                        ddl_department.DataTextField = "dept_name";
                        ddl_department.DataValueField = "dept_id";
                        ddl_department.DataBind();
                        ddl_department.Items.Insert(0, new ListItem("-- Select Department --", "0"));

                    }
                }
            }
            catch(Exception ex)
            {
                Library.InsertLog.WriteErrorLog("at page load binding Dept DDL E_Message : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
        }

        private bool HasEvents(DropDownList ddl_department)
        {
            throw new NotImplementedException();
        }
        //Author -> Satyam
        /// <summary>
        /// DDl Event Method For Bind Feedback Data Based On Department 
        /// And Module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_module_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                    int sel_dept = Convert.ToInt32(ddl_department.SelectedValue);
                    int sel_mod = Convert.ToInt32(ddl_module.SelectedValue);
                    FeedbackFormEntity en_mod = new FeedbackFormEntity();
                    en_mod.ddlDepartment = sel_dept;
                    en_mod.ddlModuleName = sel_mod;
                    DataSet ds_dept = new DataSet();
                    ds_dept = GetDataFromAPI.get_feed_data(en_mod);
                    vie_empFeedback.DataSource = ds_dept;
                    vie_empFeedback.DataBind();
               
            }
            catch(Exception ex)
            {
                Library.InsertLog.WriteErrorLog("module ddl binding and retive selection on change event of ddl E_Message : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
        }
        //Author -> Satyam
        /// <summary>
        /// Item Commend Method For Get ID On View Profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void vie_empFeedback_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect(String.Format("FeedbackForm.aspx?id={0}", id), false);
                }
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Admin_ViewData : vie_empFeedback_ItemCommand : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
             }
        }
        //Author -> Satyam
        /// <summary>
        /// Department DDL For Bind Module DDL and 
        /// Bind ListView Based On Department 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_department.SelectedIndex == 0)
                {
                    ddl_module.Items.Clear();
                    ddl_module.Visible = false;
                    vie_empFeedback.Visible = false;
                }
                else
                {
                    FeedbackFormEntity en_mod = new FeedbackFormEntity();
                    en_mod.ddlDepartment = Convert.ToInt32(ddl_department.SelectedValue);
                    DataTable ds_mod = new DataTable();
                    GetDataFromAPI obj_apicall = new GetDataFromAPI();
                    ds_mod = obj_apicall.getData(en_mod);
                    if (ds_mod.Rows.Count > 0)
                    {
                        ddl_module.DataSource = ds_mod;
                        ddl_module.DataTextField = "mod_name";
                        ddl_module.DataValueField = "mod_id";
                        ddl_module.DataBind();
                        ddl_module.Items.Insert(0, new ListItem("-- Select Module --", "0"));
                    }


                    int sel_dept = Convert.ToInt32(ddl_department.SelectedValue);
                    en_mod = new FeedbackFormEntity();

                    en_mod.ddlDepartment = sel_dept;
                    en_mod.ddlModuleName = 0;
                    DataSet ds_dept = new DataSet();
                    ds_dept = GetDataFromAPI.get_feed_data(en_mod);
                    vie_empFeedback.DataSource = ds_dept;
                    vie_empFeedback.DataBind();
                    vie_empFeedback.Visible = true;
                    ddl_module.Visible = true;
                }
            }
            catch(Exception ex)
            {
                Library.InsertLog.WriteErrorLog("On selection change event showing data in Gird View E_Mess : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
        }

      
    }
}