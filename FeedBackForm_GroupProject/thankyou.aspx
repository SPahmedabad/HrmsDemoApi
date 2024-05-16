<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="thankyou.aspx.cs" Inherits="FeedBackForm_GroupProject.thankyou" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx" crossorigin="anonymous"/>
    <script type="text/javascript">
        debugger;
        window.onload = window.history.forward(0);
    </script> 
<%--   <script type="text/javascript">
       debugger;
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="jumbotron text-center">
                <h1 class="display-3">Thank You!</h1>
                <p class="lead"><strong>Thank you for sharing your valuable feedback with us</strong></p>
                <hr>
                <p>
                    Having trouble? <a href="mailto:Juhi.s@optimumfintech.co.in">Contact Us</a>
                </p>

            </div>
        </div>
    </form>
</body>
</html>
