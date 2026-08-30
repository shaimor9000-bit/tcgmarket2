<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web2.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>התחברות - TCG Market</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous" />
    <link href="https://fonts.googleapis.com/css2?family=Heebo:wght@400;500;700;800&display=swap" rel="stylesheet" />
    <link href="Site.css" rel="stylesheet" />
</head>
<body>
    <header class="tcg-header">
        <div class="container">
            <a href="default.aspx" class="tcg-brand">🃏 TCG Market</a>
        </div>
    </header>

    <form id="form1" runat="server">
        <div class="tcg-auth-card">
            <h3>התחברות</h3>

            <div class="mb-3">
                <label class="form-label">טלפון</label>
                <asp:TextBox ID="TxtUser" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">סיסמה</label>
                <asp:TextBox ID="TxtPass" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <div class="d-grid mb-3">
                <asp:Button ID="BtnLogin" runat="server" Text="התחבר" OnClick="BtnLogin_Click" CssClass="btn btn-primary" />
            </div>

            <a href="Reg.aspx" class="tcg-auth-switch">אין לך חשבון? הירשם כאן</a>

            <asp:Literal ID="LtlMsg" runat="server" />
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>