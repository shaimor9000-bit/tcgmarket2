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
            User vv = new User();// יצירת אובייקט מסוג יוזר
            Session["x"] = "abc";
            Session["y"] = 99;
            Session["z"] = vv;
            int xxx =(int) Session["y"];
            User cc= (User)Session["z"];


            
            //int x = 100,y=3,z;
            //double k;
            //k = (double)x / y;
            //int a = 4;
            //double b = 5.6;
            //a =(int) b;
            if (Session["Login"] ==null)// בדיקה האם קיים סשן לוגין, במידה ולא מעבירים לעמוד לוגין
            {
                Response.Redirect("login.aspx");
            }
            User us =(User) Session["Login"];// שליפת אוביקט המשתמש מתוך הסשן
            LtlUser.Text =" שלום משתמש יקר בשם " +us.UserName;// הצגת הודעה מותאמת אישית עם שם המשתמש
            if(IsPostBack==false)// האם זו טעינה ראשונה 
            {
                // מקובל לכתוב כאן פעולות התחלתיות הקשורות לנראות של העמוד, כגון טעינת רשימות של ערים, לקוחות... וכדומה
            }
            else // הייתה שליחה של אירוע מתוך העמוד שנטען עוד קודם
            {

            }

        }
    }
}