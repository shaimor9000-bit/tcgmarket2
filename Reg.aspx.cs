using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Web2
{
    public partial class Reg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnReg_Click(object sender, EventArgs e)
        {
            string Msg = "";// הגדרת מחרוזת שתציג את הודעת השגיאה
            int UserId = new Random().Next(100, 10000);
            string Username = TxtUser.Text;
            string Pass = TxtPass.Text;
            string PassValid = TxtPassValid.Text;
            string FullName = TxtFullname.Text;

            // נבצע בדיקת תקינות קלט
            // השם המלא הוא שדה חובה
            if (FullName.Length < 3)
                Msg += "<div class='badge badge-alert'>שם מלא חובה , נא להזין שם מלא</div>";
            if(Username.Contains("@")==false)
                Msg += "<div class='alert'>שם משתץמש אינו כתץובת מייל תקינה, נא לתקן</div>";
            if(Pass.Length<6)
                Msg += "<div class='alert'>סיסמה לא תקינה, גודל מינימלי שישה תווים</div>";
            if(Pass != PassValid)
                Msg += "<div class='alert'>סיסמה ווידוא סיסמה אינם תואמים</div>";

            if(Msg=="")
            {
                User us = new User(UserId, Username, Pass, FullName);
                us.Register();// שמירת המשתמש במערכת לכאורה
                Response.Redirect("default.aspx");
            }
            else
            {
                LtlMsg.Text = Msg;
            }
           

        }
    }
}