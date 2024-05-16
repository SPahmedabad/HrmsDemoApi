<%@ Page Title="" Language="C#" MasterPageFile="~/OptimumPage.Master" AutoEventWireup="true" CodeBehind="MIS_Report.aspx.cs" Inherits="FeedBackForm_GroupProject.MIS_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 style="text-align: center;">MIS Report</h1>
    <div class="row">
        <div class="col-sm-6">
            <asp:DropDownList runat="server" ID="ddl_dept" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div class="col-sm-6">
            <asp:DropDownList runat="server" ID="ddl_module" OnSelectedIndexChanged="ddl_module_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
            </asp:DropDownList><br>
        </div>
    </div>
    <br>
    <asp:ListView ID="dept_mis_lv" runat="server">
        <LayoutTemplate>
            <table id="tbl1" width="640px" border="1" style="text-align: center; border: 2px solid blue;" runat="server">
                <tr runat="server" style="background-color: #98FB98">

                    <th style="text-align: center" runat="server">Module Name</th>
                    <th style="text-align: center" runat="server">Trainee ID</th>
                    <th style="text-align: center" runat="server">Employee Name</th>
                    <th style="text-align: center" runat="server">Rating</th>

                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>

        <ItemTemplate>
            <tr runat="server">

                <td>
                    <asp:Label ID="id_label" runat="server" Text='<%# Eval("mod_name") %>' />
                </td>
                <td>
                    <asp:Label ID="lel_email" runat="server" Text='<%# Eval("trainee_id") %>' />
                </td>
                <td>
                    <asp:Label ID="email_label" runat="server" Text='<%# Eval("emp_name") %>' />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("rating") %>' />
                </td>
            </tr>
        </ItemTemplate>


    </asp:ListView>




    <asp:ListView ID="modulelv" Visible="false" runat="server">
        <LayoutTemplate>
            <table id="tbl1" width="640px" border="1" style="text-align: center; border: 2px solid blue;" runat="server">
                <tr runat="server" style="background-color: #98FB98">

                    <th style="text-align: center" runat="server">Module Name</th>
                    <th style="text-align: center" runat="server">Employee Name</th>
                    <th style="text-align: center" runat="server">Rating</th>

                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr runat="server">
                <%-- <td>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("mod_id") %>' />
                </td>--%>
                <td>
                    <asp:Label ID="id_label" runat="server" Text='<%# Eval("mod_name") %>' />
                </td>

                <td>
                    <asp:Label ID="email_label" runat="server" Text='<%# Eval("emp_name") %>' />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ratings") %>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <br>
    <br>

    <asp:Button ID="btnexptoexc" runat="server" CssClass="btn btn-primary" Text="Export-To-Excel" OnClick="btnexptoexc_Click" Visible="false" />


</asp:Content>
