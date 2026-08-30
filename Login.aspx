<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web2.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
     <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous" />
</head>
<body>
     <form id="form1" runat="server">
     טלפון:  <asp:TextBox ID="TxtUser" runat="server" />
          סיסמה:  <asp:TextBox ID="TxtPass" runat="server" TextMode="Password" />
         <asp:Button id="BtnLogin" runat="server" Text="התחבר" onclick="BtnLogin_Click" />
         <br />
<a href="Reg.aspx">אין לך חשבון? הירשם כאן</a>
 
    <asp:Literal ID="LtlMsg" runat="server" />

         </form>

   
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>

</body>
</html>