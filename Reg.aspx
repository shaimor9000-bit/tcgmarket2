<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="Web2.Reg" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>הרשמה למערכת</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
</head>
<body>
    <div class="container mt-4">
        <div class="row">
            <div class="col-12">
                <a href="Login.aspx">כבר יש לך חשבון? התחבר כאן</a>
                <form id="form1" runat="server">
                    <div class="row">
                        <div class="col-3">שם מלא</div>
                        <div class="col-6">
                            <asp:TextBox ID="TxtFullname" runat="server" />
                            <asp:RequiredFieldValidator ID="ReqFully" runat="server" ControlToValidate="TxtFullname" ErrorMessage="חובה להזין שם מלא" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">כתובת</div>
                        <div class="col-6">
                            <asp:TextBox ID="TxtAddress" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">תאריך לידה (DD/MM/YYYY)</div>
                        <div class="col-6">
                            <asp:TextBox ID="TxtYear" runat="server" />
                            <asp:RegularExpressionValidator ID="RangeValidator1" runat="server" ErrorMessage="יש להזין תאריך לידה בפורמט DD/MM/YYYY" ControlToValidate="TxtYear" ValidationExpression="\d{2}/\d{2}/\d{4}" Text="*" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">טלפון</div>
                        <div class="col-6">
                            <asp:TextBox ID="TxtPhone" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="טלפון אינו תקין" Text="*" ControlToValidate="TxtPhone" ValidationExpression="05[0-9][1-9][0-9]{6}" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">מייל</div>
                        <div class="col-6">
                            <asp:TextBox ID="TxtUser" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">סיסמה</div>
                        <div class="col-6">
                            <asp:TextBox ID="TxtPass" runat="server" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="חובה להזין סיסמה" ControlToValidate="TxtPass" Text="*"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">וידוא סיסמה</div>
                        <div class="col-6">
                            <asp:TextBox ID="TxtPassValid" runat="server" TextMode="Password" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="סיסמה ווידוא אינן תואמות" ControlToValidate="TxtPassValid" ControlToCompare="TxtPass"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">עיר</div>
                        <div class="col-6">
                            <asp:DropDownList ID="DDLCity" runat="server">
                                <asp:ListItem Text="אשדוד" Value="10" />
                                <asp:ListItem Text="יבנה" Value="20" />
                                <asp:ListItem Text="תל אביב" Value="30" />
                                <asp:ListItem Text="ירושלים" Value="40" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">
                            <asp:Button ID="BtnReg" runat="server" Text="הרשמה" CssClass="btn btn-outline-success" OnClick="BtnReg_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowMessageBox="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">
                            <asp:Literal ID="LtlMsg" runat="server" />
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>