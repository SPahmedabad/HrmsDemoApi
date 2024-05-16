<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Department_Employees.aspx.cs" MasterPageFile="~/OptimumPage.Master" Inherits="FeedBackForm_GroupProject.Add_Department_Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="margin-top: 80px">
        <div class="col-sm-6">
            <h2 class="text-primary">For Add Employee</h2>
            <hr />
            <asp:Label ID="lbl_dep_name" runat="server" Text="Department Name :"></asp:Label>
            <asp:DropDownList ID="ddl_dept" runat="server" CssClass="form-control"></asp:DropDownList>
            <br />

            <asp:Label ID="lbl_emp" runat="server" Text="Enter Employee Name : "></asp:Label>
            <asp:TextBox ID="txt_emp" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RegularExpressionValidator  ForeColor="Red" ID="revEmail" Display="Dynamic"
                ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txt_emp"
                runat="server"
                ErrorMessage="Only Letters Allowed"></asp:RegularExpressionValidator>
            <br />

            <asp:Label ID="lbl_join_date" runat="server" Text="Joining Date : "></asp:Label>
            <asp:TextBox ID="txt_join_date" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Label ID="lbl_is_active" runat="server" Text="Is Active ? : "> </asp:Label>
            <asp:DropDownList ID="ddl_is_active" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y" />
                <asp:ListItem Text="No" Value="N" />
            </asp:DropDownList>
            <br />

            <asp:Label ID="lbl_email" runat="server" Text="Employee Email : "></asp:Label>
            <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RegularExpressionValidator ID="rev_" runat="server" ControlToValidate="txt_email"
                ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                Display="Dynamic" ErrorMessage="Invalid email address" />
            <br />

            <asp:Button ID="btn_emp" runat="server" Text="Add Employee" CssClass="btn btn-primary" OnClick="btn_emp_Click"/>
        </div>
        <div class="col-sm-6">
            <h2 class="text-primary">For add Department</h2>
            <hr />
            <asp:Label ID="lbl_dept" runat="server" Text="Enter Training DepartMent Name :"></asp:Label>         
            <asp:TextBox ID="txt_dept" runat="server" CssClass="form-control"></asp:TextBox><br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic"
                 ForeColor="Red" ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txt_dept"
                runat="server"
                ErrorMessage="Only Letters Allowed">
            </asp:RegularExpressionValidator>
             
            <asp:Button ID="btn_dept" runat="server" Text="Add DepartMent" CssClass="btn btn-warning" OnClick="btn_dept_Click" />
        </div>
    </div>
</asp:Content>
