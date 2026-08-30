<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Web2.ProductList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>קלפים למכירה</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous" />
</head>
<body>
    <div class="container mt-4">
        <h3>קלפים למכירה</h3>
        <a href="default.aspx">חזרה לעמוד הבית</a>

        <div class="mt-3">
            <asp:Literal ID="LtlEmpty" runat="server" />

            <asp:Repeater ID="RptListings" runat="server">
                <HeaderTemplate>
                    <div class="row row-cols-1 row-cols-md-3 g-4 mt-1">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100">
                            <img src='<%# Eval("CardImageUrl") %>' class="card-img-top" alt='<%# Eval("CardName") %>' style="max-height:260px;object-fit:contain;" />
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("CardName") %></h5>
                                <p class="card-text">משחק: <%# Eval("CardGame") %></p>
                                <p class="card-text">כמות זמינה: <%# Eval("Quantity") %></p>
                                <hr />
                                <p class="card-text mb-1">מוכר: <%# Eval("SellerName") %></p>
                                <p class="card-text mb-1">טלפון: <%# Eval("SellerPhone") %></p>
                                <p class="card-text mb-0">מייל: <%# Eval("SellerEmail") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>