using Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FeedBackForm_GroupProject
{
    public partial class LoginAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    Session.Abandon();
                    Session.Clear();                
            }
            
        }

        //Author - Saddam Shaikh
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = ConfigurationManager.AppSettings["username"];
                string password = ConfigurationManager.AppSettings["password"];

                if (!(string.IsNullOrWhiteSpace(txtUsername.Text) && string.IsNullOrWhiteSpace(txtPassword.Text)))
                {
                    if (txtUsername.Text == username && txtPassword.Text == password)
                    {
                        Session["Login"] = true;
                        Response.Redirect("Admin_ViewData.aspx",false);
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid UserName & Password !!!')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                InsertLog.WriteErrorLog(ex.ToString());
            }


        }
    }
}