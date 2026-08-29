using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BLL;
using DAL;

namespace Web2
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)// אירוע טעינה של האפליקציה
        {
            try
            {
                MongoHelper.EnsureIndexes();// יוצר את האינדקסים הייחודיים במונגו (שם משתמש, מייל, טלפון)
                Card.SeedIfEmpty();// ממלא את קטלוג הקלפים בפעם הראשונה, לא עושה כלום אם כבר יש קלפים
            }
            catch (Exception ex)
            {
                // אם המונגו לא רץ כרגע, לא נרצה שהאתר יקרוס, רק שנדע שהייתה בעיה
                System.Diagnostics.Debug.WriteLine("Mongo connection failed: " + ex.Message);
            }

            // אנחנו מדמים שליפה של מרשימת המוצרים מתוך בסיס הנתונים
            List<Product> lst = new List<Product>();// יצירת רשימה של מוצרים
            Product p = new Product();// יצירת אובייקט סוג מוצר
            p.PName = "aaa";
            lst.Add(p);// הוספת האובייקט לרשימה
            p = new Product();// יצירת אובייקט סוג מוצר
            p.PName = "bbb";
            lst.Add(p);// הוספת האובייקט לרשימה
            p = new Product();// יצירת אובייקט סוג מוצר
            p.PName = "ccc";
            lst.Add(p);// הוספת האובייקט לרשימה
            p = new Product();// יצירת אובייקט סוג מוצר
            p.PName = "zzz";
            lst.Add(p);// הוספת האובייקט לרשימה
            Application["Prods"] = lst;// שמירה רשימת המוצרים בתוך אובייקט מסוג אפליקיישן

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}