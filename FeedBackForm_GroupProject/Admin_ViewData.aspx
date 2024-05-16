<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/OptimumPage.Master" CodeBehind="Admin_ViewData.aspx.cs" Inherits="FeedBackForm_GroupProject.Admin_ViewData" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</asp:Content>
        <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
            <div class="row" style="margin-top:60px">
                
                <div class="col-sm-6">
                    <asp:DropDownList runat="server" class="btn btn-success dropdown-toggle" data-toggle="dropdown" ID="ddl_department" ValidateRequestMode="Enabled" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                   
                    <asp:DropDownList runat="server" class="btn btn-Warning dropdown-toggle" data-toggle="dropdown" Visible="false" ID="ddl_module" ValidateRequestMode="Enabled" OnSelectedIndexChanged="ddl_module_SelectedIndexChanged" AutoPostBack="true">
                     </asp:DropDownList>
                    </div>
             
            </div>

            <div style="margin-top:30px">
                <asp:ListView ID="vie_empFeedback" runat="server" OnItemCommand="vie_empFeedback_ItemCommand">
                    <LayoutTemplate>
                        <table id="tbl1" class="table table-striped table-dark" runat="server">
                            <tr runat="server" class="thead-dark">

                                <th scope="col" runat="server">Trainee ID</th>
                                <th scope="col" runat="server">Module Name</th>
                                <th scope="col" runat="server">Training Duration(Day)</th>
                                <th scope="col" runat="server">Action</th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <tr runat="server">
                            <td>
                                <asp:Label ID="id_label" scope="row" runat="server" Text='<%# Eval("trainee_id") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lel_email" scope="row" runat="server" Text='<%# Eval("mod_name") %>' />
                            </td>
                            <td>
                                <asp:Label ID="email_label" scope="row" runat="server" Text='<%# Eval("duration") %>' />
                            </td>
                            <td class="btn-group-sm">
                                <asp:LinkButton ID="ViewButton" scope="row" runat="server" Text="View Details" CommandName="View" CommandArgument='<%# Eval("trainee_id") %>' />                    
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="emptyTable">
                            <tr>
                                <td></br>
                                        <asp:Image ID="NoDataImage"
                                            ImageUrl="Image/NoDataFound.jpg"
                                            runat="server" Width="500px" Height="500px" /></br>
                                </td>
                                <td>No records available.
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            </div>
      
</asp:Content>

