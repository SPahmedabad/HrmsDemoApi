<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="FeedBackForm_GroupProject.LoginAdmin" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        .container {
            margin-top: 70px;
            position: relative;
            height: 400px;
        }

        .centered-element {
            margin: 0;
            padding-top: 100px;
            position: absolute;
            top: 50%;
            transform: translateY(-50%);
        }

        .hplSignUp{
            padding-left:50px;
        }
    </style>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
    <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
        media="screen" />
</head>
<body>
 <form id="form1" runat="server">
    <div class="container" style="max-width: 400px; align-self: center;border:1px solid 600px">
        <h2 class="text-primary">Login</h2>
        <hr />
        <div class="form-group">
            <label for="txtUsername">Username</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Username" required="required" />
        </div>
        <br />
        <div class="form-group">
            <label for="txtPassword">Password</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter Password" required="required" />
        </div>   
        <asp:Button ID="btnLogin" Text="Login" runat="server" Class="btn btn-primary" OnClick="btnLogin_Click" />
        <br />  
        <div id="dvMessage" runat="server" visible="false" class="alert alert-danger">
            <strong>Error!</strong>
        </div>
    </div>
   </form>
</body>
</html>

