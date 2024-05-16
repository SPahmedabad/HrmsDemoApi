using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FeedBackForm_GroupProject
{
    public partial class MIS_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        AdminEntity adm_entity = new AdminEntity();
                        adm_entity.flag = "Department";

                        DataTable dt = GetDataFromAPI.Get_Dropdown_Mis_Data(adm_entity);

                        if (dt.Rows.Count >0)
                        {
                            ddl_dept.DataTextField = dt.Columns["dept_name"].ToString();
                            ddl_dept.DataValueField = dt.Columns["dept_id"].ToString();
                            ddl_dept.DataSource = dt;
                            ddl_dept.DataBind();
                            ddl_dept.Items.Insert(0, new ListItem("-- Select Department --", "0"));
                            ddl_module.Visible = false;
                        }
                        else
                        {
                            ddl_dept.Items.Insert(0, new ListItem("-- Select Department --", "0"));
                            ddl_module.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Library.InsertLog.WriteErrorLog("MisGetDropDownDataFromAPI : Get_Dropdown_Mis_Data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
                }
            }
           else
            {
                Response.Redirect("LoginAdmin.aspx",false);
            }
        }

        //department dropdown changed event call
        protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_module.Items.Clear();
            if (ddl_dept.SelectedIndex > 0)
            {
                try
                {
                    int sel_dept = Convert.ToInt32(ddl_dept.SelectedValue);
                    modulelv.Visible = false;
                    dept_mis_lv.Visible = true;
                    AdminEntity adm_entity = new AdminEntity();
                    adm_entity.dpt_id = sel_dept;
                    adm_entity.flag = "dept";
                    DataTable dt = GetDataFromAPI.Get_Listview_Mis_Data(adm_entity);

                    if (dt.Rows.Count>0)
                    {
                        dept_mis_lv.DataSource = dt;
                        dept_mis_lv.DataBind();
                        bindmoduleddl(Convert.ToInt32(ddl_dept.SelectedValue));//call when department index changed
                        btnexptoexc.Visible = true; //added by dinesh
                        ddl_module.Visible = true;
                        dept_mis_lv.Visible = true;
                    }
                    else
                    {
                        btnexptoexc.Visible = false;
                        ddl_module.Visible = false;
                        dept_mis_lv.Visible = false;
                        modulelv.Visible = false;

                        Response.Write("<script>alert('No Data Found')</script>");
                    }
                }
                catch (Exception ex)
                {
                    Library.InsertLog.WriteErrorLog("DeptGetDataListviewAPI : Get_Listview_Mis_Data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
                }
            }

            else
            {               
                dept_mis_lv.Visible = false;
                ddl_module.Visible = false;
                btnexptoexc.Visible = false;
                modulelv.Visible = false;
            }
        }

        //module dropdown bind when department index changed 
        public void bindmoduleddl(int dept_id)
        {
            AdminEntity adm_entity = new AdminEntity();
            adm_entity.flag = "Module";
            adm_entity.dpt_id = dept_id;
            DataTable dt = GetDataFromAPI.Get_Dropdown_Mis_Data(adm_entity);
            if (dt.Rows.Count > 0)
            {
                ddl_module.DataTextField = dt.Columns["mod_name"].ToString();
                ddl_module.DataValueField = dt.Columns["mod_id"].ToString();
                ddl_module.DataSource = dt;
                ddl_module.DataBind();
                ddl_module.Items.Insert(0, new ListItem("-- Select Module --", "0"));
            }else
            {
                ddl_module.Items.Insert(0, new ListItem("-- Select Module --", "0"));
            }
         
        }


        //module selected index changed called
        protected void ddl_module_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_module.SelectedIndex > 0)
            {
                try
                {
                    int mode_id = Convert.ToInt32(ddl_module.SelectedValue);
                    modulelv.Visible = true;
                    dept_mis_lv.Visible = false;
                    AdminEntity adm_entity = new AdminEntity();
                    adm_entity.md_id = mode_id;
                    adm_entity.dpt_id = Convert.ToInt32(ddl_dept.SelectedValue);
                    adm_entity.flag = "module";
                    DataTable dt = GetDataFromAPI.Get_Listview_Mis_Data(adm_entity);
                    if (dt.Rows.Count > 0)
                    {
                        btnexptoexc.Visible = true;
                        modulelv.DataSource = dt;
                        modulelv.DataBind();
                    }
                    else
                    {
                        btnexptoexc.Visible = false;
                        modulelv.Visible = false;
                        dept_mis_lv.Visible = false;
                        Response.Write("<script>alert('No Data Found')</script>");
                    }
                }
                catch (Exception ex)
                {
                    Library.InsertLog.WriteErrorLog("DeptGetDataListviewAPI : Get_Listview_Mis_Data : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
                }
            }
            else
            {              
                modulelv.Visible = false;
                dept_mis_lv.Visible = true;
            }           
        }

        //Button Click Export Listview Data To Excel File
        protected void btnexptoexc_Click(object sender, EventArgs e)
        {
            try
            {
                AdminEntity en = new AdminEntity();
                int dept_id = Convert.ToInt32(ddl_dept.SelectedValue);
                int mode_id = Convert.ToInt32(ddl_module.SelectedValue);
                en.dpt_id = dept_id;
                en.md_id = mode_id;

                if (ddl_module.SelectedItem.Text != "-- Select Module --" && ddl_dept.SelectedItem.Text != "-- Select Department --")
                {
                    en.flag = "module";
                }
                else
                {
                    en.flag = "dept";
                }
                Operation objData = new Operation();
                DataTable dt = objData.Get_Mis_Listview_Data(en);
                ExportDataToExcel(dt);//This Method Called when Export listview data to excel
            }

            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog(" error is arrived on btnexptoexc_Click " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }

        }

        //This method Is Used to Export Listview data to excel file
        private void ExportDataToExcel(DataTable dt)
        {
            try
            {
                string attachment = "attachment; filename=MISReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                HttpContext.Current.Response.Flush(); 
                HttpContext.Current.Response.SuppressContent = true;  
                HttpContext.Current.ApplicationInstance.CompleteRequest(); 
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog(" error is arrived on ExportDataToExcel " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
        }
    }
}