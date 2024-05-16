using BL;
using Entity;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FeedBackForm_GroupProject
{
    public partial class Module : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //condintion added by dinesh
            if (Session["Login"] == null)
            {
                Response.Redirect("LoginAdmin.aspx",false);
            }

            if(!IsPostBack)
            {
                try
                {
                    
                    FeedbackFormEntity obj = new FeedbackFormEntity();
                    GetDataFromAPI get_dept = new GetDataFromAPI();
                    DataTable dt_dept = get_dept.getDept_new(obj);
                    

                    if (dt_dept != null )
                    {
                        ddl_dept.DataSource = dt_dept;
                        ddl_dept.DataTextField = "dept_name";
                        ddl_dept.DataValueField = "dept_id";
                        ddl_dept.DataBind();
                        ddl_dept.Items.Insert(0, new ListItem("-- Select Department --", "0"));
                    }
                    else
                    {
                        ddl_dept.Items.Insert(0, new ListItem("-- Select Department --", "0"));
                    }
                }
                catch (Exception ex)
                {
                    InsertLog.WriteErrorLog("Error in binding department drop down in page load module.aspx : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
                }

            }
        }
   //This button click will add the entered module into the module table
        protected void btn_add_mod_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt_add_mod.Text == "" || ddl_dept.SelectedIndex == 0 )
                {
                    Response.Write("<script>alert('please enter module name. or select dept first')</script>");
                }
                else
                {
                    Operation obj = new Operation();
                    FeedbackFormEntity entity = new FeedbackFormEntity();
                    entity.Module_Entity.mod_name = txt_add_mod.Text;
                    //entity.Department_Entity.dept_id = Convert.ToInt32(ddl_dept.SelectedItem.Text);
                    entity.Department_Entity.dept_id = Convert.ToInt32(ddl_dept.SelectedValue);
                    string msg = obj.saveData(entity);
                    Response.Write("<script>alert('" + msg + "');</script>");
                    txt_add_mod.Text = "";
                }
            }
            catch(Exception ex)
            {
                InsertLog.WriteErrorLog("Error in button click event of btn_add_mod_Click: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
        }
          
        }
    }
