<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateListing.aspx.cs" Inherits="Web2.CreateListing" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>הצעת קלף למכירה - TCG Market</title>
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

    <div class="container mb-5">
        <div class="tcg-auth-card" style="max-width:720px;">
            <h3>הצעת קלף למכירה</h3>
            <form id="form1" runat="server" enctype="multipart/form-data">
                <div class="mb-3">
                    <label class="form-label">חיפוש קלף לפי שם</label>
                    <div class="input-group">
                        <asp:TextBox ID="TxtCardSearch" runat="server" CssClass="form-control" />
                        <asp:Button ID="BtnSearch" runat="server" Text="חפש" CssClass="btn btn-outline-primary" OnClick="BtnSearch_Click" />
                    </div>
                </div>

                <asp:Panel ID="PnlCreate" runat="server" Visible="false">
                    <div class="mb-3">
                        <label class="form-label">בחר קלף מהתוצאות</label>
                        <asp:DropDownList ID="DdlCards" runat="server" CssClass="form-select" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">כמות להצעה</label>
                        <asp:TextBox ID="TxtQty" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">תמונה של הקלף (אופציונלי)</label>
                        <asp:FileUpload ID="FileCardPhoto" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">הערות על מצב הקלף</label>
                        <asp:TextBox ID="TxtNotes" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" />
                    </div>
                    <div class="d-grid mb-3">
                        <asp:Button ID="BtnCreateListing" runat="server" Text="פרסם למכירה" CssClass="btn btn-success" OnClick="BtnCreateListing_Click" />
                    </div>
                </asp:Panel>

                <asp:Literal ID="LtlMsg" runat="server" />
            </form>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/tinymce/6.8.3/tinymce.min.js" referrerpolicy="origin"></script>
    <script>
        tinymce.init({
            selector: '#<%= TxtNotes.ClientID %>',
            height: 200,
            menubar: false,
            plugins: 'lists link',
            toolbar: 'bold italic underline | bullist numlist | link'
        });
    </script>
</body>
</html>