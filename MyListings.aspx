<%@ Page Title="הפריטים שלי" Language="C#" AutoEventWireup="true" CodeBehind="MyListings.aspx.cs" Inherits="Web2.MyListings" ValidateRequest="false" %>

<!DOCTYPE html>
<html dir="rtl" lang="he">
<head runat="server">
    <meta charset="utf-8" />
    <title>הפריטים שלי - TCG Market</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div class="container mt-4">
        <h2 class="mb-4">הפריטים שאני מוכר</h2>

        <asp:Literal ID="LtlMessage" runat="server" />

        <asp:Repeater ID="RptMyListings" runat="server" OnItemCommand="RptMyListings_ItemCommand">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="row g-0">
                        <div class="col-md-2 text-center p-2">
                            <img src='<%# GetDisplayImage(Eval("PhotoUrl"), Eval("CardImageUrl")) %>' class="img-fluid rounded" style="max-height:120px;" />
                        </div>
                        <div class="col-md-10">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("CardName") %> <small class="text-muted">(<%# Eval("CardGame") %>)</small></h5>

                                <div class="row g-2 align-items-end">
                                    <div class="col-auto">
                                        <label class="form-label">כמות</label>
                                        <asp:TextBox ID="TxtEditQty" runat="server" CssClass="form-control" Text='<%# Eval("Quantity") %>' TextMode="Number" style="width:100px;" />
                                    </div>
                                    <div class="col">
                                        <label class="form-label">הערות</label>
                                        <asp:TextBox ID="TxtEditNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" Text='<%# Eval("Notes") %>' />
                                    </div>
                                    <div class="col-auto">
                                        <asp:Button runat="server" Text="שמור" CssClass="btn btn-primary" CommandName="Save" CommandArgument='<%# Eval("Id") %>' />
                                    </div>
                                    <div class="col-auto">
                                        <asp:Button runat="server" Text="מחק" CssClass="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('למחוק את הפריט?');" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Literal ID="LtlEmpty" runat="server" />
    </div>
    </form>
</body>
</html>