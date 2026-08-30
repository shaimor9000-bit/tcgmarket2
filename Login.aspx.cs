using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            var phone = TxtUser.Text.Trim();
            var pass = TxtPass.Text;

            User loggedInUser;
            var result = BLL.User.Login(phone, pass, out loggedInUser);

            switch (result)
            {
                case LoginResult.Success:
                    Session["Login"] = loggedInUser;
                    Response.Redirect("default.aspx");
                    break;
                case LoginResult.AlreadyConnected:
                    LtlMsg.Text = "<div class='alert alert-danger mt-3'>המשתמש הזה כבר מחובר ממקום אחר, התנתק שם קודם</div>";
                    break;
                default:
                    LtlMsg.Text = "<div class='alert alert-danger mt-3'>טלפון / סיסמה אינם נכונים</div>";
                    break;
            }
        }
    }
}