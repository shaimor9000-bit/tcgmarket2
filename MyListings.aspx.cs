using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using MongoDB.Bson;

namespace Web2
{
    public partial class MyListings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["Login"] as BLL.User;
            if (user == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindListings(user.Id);
            }
        }

        private void BindListings(ObjectId sellerId)
        {
            var listings = Listing.GetBySeller(sellerId);
            RptMyListings.DataSource = listings;
            RptMyListings.DataBind();

            LtlEmpty.Text = (listings == null || listings.Count == 0)
                ? "<p class='text-muted'>עדיין לא הוספת פריטים למכירה.</p>"
                : "";
        }

        protected string GetDisplayImage(object photoUrl, object cardImageUrl)
        {
            var photo = photoUrl as string;
            return string.IsNullOrEmpty(photo) ? (cardImageUrl as string) : ResolveUrl(photo);
        }

        protected void RptMyListings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var user = Session["Login"] as BLL.User;
            if (user == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            ObjectId listingId;
            if (!ObjectId.TryParse(e.CommandArgument.ToString(), out listingId))
                return;

            if (e.CommandName == "Delete")
            {
                Listing.Delete(listingId, user.Id);
                LtlMessage.Text = "<div class='alert alert-success'>הפריט נמחק.</div>";
            }
            else if (e.CommandName == "Save")
            {
                var txtQty = e.Item.FindControl("TxtEditQty") as TextBox;
                var txtNotes = e.Item.FindControl("TxtEditNotes") as TextBox;

                int qty;
                if (txtQty == null || !int.TryParse(txtQty.Text, out qty) || qty <= 0)
                {
                    LtlMessage.Text = "<div class='alert alert-danger'>כמות לא תקינה.</div>";
                    BindListings(user.Id);
                    return;
                }

                var notes = txtNotes != null ? txtNotes.Text : "";

                bool ok = Listing.Update(listingId, user.Id, qty, notes);
                LtlMessage.Text = ok
                    ? "<div class='alert alert-success'>השינויים נשמרו.</div>"
                    : "<div class='alert alert-danger'>שמירה נכשלה.</div>";
            }

            BindListings(user.Id);
        }
    }
}