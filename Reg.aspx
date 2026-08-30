<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="Web2.Reg" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>הרשמה למערכת - TCG Market</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
    <link href="https://fonts.googleapis.com/css2?family=Heebo:wght@400;500;700;800&display=swap" rel="stylesheet" />
    <link href="Site.css" rel="stylesheet" />
</head>
<body>
    <header class="tcg-header">
        <div class="container">
            <a href="default.aspx" class="tcg-brand">🃏 TCG Market</a>
        </div>
    </header>

    <div class="tcg-auth-card" style="max-width:640px;">
        <h3>הרשמה למערכת</h3>
        <a href="Login.aspx" class="tcg-auth-switch">כבר יש לך חשבון? התחבר כאן</a>

        <form id="form1" runat="server">
            <div class="mb-3">
                <label class="form-label">שם מלא</label>
                <asp:TextBox ID="TxtFullname" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="ReqFully" runat="server" ControlToValidate="TxtFullname" ErrorMessage="חובה להזין שם מלא" CssClass="text-danger" />
            </div>

            <div class="mb-3">
                <label class="form-label">כתובת</label>
                <asp:TextBox ID="TxtAddress" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">תאריך לידה (DD/MM/YYYY)</label>
                <asp:TextBox ID="TxtYear" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="RangeValidator1" runat="server" ErrorMessage="יש להזין תאריך לידה בפורמט DD/MM/YYYY" ControlToValidate="TxtYear" ValidationExpression="\d{2}/\d{2}/\d{4}" Text="*" CssClass="text-danger" />
            </div>

            <div class="mb-3">
                <label class="form-label">טלפון</label>
                <asp:TextBox ID="TxtPhone" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="טלפון אינו תקין" Text="*" ControlToValidate="TxtPhone" ValidationExpression="05[0-9][1-9][0-9]{6}" CssClass="text-danger" />
            </div>

            <div class="mb-3">
                <label class="form-label">מייל</label>
                <asp:TextBox ID="TxtUser" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label">סיסמה</label>
                <asp:TextBox ID="TxtPass" runat="server" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="חובה להזין סיסמה" ControlToValidate="TxtPass" Text="*" CssClass="text-danger"/>
            </div>

            <div class="mb-3">
                <label class="form-label">וידוא סיסמה</label>
                <asp:TextBox ID="TxtPassValid" runat="server" TextMode="Password" CssClass="form-control" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="סיסמה ווידוא אינן תואמות" ControlToValidate="TxtPassValid" ControlToCompare="TxtPass" CssClass="text-danger"/>
            </div>

            <div class="mb-3">
                <label class="form-label">עיר</label>
                <asp:DropDownList ID="DDLCity" runat="server" CssClass="form-select">
                    <asp:ListItem Text="אשדוד" Value="10" />
                    <asp:ListItem Text="יבנה" Value="20" />
                    <asp:ListItem Text="תל אביב" Value="30" />
                    <asp:ListItem Text="ירושלים" Value="40" />
                </asp:DropDownList>
            </div>

            <div class="d-grid mb-3">
                <asp:Button ID="BtnReg" runat="server" Text="הרשמה" CssClass="btn btn-primary" OnClick="BtnReg_Click" />
            </div>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="text-danger" />

            <asp:Literal ID="LtlMsg" runat="server" />
        </form>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>