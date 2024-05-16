<%@ Page Language="C#" MasterPageFile="~/OptimumPage.Master" AutoEventWireup="true" CodeBehind="Feedback_send_mail.aspx.cs" Inherits="FeedBackForm_GroupProject.Feedback_send_mail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.24/css/jquery.dataTables.min.css" />
    <script src="https://cdn.datatables.net/1.10.24/js/jquery.dataTables.min.js"></script>


    <style>
        .dataTables_wrapper .dataTables_filter input {
            border: 1px solid #aaa;
            border-radius: 3px;
            padding: 5px;
            background-color: transparent;
            margin-left: 3px;
            margin-bottom: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-4">
            <asp:DropDownList runat="server" ID="ddl_emp" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddl_emp_SelectedIndexChanged" >
            </asp:DropDownList>
        </div>
        <div class="col-sm-4">
            <asp:DropDownList runat="server" ID="ddl_dept" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged" >
            </asp:DropDownList>
        </div>
        <div class="col-sm-4">
            <asp:DropDownList runat="server" ID="ddl_module" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddl_module_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
    <div style="margin-top: 40px" id="listdiv" runat="server" visible="false">
        <asp:ListView ID="list_employee" runat="server" OnSelectedIndexChanging="list_employee_SelectedIndexChanging" Style="margin-top: 40px">
            <LayoutTemplate>
                <table id="tbl_list">
                    <thead style="background-color: #3399ff; color: white" style="margin-top: 40px">
                        <th>Employee Name</th>
                        <th>Department </th>
                   
                        <th>Select employee</th>
                        <thead>
                    <tbody>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("emp_name")%> </td>
                    <td><%# Eval("dept_name")%></td>
                   
                    <td>
                        <asp:CheckBox ID="check_email" runat="server" ToolTip='<%# Eval("email") +"," + Eval("emp_name") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <div style="float: right; margin-top: 50px">
                <asp:CheckBox ID="check_all" AutoPostBack="true" Text="Select All"  class="form-check-input" OnCheckedChanged="check_all_CheckedChanged" runat="server" />
            <br />
            <div style="margin-top: 10px">
                <asp:Button ID="btn_submit" runat="server" Text="Send Mail" OnClick="btn_submit_Click" CssClass="btn btn-primary"/>
            </div>
        </div>

    </div>

    <%--author ravi vaghela--%>
    <%--datatable bind--%>
    <script>
        $(document).ready(function () {
            $('#tbl_list').DataTable({
                "pagingType": "full_numbers"
            });
        });
    </script>
</asp:Content>
