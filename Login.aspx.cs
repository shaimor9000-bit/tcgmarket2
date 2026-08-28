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
            if(IsPostBack==false)// כניסה ראשונה לעמוד
            {
                TxtUser.Text = "bla bla";
                
                int a;
                a = 4;
                string b;
                User u1;// כרגע משתנה הייחוס מכיל נאל
                u1= new User(100,"ylapidot@gmail.com","123456","Yaron Lapidot");// יצירת אובייקט מסוג משתמש בשם יו אחד

                // אנחנו מפעילים את פונקציית הבנאי בעת יצירת אובייקט
                // User() - זו הפעלה של הבנאי של המחלקה יוזר
                // במידה ולא הוגדר במחלקה בנאי, המערכת מייצרת מאחורי הקלעים בנאי ברירת מחדל עבורנו
                //? מה יש בתוך התכונות של יו אחד לאחר הפקודה
                //אפסים ונאלים מחרוזת ריקה, כלום, הכול מאופס
                // מה נעשה, במידה ואנחנו מעונייםנים בעת יצירת האוביקט לתת לו ערכים משלנו במקום האפסים והנאלים?
                // תשובה: ניצור פונקציה שתאתחל את השדות שלו, לפונקציה זו קוראים בנאי
                u1.UserName = "aaa";
                u1.Pass = "bbb";// הכנסה לתוך השדות ערכים
                User u2 = new User("aaa","bbb");
                u1.ChkLogin();
                BLL.User.ChkLoginExternal("","");// הפעלת פעולה 

                BLL.User.Max(3, 5);

            }
          
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string Email=TxtUser.Text;
            string Pass=TxtPass.Text;
            User user = new User(Email,Pass);// יצירת אובייקט מסוג משתמש
            if(user.ChkLogin())// במידה והשם והסיסמה תקינים, התחבר למערכת
            {
                Session["Login"]=user;//שמירת אוביקט המשתמש בתוך משתנה מסוג סשן
                Response.Redirect("default.aspx");// מעבר לעמוד הבית
            }
            else// השם והסיסמה אינם תקינים, הצג הודעה מתאימה
            {
                LtlMsg.Text = "<div class='badge badge-error' >שם / סיסמה אינם נכונים</div>";
            }

        }

        protected void btnTmp_Click(object sender, EventArgs e)
        {

        }
    }
}