using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web2
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Product> lst =(List<Product>) Application["Prods"];// שליפת רשימת המוצרים מתוך הזכרון של האפליקיישן
            for(int i = 0; i < lst.Count; i++)
            {
                Response.Write(lst[i].PName);// הדפסה של שמות המוצרים באתר
            }
        }
    }
}