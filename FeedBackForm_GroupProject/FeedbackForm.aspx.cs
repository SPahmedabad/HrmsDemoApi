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
    public partial class FeedbackForm : System.Web.UI.Page
    {
        //author -> Chirag Dhruv
        /// <summary>
        /// here we fetch id form query string.
        /// and then send that id to Api.
        /// we get response in data set and fill the data in textbox and dropdown..
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //author Dinesh
            //this condition is checking whether the link is used or not by checking pk of email_log_table to main table 
            //if user already used this link and successfully submit response then that log insert in main table with pk of email_log 
            //hence we can check user already filled or not 
            if (Request.QueryString["qs"] != null && Request.QueryString["qs"] != string.Empty)
            {
                userView(); //showing user related view
            }

            //this condition for admin option of viewFeedback
            else if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty && Session["Login"] != null)
            {
                adminView(); //showing admin related form view
            }
            else
            {
                Response.Redirect("AccessDenied.aspx", false);
            }
        }

        //author Chirag changes done by Dinesh
        /// <summary>
        /// this method is use for showing form perticular user with prefilled values for admin show
        /// </summary>
        public void adminView()
        {
            try
            {
                int trainee_id = 0;
                trainee_id = Convert.ToInt32(Request.QueryString["id"]);
                FeedbackFormEntity obj_trainee = new FeedbackFormEntity();
                obj_trainee.Trainee_Id = trainee_id;

                //here we fetch data from Trainee_FeedBack Table.
                GetDataFromAPI obj_FetchProfile = new GetDataFromAPI();
                DataSet ds = obj_FetchProfile.FillFormData(obj_trainee);

                //here we check Dataset is empty or not..
                if (ds.Tables[0].Rows.Count > 0)
                {
                    FillValues(ds); //here we fill values in TextBox.
                }
                else
                {
                    Response.Write("<script>alert('Record not Found.')</script>");
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in PageLoad Event of FeedBackForm when filling Form : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
        }

        //author Dinesh
        /// <summary>
        /// this method is use for showing feedback form to user in which 3 field already filled by admin this will come from quary string and we retrive it 
        /// and assign value to that field user cannot change that value rest of that user can fill 
        /// </summary>
        public void userView()
        {
            try
            {
                string[] prevalue = Operation.Decrypt(Request.QueryString["qs"]).Split(',');

                if (prevalue[0] != "NotValid")
                {
                    EmployeeEntity ent = new EmployeeEntity();
                    ent.email_log_id = Convert.ToInt32(prevalue[3]);

                    //here we cheking quary link to find out this is filled or not here we check by pk of table Email_log 
                    //and fk mail_log_id of Trainee_FeedBack by matching that value we can make decision
                    DataTable dt_feedbackExist = GetDataFromAPI.chk_log_exists(ent);

                    if (dt_feedbackExist.Rows.Count > 0)
                    {
                        //WINDOW CLOSE ON OK CLICK
                        //Response.Write("<script>alert('You Already Filled This Form');window.close();</script>");
                        Response.Redirect("thankyou.aspx", false);
                        Control form = (Control)Master.FindControl("form1");
                        form.Visible = false;
                        return;
                    }
                    else
                    {
                        if (!IsPostBack)
                        {
                            try
                            {
                                GetDataFromAPI get_employee = new GetDataFromAPI();
                                DataTable ds_fas = get_employee.getEmployee();
                                ddlemp_binding(ds_fas);//here we bind Employee data
                            }
                            catch (Exception ex)
                            {
                                InsertLog.WriteErrorLog("Error in PageLoad binding ddlEmployee : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);

                            }
                            try
                            {
                                //no need to use blank entity.
                                FeedbackFormEntity obj = new FeedbackFormEntity();
                                GetDataFromAPI get_dept = new GetDataFromAPI();
                                DataTable dt_dept = get_dept.getDept_new(obj);
                                ddldept_binding(dt_dept);//here we bind Department data
                            }
                            catch (Exception ex)
                            {
                                InsertLog.WriteErrorLog("Error in PageLoad binding ddlDepartment : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);

                            }

                            //dinesh
                            ddlmod_bind(); //here we bind module data
                        }
                        //author --dinesh  decrypt quary string assining prefill value of admin to user
                        // string[] prevalue = Operation.Decrypt(Request.QueryString["qs"]).Split(',');
                        ddlDepartment.SelectedValue = prevalue[1].ToString();
                        ddlModuleName.SelectedValue = prevalue[2].ToString();
                        ddlTrainer.SelectedValue = prevalue[0].ToString();
                        ddlDepartment.Enabled = false;
                        ddlModuleName.Enabled = false;
                        ddlTrainer.Enabled = false;
                    }
                }
                else
                {
                    Response.Redirect("AccessDenied.aspx", false);
                    ////here on ok click we will close tab
                    //Response.Write("<script>alert('Please Enter Valid FeedBack URL');window.close();</script>");
                    //Control form = (Control)Master.FindControl("form1");
                    //form.Visible = false;
                    //return;
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in PageLoad Retriving quaryString : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
        }

        //Author -> Chirag Dhruv
        //this method fill values in textbox of Feedbackform
        public void FillValues(DataSet ds)
        {
            ddlDepartment.SelectedItem.Text = ds.Tables[0].Rows[0]["dept_name"].ToString();
            ddlDepartment.Enabled = false;
            txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["start_date"]).ToString("yyyy-MM-dd");
            txtStartDate.ReadOnly = true;
            txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["end_date"]).ToString("yyyy-MM-dd");
            txtEndDate.ReadOnly = true;
            ddlModuleName.SelectedItem.Text = ds.Tables[0].Rows[0]["mod_name"].ToString();
            ddlModuleName.Enabled = false;
            ddlTrainer.SelectedItem.Text = ds.Tables[0].Rows[0]["emp_Name"].ToString();
            ddlTrainer.Enabled = false;
            ddlCity.SelectedItem.Text = ds.Tables[0].Rows[0]["city"].ToString();
            ddlCity.Enabled = false;
            ddlState.SelectedItem.Text = ds.Tables[0].Rows[0]["state"].ToString();
            ddlState.Enabled = false;
            ddlMode.Text = ds.Tables[0].Rows[0]["mode_of_training_is_online_ofline"].ToString();
            ddlMode.Enabled = false;

            string[] enojoy_feed = ds.Tables[0].Rows[0]["trainee_enjoyment"].ToString().Split(',');

            txtEnjoyMostAboutTrainingA.Text = enojoy_feed[0];
            txtEnjoyMostAboutTrainingA.ReadOnly = true;
            txtEnjoyMostAboutTrainingB.Text = enojoy_feed[1];
            txtEnjoyMostAboutTrainingB.ReadOnly = true;
            txtEnjoyMostAboutTrainingC.Text = enojoy_feed[2];
            txtEnjoyMostAboutTrainingC.ReadOnly = true;
            txtImprove.Text = ds.Tables[0].Rows[0]["need_improve"].ToString();
            txtImprove.ReadOnly = true;
            txtInformationSkills.Text = ds.Tables[0].Rows[0]["trainee_learn"].ToString();
            txtInformationSkills.ReadOnly = true;
            txtComments.Text = ds.Tables[0].Rows[0]["trainee_comments"].ToString();
            txtComments.ReadOnly = true;
            btnSubmit.Visible = false;
            //here we fetch data from Trainee_Rating Table.
            ddl_ques1.SelectedItem.Text = ds.Tables[1].Rows[0]["rating"].ToString();
            ddl_ques1.Enabled = false;
            ddl_ques2.SelectedItem.Text = ds.Tables[1].Rows[1]["rating"].ToString();
            ddl_ques2.Enabled = false;
            ddl_ques3.SelectedItem.Text = ds.Tables[1].Rows[2]["rating"].ToString();
            ddl_ques3.Enabled = false;
            ddl_ques4.SelectedItem.Text = ds.Tables[1].Rows[3]["rating"].ToString();
            ddl_ques4.Enabled = false;
            ddl_ques5.SelectedItem.Text = ds.Tables[1].Rows[4]["rating"].ToString();
            ddl_ques5.Enabled = false;
            ddl_ques6.SelectedItem.Text = ds.Tables[1].Rows[5]["rating"].ToString();
            ddl_ques6.Enabled = false;
            ddl_ques7.SelectedItem.Text = ds.Tables[1].Rows[6]["rating"].ToString();
            ddl_ques7.Enabled = false;
            ddl_ques8.SelectedItem.Text = ds.Tables[1].Rows[7]["rating"].ToString();
            ddl_ques8.Enabled = false;
            ddl_ques9.SelectedItem.Text = ds.Tables[1].Rows[8]["rating"].ToString();
            ddl_ques9.Enabled = false;
            ddl_ques10.SelectedItem.Text = ds.Tables[1].Rows[9]["rating"].ToString();
            ddl_ques10.Enabled = false;

        }

        //here we perform dropdown binding of Employee.
        public void ddlemp_binding(DataTable ds_fas)
        {
            ddlTrainer.DataSource = ds_fas;
            ddlTrainer.DataTextField = "emp_name";
            ddlTrainer.DataValueField = "emp_id";
            ddlTrainer.DataBind();
            ddlTrainer.Items.Insert(0, new ListItem("--Select Trainer--", "0"));
        }

        //here we perform dropdown binding of Department..
        public void ddldept_binding(DataTable dt_dept)
        {
            ddlDepartment.DataSource = dt_dept;
            ddlDepartment.DataTextField = "dept_name";
            ddlDepartment.DataValueField = "dept_id";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
        }

        //author dinesh
        public void ddlmod_bind()
        {
            try
            {
                FeedbackFormEntity en_mod = new FeedbackFormEntity();
                //en_mod.ddlDepartment = Convert.ToInt32(ddlDepartment.SelectedValue);
                DataTable ds_mod = new DataTable();
                GetDataFromAPI obj_apicall = new GetDataFromAPI();
                ds_mod = obj_apicall.getData(en_mod);
                if (ds_mod.Rows.Count > 0)
                {
                    ddlModuleName.DataSource = ds_mod;
                    ddlModuleName.DataTextField = "mod_name";
                    ddlModuleName.DataValueField = "mod_id";
                    ddlModuleName.DataBind();
                    //ddlModuleName.Items.Insert(0, new ListItem("-- Select Module --", "0"));
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in  ddlDepartment_SelectedIndex/getdata() ddlModuleName: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
        }

        //Author -> Saddam Shaikh
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                string[] prevalue = Operation.Decrypt(Request.QueryString["qs"]).Split(',');
                EmployeeEntity ent = new EmployeeEntity();
                ent.email_log_id = Convert.ToInt32(prevalue[3]);

                //here we cheking quary link to find out this is filled or not here we check by pk of table Email_log 
                //and fk mail_log_id of Trainee_FeedBack by matching that value we can make decision
                DataTable dt_feedbackExist = GetDataFromAPI.chk_log_exists(ent);

                if (dt_feedbackExist.Rows.Count > 0)
                {
                    Response.Redirect("thankyou.aspx", false);
                }
                else
                {
                    //Author -> Chirag Dhruv
                    //here we perform validations.
                    if (
                        string.IsNullOrWhiteSpace(txtStartDate.Text)
                        || string.IsNullOrWhiteSpace(txtEndDate.Text) || ddlModuleName.SelectedValue == "Select"
                        || ddlDepartment.SelectedIndex == 0 || ddlTrainer.SelectedIndex == 0 || ddlState.SelectedValue == "Select"
                        || hidDdl_City.Value == "" || ddlMode.SelectedValue == "Select" || string.IsNullOrWhiteSpace(txtEnjoyMostAboutTrainingA.Text)
                        || string.IsNullOrWhiteSpace(txtImprove.Text) || string.IsNullOrWhiteSpace(txtInformationSkills.Text) || ddl_ques1.SelectedValue == "Select"
                        || ddl_ques2.SelectedValue == "Select" || ddl_ques3.SelectedValue == "Select"
                        || ddl_ques4.SelectedValue == "Select" || ddl_ques5.SelectedValue == "Select"
                        || ddl_ques6.SelectedValue == "Select" || ddl_ques7.SelectedValue == "Select"
                        || ddl_ques8.SelectedValue == "Select" || ddl_ques9.SelectedValue == "Select"
                        || ddl_ques10.SelectedValue == "Select"
                        )
                    {

                        Response.Write("<script>alert('Please Fill All Fields....');</script>");
                    }
                    else
                    {
                        //author dinesh
                        prevalue = null;
                        try
                        {
                            FeedbackFormEntity formEntity = bindFormData();
                            if (Request.QueryString["qs"] != null && Request.QueryString["qs"] != string.Empty)
                                prevalue = Operation.Decrypt(Request.QueryString["qs"]).Split(',');

                            //here 3rd value is our pk of email_log_table
                            formEntity.mail_log_id = Convert.ToInt32(prevalue[3]);
                            int rowsInserted = GetDataFromAPI.saveFeedbackFormData(formEntity);

                            if (rowsInserted > 0)
                            {
                                //change by dinesh
                                //if data is inserted successfully this form will desappear 
                                Control form = (Control)Master.FindControl("form1");
                                form.Visible = false;
                                clearTextBox();
                                Response.Redirect("thankyou.aspx", false);
                            }
                            else
                            {
                                Response.Write("<script>alert('Feedback Not Submited !!! -- Please Contact HR ');</script>");
                            }
                        }
                        catch (Exception ex)
                        {
                            InsertLog.WriteErrorLog("Error on submint button click inserting data in main table : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog(ex.ToString());
            }
        }

        //Author -> Saddam Shaikh
        //This method will bind form data on submit click.
        private FeedbackFormEntity bindFormData()
        {
            String city = hidDdl_City.Value;

            FeedbackFormEntity formEntity = new FeedbackFormEntity();
            formEntity.StartDate = Convert.ToDateTime(txtStartDate.Text);
            formEntity.EndDate = Convert.ToDateTime(txtEndDate.Text);
            formEntity.ddlModuleName = Convert.ToInt32(ddlModuleName.SelectedValue);
            formEntity.ddlDepartment = Convert.ToInt32(ddlDepartment.SelectedValue);
            formEntity.ddlTrainer = Convert.ToInt32(ddlTrainer.SelectedValue);
            formEntity.State = ddlState.SelectedValue;
            formEntity.City = city;
            formEntity.ModeOfTraining = Convert.ToChar(ddlMode.SelectedValue);
            formEntity.EnjoyMostAboutTraining1 = txtEnjoyMostAboutTrainingA.Text;
            formEntity.EnjoyMostAboutTraining2 = txtEnjoyMostAboutTrainingB.Text;
            formEntity.EnjoyMostAboutTraining3 = txtEnjoyMostAboutTrainingC.Text;
            formEntity.ImproveTraining = txtImprove.Text;
            formEntity.SkillsLearn = txtInformationSkills.Text;
            formEntity.Comments = txtComments.Text;

            formEntity.Answers = ddl_ques1.SelectedValue + ',' + ddl_ques2.SelectedValue + ',' + ddl_ques3.SelectedValue + ',' + ddl_ques4.SelectedValue + ',' + ddl_ques5.SelectedValue + ',' + ddl_ques6.SelectedValue + ',' + ddl_ques7.SelectedValue + ',' + ddl_ques8.SelectedValue + ',' + ddl_ques9.SelectedValue + ',' + ddl_ques10.SelectedValue;

            return formEntity;
        }

        protected void ddlDepartment_SelectedIndexChanged2(object sender, EventArgs e)
        {

            //coppy for use -- module bind differtnt
            try
            {
                FeedbackFormEntity en_mod = new FeedbackFormEntity();
                en_mod.ddlDepartment = Convert.ToInt32(ddlDepartment.SelectedValue);
                DataTable ds_mod = new DataTable();
                GetDataFromAPI obj_apicall = new GetDataFromAPI();
                ds_mod = obj_apicall.getData(en_mod);
                if (ds_mod.Rows.Count > 0)
                {
                    ddlModuleName.DataSource = ds_mod;
                    ddlModuleName.DataTextField = "mod_name";
                    ddlModuleName.DataValueField = "mod_id";
                    ddlModuleName.DataBind();
                    //ddlModuleName.Items.Insert(0, new ListItem("-- Select Module --", "0"));
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("Error in  ddlDepartment_SelectedIndex/getdata() ddlModuleName: Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
        }

        //Author -> Saddam Shaikh
        //This method will clear form data after successfull submit operation.
        private void clearTextBox()
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            ddlModuleName.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlTrainer.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlMode.SelectedIndex = 0;
            txtEnjoyMostAboutTrainingA.Text = "";
            txtEnjoyMostAboutTrainingB.Text = "";
            txtEnjoyMostAboutTrainingC.Text = "";
            txtImprove.Text = "";
            txtInformationSkills.Text = "";
            txtComments.Text = "";
            ddl_ques1.SelectedIndex = 0;
            ddl_ques2.SelectedIndex = 0;
            ddl_ques3.SelectedIndex = 0;
            ddl_ques4.SelectedIndex = 0;
            ddl_ques5.SelectedIndex = 0;
            ddl_ques6.SelectedIndex = 0;
            ddl_ques7.SelectedIndex = 0;
            ddl_ques8.SelectedIndex = 0;
            ddl_ques9.SelectedIndex = 0;
            ddl_ques10.SelectedIndex = 0;
        }
    }
}