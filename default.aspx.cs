using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web2
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)// אירוע טעינת העמוד
        {
            if (Session["Login"] == null)// בדיקה האם קיים סשן לוגין, במידה ולא מעבירים לעמוד לוגין
            {
                Response.Redirect("Login.aspx");
            }

            var us = (User)Session["Login"];// שליפת אוביקט המשתמש מתוך הסשן
            LtlUser.Text = "<h3>שלום " + us.FullName + "</h3>";// הצגת הודעה מותאמת אישית עם שם המשתמש
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            var us = (User)Session["Login"];
            BLL.User.Logout(us.Id);// מנקה את דגל "מחובר" במונגו, כדי שהמשתמש יוכל להתחבר שוב במקום אחר
            Session["Login"] = null;// מנקה את הסשן
            Response.Redirect("Login.aspx");
        }
    }
}