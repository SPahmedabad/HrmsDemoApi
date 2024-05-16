<%@ Page Title="" Language="C#" MasterPageFile="~/OptimumPage.Master" AutoEventWireup="true" CodeBehind="MIS_Check_Status.aspx.cs" Inherits="FeedBackForm_GroupProject.MIS_Check_Status" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
    <h3 class="page-header"  >MIS Submit Status</h3>
    <asp:Label ID="lbl_status" runat="server" Text="Please Select Status"></asp:Label>
    <br />
     <asp:DropDownList ID="ddlcheck_status" runat="server"  OnSelectedIndexChanged="ddlcheck_status_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" Width="240px">
            <asp:ListItem Value="0">--Select Status--</asp:ListItem>
            <asp:ListItem Value="submit">Submited Data</asp:ListItem>
            <asp:ListItem Value="not_submit">Not submited Data</asp:ListItem>
        </asp:DropDownList><br /><br />

     <asp:ListView ID="lv_subdata" runat="server" >
        <LayoutTemplate>
            <table id="tbl1"  class="table table-striped table-light"   runat="server" border="3" >
                <tr runat="server" class="thead-dark">
                    <th scope="col" runat="server">Trainer_Name</th>
                    <th scope="col" runat="server">Module_Name</th>
                    <th scope="col" runat="server">Department_Name</th>
                    <th scope="col" runat="server">Submitted Count</th>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>

        <ItemTemplate>
            <tr runat="server">
                <td>
                    <asp:Label ID="lbl_empnm" scope="row" runat="server" Text='<%# Eval("emp_name") %>' />
                </td>
                <td>
                    <asp:Label ID="lbl_modnm" scope="row" runat="server" Text='<%# Eval("mod_name") %>' />
                </td>
                 <td>
                    <asp:Label ID="lbl_deptnm" scope="row" runat="server" Text='<%# Eval("dept_name") %>' />
                </td>
                <td>
                    <asp:Label ID="lbl_subcnt" scope="row" runat="server" Text='<%# Eval("Submitted") %>' />
                </td>
       
            </tr>
        </ItemTemplate>
    </asp:ListView>


    <asp:ListView ID="lv_notsubdata" runat="server" >
        <LayoutTemplate>
            <table id="tbl1"  class="table table-striped table-light"   runat="server" border="3" >
                <tr runat="server" class="thead-dark">
                    <th scope="col" runat="server">Trainer_Name</th>
                    <th scope="col" runat="server">Module_Name</th>
                    <th scope="col" runat="server">Department_Name</th>
                    <th scope="col" runat="server">Not Submitted Count</th>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>

        <ItemTemplate>
            <tr runat="server">
                <td>
                    <asp:Label ID="lbl_empnm" scope="row" runat="server" Text='<%# Eval("emp_name") %>' />
                </td>
                <td>
                    <asp:Label ID="lbl_modnm" scope="row" runat="server" Text='<%# Eval("mod_name") %>' />
                </td>
                 <td>
                    <asp:Label ID="lbl_deptnm" scope="row" runat="server" Text='<%# Eval("dept_name") %>' />
                </td>
                <td>
                    <asp:Label ID="lbl_subcnt" scope="row" runat="server" Text='<%# Eval("Not_Submitted") %>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
