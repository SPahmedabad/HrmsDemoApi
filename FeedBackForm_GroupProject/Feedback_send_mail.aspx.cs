using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using Library;
using System.Threading;

namespace FeedBackForm_GroupProject
{
    public partial class Feedback_send_mail : System.Web.UI.Page
    {
        //author ravi vaghela
        //dropdown bind
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("LoginAdmin.aspx", false);
            }
            else
            {
                if (!IsPostBack)
                {
                    try
                    {

                        bindDDL();
                    }
                    catch (Exception exe)
                    {
                        Library.InsertLog.WriteErrorLog("Error in binding og feedback_send_mail.aspx" + exe.Message + "stack trace :+" + exe.StackTrace);
                    }
                }
            }

        }
        //bind dropdown of feedback_send_mail page
        public void bindDDL()
        {
            //bind employee dropdown
            //AdminEntity adm_entity = new AdminEntity();
            //adm_entity.flag = "emp";

            EmployeeEntity obj = new EmployeeEntity();

            GetDataFromAPI apiobj = new GetDataFromAPI();
            DataTable dt = apiobj.Get_Employee(obj);

            if (dt.Rows.Count > 0)
            {
                ddl_emp.DataSource = dt;
                ddl_emp.DataTextField = dt.Columns["emp_name"].ToString();
                ddl_emp.DataValueField = dt.Columns["emp_id"].ToString();

                ddl_emp.DataBind();
                ddl_emp.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
            }
            else
            {
                ddl_emp.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
            }


            //bind department ddl 
            AdminEntity adm_entity = new AdminEntity();
            adm_entity.flag = "Department";

            DataTable dt_dept = GetDataFromAPI.Get_Dropdown_Mis_Data(adm_entity);
            if (dt_dept.Rows.Count > 0)
            {
                ddl_dept.DataTextField = dt_dept.Columns["dept_name"].ToString();
                ddl_dept.DataValueField = dt_dept.Columns["dept_id"].ToString();
                ddl_dept.DataSource = dt_dept;
                ddl_dept.DataBind();
                ddl_dept.Items.Insert(0, new ListItem("-- Select Department --", "0"));
            }
            else
            {
                ddl_dept.Items.Insert(0, new ListItem("-- Select Department --", "0"));
            }


            //bind module ddl 

            

           
        }

        //this method bind listview of all new join employee
        protected void ddl_module_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_emp.SelectedIndex != 0 && ddl_dept.SelectedIndex != 0)
                {
                    if (ddl_module.SelectedIndex == 0)
                    {
                        //list_employee.Visible = false;
                        listdiv.Visible = false;
                        //check_all.Visible = false;
                        //btn_submit.Visible = false;
                        Response.Write("<script> alert('Please select module name') </script>");
                    }
                    else
                    {
                        //list_employee.Visible = true;
                      
                        DataTable all_emp = GetDataFromAPI.get_Emp_Mail_DT();
                        if (all_emp.Rows.Count > 0)
                        {
                            listdiv.Visible = true;
                            list_employee.DataSource = all_emp;
                            list_employee.DataBind();

                            //check_all.Visible = true;
                            //btn_submit.Visible = true;
                        }else
                        {
                            listdiv.Visible = false;
                        }
                    }

                }
                else
                {

                    Response.Write("<script> alert('Please select employee and department') </script>");

                }


            }
            catch (Exception exe)
            {
                Library.InsertLog.WriteErrorLog("Error in binding listview of employee" + exe.Message + "stack trace :+" + exe.StackTrace);
            }
        }

        //author : ravi vaghela and saddam 
        //send email to all selected employee

        protected void btn_submit_Click(object sender, EventArgs e)
        {


            try
            {
                if (ddl_dept.SelectedIndex != 0 && ddl_emp.SelectedIndex != 0 && ddl_module.SelectedIndex != 0)
                {
                    string feedbackform_url = Api_URL.feedbackform_url;
                    string quaryString = ddl_emp.SelectedValue + "," + ddl_dept.SelectedValue + "," + ddl_module.SelectedValue;

                    if (Globals.lastmail_get_status == 0)
                    {
                        Globals.lastmail_get_status = LastMailId();
                        
                    }

                    bool isChecked = false;
                    List<EmployeeEntity> ListItems = new List<EmployeeEntity>();
                    foreach (var el_loopVariable in list_employee.Items)
                    {
                        //author saddam
                        //Passing id "chk" to FindControl method of current ListViewItem and trying to cast it as Checkbox

                        var checkBox = el_loopVariable.FindControl("check_email") as CheckBox;

                        if (checkBox != null && checkBox.Checked)
                        {
                            isChecked = true;
                            string[] temp = checkBox.ToolTip.Split(',');
                            EmployeeEntity obj_emp = new EmployeeEntity();
                            obj_emp.emp_email = temp[0];
                            obj_emp.emp_name = temp[1];

                            obj_emp.querystring = feedbackform_url + Operation.encrypt(quaryString + "," + Globals.mail_log_pk);

                            //for table email_log
                            obj_emp.email_log_id = Globals.mail_log_pk;
                            obj_emp.IsSend = 'N'; //mail not send this value is defualt
                            obj_emp.deptEntity.dept_name = ddl_dept.SelectedItem.Text;
                            obj_emp.moduleEntity.mod_name = ddl_module.SelectedItem.Text;
                            obj_emp.TrainerName = ddl_emp.SelectedItem.Text;
                            //testing this is for enter data in mail_log
                            obj_emp.deptEntity.dept_id = Convert.ToInt32(ddl_dept.SelectedValue);
                            obj_emp.moduleEntity.mod_id = Convert.ToInt32(ddl_module.SelectedValue);
                            obj_emp.emp_id = Convert.ToInt32(ddl_emp.SelectedValue);
                            ListItems.Add(obj_emp);

                            Globals.mail_log_pk = Globals.mail_log_pk + 1;
                        }
                    }

                    //send data to Api
                    if (isChecked)
                    {
                        Thread thread = null;
                        thread = new Thread(() => GetDataFromAPI.SendMail(ListItems));
                        //  string message = GetDataFromAPI.SendMail(ListItems);
                        thread.Start();
                        Response.Write("<script>alert('Mail Sent Successfully')</script>");
                    }
                    else
                    {
                        Response.Write("<script> alert('Please select employee first') </script>");
                    }

                }
                else
                {
                    Response.Write("<script> alert('Please select employee or module or department') </script>");
                }
            }
            catch (Exception exe)
            {
                Library.InsertLog.WriteErrorLog("Error in sending mail of all employee" + exe.Message + "stack trace :+" + exe.StackTrace);
            }

        }


        protected void list_employee_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            this.list_employee.SelectedIndex = e.NewSelectedIndex;
        }

        protected void check_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                foreach (var el_loopVariable in list_employee.Items)
                {
                    var checkBox = el_loopVariable.FindControl("check_email") as CheckBox;

                    if (check_all.Checked)
                    {
                        checkBox.Checked = true;
                    }
                    else
                    {
                        checkBox.Checked = false;
                    }
                }

            }
            catch (Exception exe)
            {
                InsertLog.WriteErrorLog("Error in select all checkboxes " + exe.Message + "stack trace :+" + exe.StackTrace);
            }
        }

        //this method will get last pk og Email_log for new pk genrating 
        public int LastMailId()
        {
            int lastmailid_status = 1;
            try
            {
                EmployeeEntity log_entity = new EmployeeEntity();
                log_entity.Flag = "lastmailid";
                DataSet ds = new DataSet();
                ds = GetDataFromAPI.Get_mis_formstatus_data(log_entity);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Globals.mail_log_pk = Convert.ToInt32(ds.Tables[0].Rows[0][0]) + 1;

                }
            }
            catch (Exception exe)
            {
                Library.InsertLog.WriteErrorLog("Error in Feedback_send_mail/lastmailid()" + exe.Message + "stack trace :+" + exe.StackTrace);
            }

            return lastmailid_status;
        }

        public class Globals
        {
            public static int mail_log_pk = 1; //this is for mail_log id genrating 
            public static int lastmail_get_status = 0; // this is i use for as flag for one time method calling LastMailId()
        }

        protected void ddl_emp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_emp.SelectedIndex != 0 && ddl_dept.SelectedIndex !=0 && ddl_module.SelectedIndex != 0)
            {
                //list_employee.Visible = true;         
                listdiv.Visible = true;
            }
            else
            {
                listdiv.Visible = false;
                //list_employee.Visible = false;
            }
        }

        protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_module.Items.Clear();
            listdiv.Visible = false;
            //if (ddl_emp.SelectedIndex != 0 && ddl_dept.SelectedIndex != 0 && ddl_module.SelectedIndex!=0)
            //{
            //    //list_employee.Visible = true;
            //    listdiv.Visible = false;
            //}
            //else
            //{
            //    listdiv.Visible = false;
            //    //list_employee.Visible = false;

            //}
            if (ddl_dept.SelectedIndex > 0)
            {
                GetDataFromAPI apiobj = new GetDataFromAPI();
                FeedbackFormEntity f_obj = new FeedbackFormEntity();

                f_obj.ddlDepartment = Convert.ToInt32(ddl_dept.SelectedValue);
                DataTable dt_mod = apiobj.getModule(f_obj);
                if (dt_mod.Rows.Count > 0)
                {
                    ddl_module.DataTextField = dt_mod.Columns["mod_name"].ToString();
                    ddl_module.DataValueField = dt_mod.Columns["mod_id"].ToString();
                    ddl_module.DataSource = dt_mod;
                    ddl_module.DataBind();
                    ddl_module.Items.Insert(0, new ListItem("-- Select Module --", "0"));
                }
                else
                {
                   
                    ddl_module.Items.Insert(0, new ListItem("-- Select Module --", "0"));
                }
            }
        }
    }
}