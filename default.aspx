<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Web2._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <meta charset="utf-8" />
    <title>TCG Market</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <asp:Literal ID="LtlUser" runat="server" />
            <div class="mt-3">
  <a href="CreateListing.aspx" class="btn btn-primary">הצע קלף למכירה</a> <a href="ProductList.aspx" class="btn btn-outline-primary">עיין בקלפים למכירה</a> <a href="MyListings.aspx" class="btn btn-outline-secondary">ההצעות שלי</a>
</div>
            <div class="mt-3">
                <asp:Button ID="BtnLogout" runat="server" Text="התנתק" CssClass="btn btn-outline-danger" OnClick="BtnLogout_Click" />
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>