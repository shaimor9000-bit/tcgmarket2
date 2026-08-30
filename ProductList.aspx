<%@ Page Title="קלפים למכירה" Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Web2.ProductList" %>

<!DOCTYPE html>
<html dir="rtl" lang="he">
<head runat="server">
    <meta charset="utf-8" />
    <title>קלפים למכירה - TCG Market</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2>קלפים למכירה</h2>
            <a href="Default.aspx">חזרה לעמוד הבית</a>
        </div>

        <input type="text" id="SearchBox" class="form-control mb-3" placeholder="חיפוש קלף לפי שם..." />

        <div id="ListingsContainer" class="row">
            <p class="text-muted">טוען קלפים...</p>
        </div>
    </div>
    </form>

    <script>
        var allListings = [];

        function toWhatsAppLink(phone, cardName, imageUrl) {
            if (!phone) return "#";
            var digits = phone.replace(/\D/g, "");
            if (digits.charAt(0) === "0") { digits = "972" + digits.substring(1); }
            var message = "היי, אני מתעניין בפריט שהצעת למכירה: " + cardName;
            if (imageUrl) {
                var fullImageUrl = imageUrl.indexOf("http") === 0 ? imageUrl : window.location.origin + imageUrl;
                message += "\n" + fullImageUrl;
            }
            return "https://wa.me/" + digits + "?text=" + encodeURIComponent(message);
        }

        var waLink = toWhatsAppLink(item.sellerPhone, item.cardName, item.photoUrl ? item.photoUrl : item.cardImageUrl); {
            var img = item.photoUrl ? item.photoUrl : item.cardImageUrl;
            var waLink = toWhatsAppLink(item.sellerPhone, item.cardName);

            return `
                <div class="col-md-4 mb-4">
                    <div class="card h-100">
                        <img src="${img}" class="card-img-top" style="max-height:200px;object-fit:contain;" />
                        <div class="card-body">
                            <h5 class="card-title">${item.cardName}</h5>
                            <p class="card-text text-muted">${item.cardGame}</p>
                            <p class="card-text">כמות זמינה: ${item.quantity}</p>
                            <p class="card-text">${item.notes ? item.notes : ""}</p>
                            <p class="card-text"><small class="text-muted">מוכר: ${item.sellerName}</small></p>
                            <a href="${waLink}" target="_blank" class="btn btn-success btn-sm">צור קשר בוואטסאפ</a>
                        </div>
                    </div>
                </div>
            `;
        }

        function renderListings(list) {
            var container = $("#ListingsContainer");
            container.empty();

            if (!list || list.length === 0) {
                container.append('<p class="text-muted">לא נמצאו קלפים.</p>');
                return;
            }

            for (var i = 0; i < list.length; i++) {
                container.append(cardHtml(list[i]));
            }
        }

        $(document).ready(function () {
            $.getJSON("api/listings", function (data) {
                allListings = data;
                renderListings(allListings);
            }).fail(function () {
                $("#ListingsContainer").html('<p class="text-danger">שגיאה בטעינת הקלפים.</p>');
            });

            $("#SearchBox").on("keyup", function () {
                var term = $(this).val().toLowerCase();
                var filtered = allListings.filter(function (item) {
                    return item.cardName && item.cardName.toLowerCase().indexOf(term) !== -1;
                });
                renderListings(filtered);
            });
        });
    </script>
</body>
</html>