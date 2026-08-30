using System;
using BLL;

namespace Web2
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            var email = TxtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                LtlMsg.Text = "<div class='alert alert-danger mt-3'>נא להזין כתובת מייל תקינה</div>";
                return;
            }

            string token;
            var found = BLL.User.GeneratePasswordResetToken(email, out token);

            if (found)
            {
                var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/");
                var resetLink = baseUrl + "ResetPassword.aspx?email=" + Uri.EscapeDataString(email) + "&token=" + token;
                EmailHelper.SendPasswordResetEmail(email, resetLink);
            }

            // אותה הודעה בין אם המייל קיים ובין אם לא, כדי לא לחשוף אילו כתובות רשומות במערכת
            LtlMsg.Text = "<div class='alert alert-success mt-3'>אם כתובת המייל הזו רשומה במערכת, נשלח אליה קישור לאיפוס סיסמה</div>";
        }
    }
}