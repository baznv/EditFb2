using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditFb2
{
    public static class DB
    {
        //public static string PathToDB { get; set; } = @"D:/Projects/EditFb2/EditFb2/Library.db";

        internal static void CreateTables()
        {
            
        }

        internal static void Init()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPathToDB = Path.Combine(dir, App.PathToDB);

            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={fullPathToDB}; Version=3;"))
            {
                // ******
            }
            //if (!File.Exists(fullPathToDB))
            //{
            //    File.Create(fullPathToDB);
            //    CreateTables();
            //}
            //string str = File.ReadAllText(@"D:/Projects/EditFb2/EditFb2/123.fb2");
        }
    }
}
