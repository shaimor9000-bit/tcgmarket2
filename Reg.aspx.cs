using BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var msg = "";// הגדרת מחרוזת שתציג את הודעת השגיאה

            var email = TxtUser.Text.Trim();
            var pass = TxtPass.Text;
            var passValid = TxtPassValid.Text;
            var fullName = TxtFullname.Text.Trim();
            var address = TxtAddress.Text.Trim();
            var phone = TxtPhone.Text.Trim();
            var cityId = int.Parse(DDLCity.SelectedValue);

            var birthDateText = TxtYear.Text.Trim();
            var birthYear = 0;
            DateTime birthDate;

            if (!DateTime.TryParseExact(birthDateText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
            {
                msg += "<div class='alert'>תאריך לידה אינו תקין</div>";
            }
            else
            {
                var age = DateTime.Today.Year - birthDate.Year;
                if (birthDate.Date > DateTime.Today.AddYears(-age))
                    age--;// מתקן אם יום ההולדת השנה עוד לא הגיע

                if (age < 18)
                    msg += "<div class='alert'>ההרשמה מגיל 18 ומעלה בלבד</div>";

                birthYear = birthDate.Year;
            }

            // נבצע בדיקת תקינות קלט
            if (fullName.Length < 3)
                msg += "<div class='badge badge-alert'>שם מלא חובה , נא להזין שם מלא</div>";
            if (email.Contains("@") == false)
                msg += "<div class='alert'>כתובת מייל אינה תקינה, נא לתקן</div>";
            if (pass.Length < 6)
                msg += "<div class='alert'>סיסמה לא תקינה, גודל מינימלי שישה תווים</div>";
            if (pass != passValid)
                msg += "<div class='alert'>סיסמה ווידוא סיסמה אינם תואמים</div>";

            if (msg == "")
            {
                var newUser = new User();
                newUser.UserName = email;// כרגע נשתמש במייל גם בתור שם המשתמש
                newUser.Email = email;
                newUser.Phone = phone;
                newUser.Pass = pass;
                newUser.FullName = fullName;
                newUser.Address = address;
                newUser.BirthYear = birthYear;
                newUser.CityId = cityId;

                var result = newUser.Register();// שמירת המשתמש במונגו

                switch (result)
                {
                    case RegisterResult.Success:
                        Response.Redirect("Login.aspx");
                        break;
                    case RegisterResult.DuplicateUserName:
                    case RegisterResult.DuplicateEmail:
                        LtlMsg.Text = "<div class='alert'>כתובת המייל הזו כבר רשומה במערכת</div>";
                        break;
                    case RegisterResult.DuplicatePhone:
                        LtlMsg.Text = "<div class='alert'>מספר הטלפון הזה כבר רשום במערכת</div>";
                        break;
                    default:
                        LtlMsg.Text = "<div class='alert'>קרתה תקלה, נסה שוב מאוחר יותר</div>";
                        break;
                }
            }
            else
            {
                LtlMsg.Text = msg;
            }
        }
    }
}