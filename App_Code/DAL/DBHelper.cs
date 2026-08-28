using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public static class DBHelper
    {
        private const string DbName = "TcgMarketDB";

        // מחזיר קונקשן סגור למסד הנתונים - מי שקורא לפונקציה אחראי לפתוח ולסגור אותו (using)
        public static SqlConnection GetConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["TcgMarketDB"].ConnectionString;
            return new SqlConnection(connStr);
        }

        // רץ בכל הפעלה של האתר - יוצר את מסד הנתונים והטבלה אם הם לא קיימים,
        // ומתקן טבלה ישנה אם חסרות בה עמודות מהגרסה החדשה
        public static void EnsureDatabase()
        {
            // מתחברים קודם ל-master כי הוא תמיד קיים, כדי לבדוק אם ה-DB שלנו כבר נוצר
            string masterConnStr = ConfigurationManager.ConnectionStrings["TcgMarketMaster"].ConnectionString;
            using (var conn = new SqlConnection(masterConnStr))
            {
                conn.Open();
                var createDbSql = "IF DB_ID('" + DbName + "') IS NULL CREATE DATABASE " + DbName;
                using (var cmd = new SqlCommand(createDbSql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                // יוצר את טבלת המשתמשים אם היא עדיין לא קיימת
                string createUsersTable = @"
                    IF OBJECT_ID('dbo.Users', 'U') IS NULL
                    BEGIN
                        CREATE TABLE dbo.Users
                        (
                            UserId       INT IDENTITY(1,1) PRIMARY KEY,
                            UserName     NVARCHAR(50)  NOT NULL UNIQUE,
                            Email        NVARCHAR(100) NOT NULL UNIQUE,
                            Phone        NVARCHAR(20)  NOT NULL UNIQUE,
                            PasswordHash NVARCHAR(200) NOT NULL,
                            PasswordSalt NVARCHAR(200) NOT NULL,
                            FullName     NVARCHAR(100) NULL,
                            Address      NVARCHAR(200) NULL,
                            BirthYear    INT NULL,
                            CityId       INT NULL,
                            IsLoggedIn   BIT NOT NULL DEFAULT 0,
                            CreatedAt    DATETIME NOT NULL DEFAULT GETDATE()
                        )
                    END";
                using (var cmd = new SqlCommand(createUsersTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // אם הטבלה כבר הייתה קיימת מגרסה קודמת, נעדכן אותה בהתאם לשינויים החדשים
                string fixOldTable = @"
                    IF COL_LENGTH('dbo.Users', 'IsLoggedIn') IS NULL
                    BEGIN
                        ALTER TABLE dbo.Users ADD IsLoggedIn BIT NOT NULL DEFAULT 0
                    END

                    IF EXISTS (SELECT 1 FROM sys.columns
                               WHERE object_id = OBJECT_ID('dbo.Users') AND name = 'Phone' AND is_nullable = 1)
                    BEGIN
                        ALTER TABLE dbo.Users ALTER COLUMN Phone NVARCHAR(20) NOT NULL
                    END

                    IF NOT EXISTS (SELECT 1 FROM sys.key_constraints
                                    WHERE type = 'UQ' AND parent_object_id = OBJECT_ID('dbo.Users')
                                    AND name = 'UQ_Users_Phone')
                    BEGIN
                        ALTER TABLE dbo.Users ADD CONSTRAINT UQ_Users_Phone UNIQUE (Phone)
                    END";
                using (var cmd = new SqlCommand(fixOldTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}