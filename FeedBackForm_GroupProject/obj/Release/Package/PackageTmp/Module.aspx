<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Module.aspx.cs" Inherits="FeedBackForm_GroupProject.Module" MasterPageFile="~/OptimumPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="accordion" style="padding-top: 30px" id="accordionExample">
        <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
            <div class="accordion-item">
                <h1 class="accordion-header" id="headingOne">Add New Module (*Please select department first)
                </h1>
                <div class="accordion-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-6">
                                
                                <asp:DropDownList ID="ddl_dept" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="-- Select Department --" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-6">
                                    <asp:TextBox ID="txt_add_mod" placeholder="Enter Module Name" CssClass="form-control" runat="server"></asp:TextBox><br />
          
                            </div>
                            <asp:Button ID="btn_add_mod" CssClass="form-control btn btn-primary" runat="server" Text="ADD" OnClick="btn_add_mod_Click" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
