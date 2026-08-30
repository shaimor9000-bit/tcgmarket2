using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using MongoDB.Bson;

namespace Web2
{
    public partial class CreateListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null) // רק משתמש מחובר יכול להציע קלפים למכירה
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            var term = TxtCardSearch.Text.Trim();
            var results = Card.SearchByName(term);

            DdlCards.Items.Clear();

            if (results.Count == 0)
            {
                LtlMsg.Text = "<div class='alert alert-warning'>לא נמצאו קלפים תואמים</div>";
                PnlCreate.Visible = false;
                return;
            }

            foreach (var card in results)
                DdlCards.Items.Add(new ListItem(card.Name + " (" + card.Game + ")", card.Id.ToString()));

            PnlCreate.Visible = true;
            LtlMsg.Text = "";
        }

        protected void BtnCreateListing_Click(object sender, EventArgs e)
        {
            int qty;
            if (!int.TryParse(TxtQty.Text.Trim(), out qty) || qty < 1)
            {
                LtlMsg.Text = "<div class='alert alert-danger'>כמות אינה תקינה</div>";
                return;
            }

            if (DdlCards.Items.Count == 0)
            {
                LtlMsg.Text = "<div class='alert alert-danger'>יש לבחור קלף קודם</div>";
                return;
            }

            var cardId = ObjectId.Parse(DdlCards.SelectedValue);
            var card = Card.GetById(cardId);
            if (card == null)
            {
                LtlMsg.Text = "<div class='alert alert-danger'>הקלף שנבחר לא נמצא, נסה שוב</div>";
                return;
            }

            string photoUrl = null;
            if (FileCardPhoto.HasFile)
            {
                var ext = Path.GetExtension(FileCardPhoto.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                {
                    LtlMsg.Text = "<div class='alert alert-danger'>ניתן להעלות רק תמונות jpg/png</div>";
                    return;
                }

                var folder = Server.MapPath("~/UploadedImages");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + ext;
                FileCardPhoto.SaveAs(Path.Combine(folder, fileName));
                photoUrl = "~/UploadedImages/" + fileName;
            }

            var user = (User)Session["Login"];

            var listing = new Listing
            {
                SellerId = user.Id,
                SellerName = user.FullName,
                SellerPhone = user.Phone,
                SellerEmail = user.Email,
                CardId = card.Id,
                CardName = card.Name,
                CardGame = card.Game,
                CardImageUrl = card.ImageUrl,
                Quantity = qty,
                PhotoUrl = photoUrl,
                Notes = TxtNotes.Text
            };

            if (listing.Create())
            {
                LtlMsg.Text = "<div class='alert alert-success'>הקלף פורסם למכירה בהצלחה</div>";
                DdlCards.Items.Clear();
                PnlCreate.Visible = false;
                TxtCardSearch.Text = "";
                TxtQty.Text = "";
                TxtNotes.Text = "";
            }
            else
            {
                LtlMsg.Text = "<div class='alert alert-danger'>קרתה תקלה, נסה שוב מאוחר יותר</div>";
            }
        }
    }
}