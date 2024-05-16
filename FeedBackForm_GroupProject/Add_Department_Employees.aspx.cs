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
    public partial class Add_Department_Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("LoginAdmin.aspx", false);
            }
            if (!IsPostBack)
            {
                bind_ddl_dept(); //binding department dropDown

            }
        }

        //author - dinesh
        public void bind_ddl_dept()
        {
            //bind department ddl 
            AdminEntity adm_entity = new AdminEntity();
            adm_entity.flag = "Department";

            DataTable dt_dept = GetDataFromAPI.Get_Dropdown_Mis_Data(adm_entity);
            if(dt_dept.Rows.Count>0)
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

        }

        protected void btn_emp_Click(object sender, EventArgs e)
        {

            //change by Dinesh  for mst_emp table change in table
            if (ddl_dept.SelectedItem.Text == "-- Select Department --"
                | txt_join_date.Text == "" | ddl_is_active.SelectedValue == "0"
                | string.IsNullOrWhiteSpace(txt_emp.Text) 
                | string.IsNullOrWhiteSpace(txt_email.Text)
               )
            {
                Response.Write("<script>alert('Please Fill All Employee field..')</script>");
            }
            else
            {

                //new field of table added by dinesh
                EmployeeEntity obj_emp = new EmployeeEntity();
                obj_emp.emp_name = txt_emp.Text;
                obj_emp.emp_email = txt_email.Text;
                obj_emp.deptEntity.dept_id = Convert.ToInt32(ddl_dept.SelectedValue);
                obj_emp.isActive = ddl_is_active.SelectedValue;
                obj_emp.joining_date = Convert.ToDateTime(txt_join_date.Text);

                GetDataFromAPI data = new GetDataFromAPI();
                string save_emp = data.Save_Data(obj_emp);

                if (save_emp == "1")
                {
                    Response.Write("<script>alert('Data Added Successfully')</script>");
                }
                else if (save_emp == "-1")
                {
                    Response.Write("<script>alert('Data already Present')</script>");

                }
                else
                {
                    Response.Write("<script>alert('Data not added ')</script>");

                }
                txt_emp.Text = "";
                txt_email.Text = "";
                txt_join_date.Text = "";
                ddl_is_active.SelectedValue = "0";
                ddl_dept.SelectedIndex = 0;
            }
        }

        protected void btn_dept_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txt_dept.Text))
            {
                Response.Write("<script>alert('Please Fill Department field..')</script>");
            }
            else
            {
                FeedbackFormEntity obj_form_entity = new FeedbackFormEntity();
                obj_form_entity.Department_Entity.dept_name = txt_dept.Text;
                GetDataFromAPI data = new GetDataFromAPI();
                string result = data.saveDept(obj_form_entity);
                Response.Write("<script>alert('" + result + "')</script>");

                txt_dept.Text = "";
            }
        }
    }
}