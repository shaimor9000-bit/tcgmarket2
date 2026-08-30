<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Web2._default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <meta charset="utf-8" />
    <title>TCG Market</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous" />
    <link href="https://fonts.googleapis.com/css2?family=Heebo:wght@400;500;700;800&display=swap" rel="stylesheet" />
    <link href="Site.css" rel="stylesheet" />
</head>
<body>
    <header class="tcg-header">
        <div class="container d-flex justify-content-between align-items-center flex-wrap">
            <a href="default.aspx" class="tcg-brand">🃏 TCG Market</a>
            <nav>
                <a href="ProductList.aspx">קלפים למכירה</a>
                <a href="CreateListing.aspx">הצע קלף</a>
                <a href="MyListings.aspx">ההצעות שלי</a>
            </nav>
        </div>
    </header>

    <form id="form1" runat="server">
        <div class="container">
            <div class="tcg-hero">
                <h1>ברוכים הבאים ל-TCG Market</h1>
                <p><asp:Literal ID="LtlUser" runat="server" /></p>
            </div>

            <div class="d-flex justify-content-center gap-2 flex-wrap mb-4">
                <a href="CreateListing.aspx" class="btn btn-primary btn-lg">הצע קלף למכירה</a>
                <a href="ProductList.aspx" class="btn btn-outline-primary btn-lg">עיין בקלפים למכירה</a>
                <a href="MyListings.aspx" class="btn btn-outline-secondary btn-lg">ההצעות שלי</a>
            </div>

            <div class="text-center mb-5">
                <asp:Button ID="BtnLogout" runat="server" Text="התנתק" CssClass="btn btn-outline-danger" OnClick="BtnLogout_Click" />
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>