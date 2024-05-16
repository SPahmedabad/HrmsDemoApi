<%@ Page Title="" Language="C#" MasterPageFile="~/OptimumPage.Master" AutoEventWireup="true" CodeBehind="FeedbackForm.aspx.cs" Inherits="FeedBackForm_GroupProject.FeedbackForm" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <script type="text/javascript">
        debugger;
        window.onload = window.history.forward(0);


        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            
            $('#<%=ddlState.ClientID %>').change(function () {
                var selectedItem = $('option:selected', $(this)).text();
                var ddlCity = $('#<%=ddlCity.ClientID %>');
                //alert(selectedItem);
                if (selectedItem === "Select State") {
                    ddlCity.find('option').not(':first').remove();
                }
                else if (selectedItem === "Gujarat") {
                    var myOptions = {
                        val1: 'Ahmedabad',

                    };
                    ddlCity.find('option').not(':first').remove();
                    $.each(myOptions, function (val, text) {
                        ddlCity.append(
                            $('<option></option>').val(text).html(text)
                        );
                    });

                }
                else if (selectedItem === "Maharashtra") {
                    var myOptions = {
                        val1: 'Mumbai',
                        val2: 'Pune'
                    };
                 ddlCity.find('option').not(':first').remove();

                    $.each(myOptions, function (val, text) {
                        ddlCity.append(
                            $('<option></option>').val(text).html(text)
                        );
                    });
                }
            });
           
              
            $('#<%=ddlCity.ClientID %>').change(function () {
                
                    $('#<%=hidDdl_City.ClientID %>').val(this.value);
                });
           
             $('#<%=btnSubmit.ClientID%>').click(function () {
                
                var startdate = $('#<%=txtStartDate.ClientID%>').val();
                var enddate = $('#<%=txtEndDate.ClientID%>').val();
                if (new Date(startdate) > new Date(enddate)) {//compare end <=, not >=
                    //your code here
                   
                    $('#<%=txtStartDate.ClientID%>').val('');
                    $('#<%=txtEndDate.ClientID%>').val('');
                    alert("Start date cannot be greater then End date..");
                }
            })
        });
    </script>

    <style type="text/css">
        
        #lblUserName {
            float: right;
            padding-right: 20px;
        }
        .auto-style3 {
            display: block;
            width: 100%;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-clip: padding-box;
            -webkit-appearance: none;
            -moz-appearance: none;
            border-radius: .25rem;
            transition: none;
            border: 1px solid #ced4da;
            margin-bottom: 23;
            background-color: #fff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidDdl_City" runat="server" />
    <div id="header">
        <h1 style="text-align: center; font-style: oblique; padding-top: 40px">Training Evaluation Form  !!!</h1>
        <ul>
            <li>
                <h6 style="font-style: normal; padding-top: 40px">Please share honest feedback for us so we can improve.</h6>
            </li>
        </ul>
        <asp:Label ID="lblUserName" Visible="false" runat="server" Text="Label"></asp:Label>
    </div>
    <div class="accordion" style="padding-top: 30px" id="accordionExample">
        <div class="accordion-item">
            <h2 class="accordion-header" id="headingOne">
                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    About Training</button>
            </h2>
            <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                <div class="accordion-body">
                    <div class="form-group">
                        <div class="row">
                               <div class="col-sm-4">
                                    <asp:Label runat="server"><strong>1. Department Name : </strong></asp:Label>
                                    <asp:DropDownList ID="ddlDepartment"  runat="server" CssClass="form-control" Width="400px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged2" AutoPostBack="true">
                                        <asp:ListItem Text="--Select Department--" Value="Select"></asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                    <asp:Label runat="server"><strong>2. Module Name : </strong></asp:Label>
                                    <asp:DropDownList ID="ddlModuleName" runat="server" CssClass="form-control" Width="400px">
                                        <asp:ListItem Text="--Select Module--" Value="Select"></asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                    <asp:Label runat="server"><strong>3. Who facilitated this training ?</strong></asp:Label>
                                    <asp:DropDownList ID="ddlTrainer" runat="server" CssClass="form-control" Width="400px">
                                        <asp:ListItem Text="--Select Trainer--" Value="Select"></asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-6">
                                <br />
                                <asp:Label runat="server"><strong>4. Start Date : </strong></asp:Label>
                                <asp:TextBox ID="txtStartDate"  TextMode="Date"  CssClass="form-control" runat="server" Width="400px"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <br />
                                <asp:Label runat="server"><strong>5.End Date:</strong></asp:Label>
                                <asp:TextBox ID="txtEndDate" TextMode="Date" CssClass="form-control" runat="server" Width="400px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                   <div class="form-group">
                    <div class="row">
                        <div class="col-sm-6">
                            <br />
                            <asp:Label runat="server"><strong>6. Where did the training happen ?</strong></asp:Label>
                            <br />
                             <asp:Label runat="server"><i>State : </i></asp:Label>
                             <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server" Class="form-control" Width="422px">
                                 <asp:ListItem  Text="Select State" Value="Select " ></asp:ListItem>
                                 <asp:ListItem  Text="Gujarat" Value="Gujarat"></asp:ListItem>
                                 <asp:ListItem  Text="Maharashtra" Value="Maharashtra"></asp:ListItem>

                             </asp:DropDownList>
                        </div>
                         <div class="col-sm-6">
                             <br />
                             <br />
                             <asp:Label runat="server"><i>City : </i></asp:Label>
                              <asp:DropDownList ID="ddlCity" CssClass="form-control" runat="server" Class="form-control" Width="422px">
                                 <asp:ListItem  Text="Select City" Value="Select" ></asp:ListItem>
                                  
                             </asp:DropDownList>                        
                        </div>
                    </div> 
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label runat="server"><strong>7. Mode of training (Online / Offline) : </strong></asp:Label>
                       <asp:DropDownList ID="ddlMode" runat="server" CssClass="form-control" Width="422px">
                           <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                              <asp:ListItem Text="Online" Value="y"></asp:ListItem>
                              <asp:ListItem Text="Offline" Value="n"></asp:ListItem>
                       </asp:DropDownList>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label runat="server"><strong>8. What 3 things did you enjoy most about the training ?</strong></asp:Label>
                        <table class="table">
                            <tr>
                                <td>
                                    <asp:Label runat="server">A. </asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtEnjoyMostAboutTrainingA" CssClass="form-control" runat="server" Width="422px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server">B. </asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtEnjoyMostAboutTrainingB" CssClass="form-control" runat="server" Width="422px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server">C. </asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtEnjoyMostAboutTrainingC" CssClass="form-control" runat="server" Width="422px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label runat="server"><strong>9. How can we improve training ? </strong></asp:Label>
                        <asp:TextBox ID="txtImprove" CssClass="form-control" TextMode="MultiLine" runat="server" Width="422px"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label runat="server"><strong>10. What information / skills did you learn that you will always apply at work ? </strong></asp:Label>
                        <asp:TextBox ID="txtInformationSkills" CssClass="form-control" TextMode="MultiLine" runat="server" Width="422px"></asp:TextBox>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <div class="accordion-item">
            <h2 class="accordion-header" id="headingTwo">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                    Ratings</button>
            </h2>
            <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                <div class="accordion-body">

                    <div class="form-group">
                        <asp:Label runat="server"><strong>(0- No experience/ can’t say, 1- Dissatisfied, 2- Somewhat satisfied, 3- Satisfied, and 4- Very Satisfied)? </strong></asp:Label>
                        <br />
                        <br />
                        <asp:Label runat="server"><strong>11. How satisfied are you with training overall ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques1" runat="server" CssClass="form-control" Width="422px" required="required">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>12. Relevance of training to your need ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques2" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>13. Interactivity of the training ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques3" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>14. Quality of information shared ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques4" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>15. Amount of information shared ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques5" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>16.  Pace of the training ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques6" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>17. Style of trainer’s delivery ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques7" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>18. Ability of trainer to engage group ?</strong></asp:Label>
                        <asp:DropDownList ID="ddl_ques8" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>19.  Ability of trainer to deliver information in a meaningful manner ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques9" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label runat="server"><strong>20.Ability of trainer to maintain and establish a positive training environment ?</strong> </asp:Label>
                        <asp:DropDownList ID="ddl_ques10" runat="server" CssClass="form-control" Width="422px">
                            <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <div class="accordion-item">
            <h2 class="accordion-header" id="headingThree">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                    Comments
                </button>
            </h2>
            <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                <div class="accordion-body">

                    <div class="form-group">
                        <asp:Label runat="server"><strong>COMMENTS AND FEEDBACK : </strong></asp:Label>
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-control" Width="422px"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="form-control" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>