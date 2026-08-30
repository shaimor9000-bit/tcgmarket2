<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateListing.aspx.cs" Inherits="Web2.CreateListing" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>הצעת קלף למכירה</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous" />
</head>
<body>
    <div class="container mt-4">
        <h3>הצעת קלף למכירה</h3>
        <form id="form1" runat="server" enctype="multipart/form-data">
            <div class="row mb-3">
                <div class="col-3">חיפוש קלף לפי שם</div>
                <div class="col-6">
                    <asp:TextBox ID="TxtCardSearch" runat="server" CssClass="form-control" />
                </div>
                <div class="col-3">
                    <asp:Button ID="BtnSearch" runat="server" Text="חפש" CssClass="btn btn-outline-primary" OnClick="BtnSearch_Click" />
                </div>
            </div>

            <asp:Panel ID="PnlCreate" runat="server" Visible="false">
                <div class="row mb-3">
                    <div class="col-3">בחר קלף מהתוצאות</div>
                    <div class="col-6">
                        <asp:DropDownList ID="DdlCards" runat="server" CssClass="form-select" />
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-3">כמות להצעה</div>
                    <div class="col-6">
                        <asp:TextBox ID="TxtQty" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-3">תמונה של הקלף (אופציונלי)</div>
                    <div class="col-6">
                        <asp:FileUpload ID="FileCardPhoto" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-3">הערות על מצב הקלף</div>
                    <div class="col-6">
                        <asp:TextBox ID="TxtNotes" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" />
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-3">
                        <asp:Button ID="BtnCreateListing" runat="server" Text="פרסם למכירה" CssClass="btn btn-success" OnClick="BtnCreateListing_Click" />
                    </div>
                </div>
            </asp:Panel>

            <div class="row">
                <div class="col-9">
                    <asp:Literal ID="LtlMsg" runat="server" />
                </div>
            </div>
        </form>
        <a href="default.aspx">חזרה לעמוד הבית</a>
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