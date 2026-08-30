using System;
using System.Web.UI;
using BLL;

namespace Web2
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindListings();
            }
        }

        private void BindListings()
        {
            var listings = Listing.GetAll();

            if (listings.Count == 0)
            {
                LtlEmpty.Text = "<div class='alert alert-info'>אין עדיין קלפים למכירה</div>";
                RptListings.Visible = false;
                return;
            }

            RptListings.DataSource = listings;
            RptListings.DataBind();
        }

        // מציג את התמונה שהמוכר העלה אם קיימת, אחרת חוזר לתמונת הקטלוג
        protected string GetDisplayImage(object photoUrl, object cardImageUrl)
        {
            var photo = photoUrl as string;
            return string.IsNullOrEmpty(photo) ? (cardImageUrl as string) : ResolveUrl(photo);
        }
    }
}