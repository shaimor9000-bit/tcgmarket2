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

        <div class="row mt-3">
            <div class="col-md-6">
                <input type="text" id="SearchBox" class="form-control" placeholder="חיפוש קלף לפי שם..." />
            </div>
        </div>

        <div class="mt-3">
            <div id="LoadingMsg" class="alert alert-secondary">טוען קלפים...</div>
            <div id="EmptyMsg" class="alert alert-info" style="display:none;">אין עדיין קלפים למכירה</div>
            <div id="ListingsContainer" class="row row-cols-1 row-cols-md-3 g-4 mt-1"></div>
        </div>
    </div>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>

    <script>
        var allListings = [];

        function toWhatsAppLink(phone, cardName) {
            if (!phone) return "#";
            var digits = phone.replace(/\D/g, "");
            if (digits.charAt(0) === "0") { digits = "972" + digits.substring(1); }
            var message = "היי, אני מתעניין בפריט שהצעת למכירה: " + cardName;
            return "https://wa.me/" + digits + "?text=" + encodeURIComponent(message);
        }

        function renderListings(list) {
            var container = $("#ListingsContainer");
            container.empty();

            if (!list || list.length === 0) {
                $("#EmptyMsg").show();
                return;
            }
            $("#EmptyMsg").hide();

            $.each(list, function (i, item) {
                var img = item.photoUrl ? item.photoUrl : item.cardImageUrl;
                var card =
                    '<div class="col">' +
                    '  <div class="card h-100">' +
                    '    <img src="' + img + '" class="card-img-top" style="max-height:260px;object-fit:contain;" />' +
                    '    <div class="card-body">' +
                    '      <h5 class="card-title">' + item.cardName + '</h5>' +
                    '      <p class="card-text">משחק: ' + item.cardGame + '</p>' +
                    '      <p class="card-text">כמות זמינה: ' + item.quantity + '</p>' +
                    '      <div class="card-text">' + (item.notes || '') + '</div>' +
                    '      <hr />' +
                    '      <p class="card-text mb-1">מוכר: ' + item.sellerName + '</p>' +
                    '      <p class="card-text mb-1">טלפון: ' + item.sellerPhone + '</p>' +
                    '      <p class="card-text mb-2">מייל: ' + item.sellerEmail + '</p>' +
                    '     ' < a href = "' + toWhatsAppLink(item.sellerPhone, item.cardName) + '" target = "_blank" class="btn btn-success btn-sm" > צור קשר בוואטסאפ</a >' +
                    '    </div>' +
                    '  </div>' +
                    '</div>';
                container.append(card);
            });
        }

        $(function () {
            $.getJSON("api/listings", function (data) {
                $("#LoadingMsg").hide();
                allListings = data;
                renderListings(allListings);
            }).fail(function () {
                $("#LoadingMsg").hide();
                $("#EmptyMsg").text("שגיאה בטעינת הקלפים, נסה שוב מאוחר יותר").show();
            });

            $("#SearchBox").on("input", function () {
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