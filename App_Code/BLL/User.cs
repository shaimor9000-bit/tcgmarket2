using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class User
    {
        public int UserId { get; set; }// קוד משתמש, מספור אוטומטי מהמערכת
        public string UserName { get; set; }// שם משתמש, נזין שם בדרך כלל את המייל
        public string Pass { get; set; }// סיסמה
        public string FullName { get; set; }// שם מלא של המשתמש

        // בנאי ברירת מחדל
        public User() {
            UserId = -1;
            UserName = "Israel";
            Pass = "Israeli";
            FullName = "Israel Israeli";
        }

        // הגדרת בנאי מלא המקבל ערכים עבור כל התכונות
        public User(int UserId, string UserName, string Pass,string FullName)
        {
            this.UserId = UserId;
            this.UserName = UserName;
            this.Pass = Pass;
            this.FullName = FullName;
        }

        public User(string UserName, string Pass)
        {
            
            this.UserName = UserName;
            this.Pass = Pass;
           
        }


        public bool ChkLogin()// פונקציה הבודקת תקינות שםש משתמנש וסיסמה של המשתמש
        {
            if (UserName == "aaa" && Pass == "bbb")// בדיקה האם שם המשתמש והסיסמה תקינים
                return true;
            else
                return false;
            
        }
        public bool Register()
        {
            return true;
        }
        public static bool ChkLoginExternal(string UserName,string Pass)
        {

            if (UserName == "aaa" && Pass == "bbb")// בדיקה האם שם המשתמש והסיסמה תקינים
                return true;
            else
                return false;
        }
        public static int Max(int a,int b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

       
    }
}