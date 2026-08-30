using System;
using BLL;

namespace Web2
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HiddenEmail.Value = Request.QueryString["email"] ?? "";
                HiddenToken.Value = Request.QueryString["token"] ?? "";

                if (string.IsNullOrEmpty(HiddenEmail.Value) || string.IsNullOrEmpty(HiddenToken.Value))
                {
                    LtlMsg.Text = "<div class='alert alert-danger mt-3'>קישור לא תקין. בקש/י קישור חדש לאיפוס סיסמה.</div>";
                    PnlReset.Visible = false;
                }
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            var newPass = TxtNewPass.Text;
            var newPassValid = TxtNewPassValid.Text;

            if (newPass.Length < 6)
            {
                LtlMsg.Text = "<div class='alert alert-danger mt-3'>סיסמה חייבת להכיל לפחות שישה תווים</div>";
                return;
            }
            if (newPass != newPassValid)
            {
                LtlMsg.Text = "<div class='alert alert-danger mt-3'>הסיסמאות אינן תואמות</div>";
                return;
            }

            var ok = BLL.User.ResetPassword(HiddenEmail.Value, HiddenToken.Value, newPass);

            if (ok)
            {
                LtlMsg.Text = "<div class='alert alert-success mt-3'>הסיסמה עודכנה בהצלחה. אפשר להתחבר עכשיו.</div>";
                PnlReset.Visible = false;
            }
            else
            {
                LtlMsg.Text = "<div class='alert alert-danger mt-3'>הקישור פג תוקף או שגוי. בקש/י קישור חדש לאיפוס סיסמה.</div>";
                PnlReset.Visible = false;
            }
        }
    }
}