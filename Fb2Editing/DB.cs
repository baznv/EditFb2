using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Windows;

namespace Fb2Editing
{
    public static class DB
    {
        private static string fullPathToDB;
        private static string stringConnection;// = $"Data Source={fullPathToDB}; Version=3; PRAGMA journal_mode=MEMORY;";

        internal static void Init()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            fullPathToDB = Path.Combine(dir, App.PathToDB);

            if (!File.Exists(fullPathToDB))
            {
                SQLiteConnection.CreateFile(fullPathToDB);
                if (File.Exists(fullPathToDB))
                {
                    stringConnection = $"Data Source={fullPathToDB}; Version=3; journal_mode=MEMORY; synchronous = OFF;";
                    CreateTables();
                    SettingsApp set = new SettingsApp("pathLibrary", @"D:\Книги");
                    InsertRow(set);
                    FullLanguage("Language.txt");
                    FullGenre("Genre.txt");
                }
                else MessageBox.Show("Возникла ошибка при создании базы данных");
            }

        }

        internal static string GetPathToLibrary()
        {
            string comm = "SELECT * FROM SettingsApp WHERE Name_Setting=\"pathLibrary\";";
            string result = "";
            using (SQLiteConnection conn = new SQLiteConnection(stringConnection))
            {
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = comm;

                conn.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                {
                    result = record["Value_Setting"].ToString();
                }
                conn.Close();
            }
            return result;
        }

        internal static int GetIDLanguage(string codeLanguage)
        {
            if (codeLanguage == null) return -1;
            string comm = String.Format("SELECT id FROM Language WHERE Code=\"{0}\";", codeLanguage);
            int id;
            using (SQLiteConnection conn = new SQLiteConnection(stringConnection))
            {
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = comm;

                conn.Open();
                id = int.Parse(command.ExecuteScalar().ToString());
                conn.Close();
            }
            return id;
        }

        internal static int GetIDGenre(string codeGenre)
        {
            string comm = String.Format("SELECT id FROM Genre WHERE Code=\"{0}\";", codeGenre.Split(' ')[0]);
            int id;
            using (SQLiteConnection conn = new SQLiteConnection(stringConnection))
            {
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = comm;

                conn.Open();
                var value = command.ExecuteScalar();
                if (value == null)
                    return -1;
                else
                    id = int.Parse(value.ToString());
                conn.Close();
            }
            return id;
        }

        internal static int GetCountRows(Type type)
        {
            string comm = $"SELECT COUNT(*) FROM {type.Name}";
            int result = 0;
            using (SQLiteConnection conn = new SQLiteConnection(stringConnection))
            {
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = comm;

                conn.Open();
                string tmp = command.ExecuteScalar().ToString();
                if (tmp != null)
                    result = int.Parse(tmp);
            }
            return result;
        }

        private static void FullGenre(string filename)
        {
            List<Genre> lst = new List<Genre>();
            using (StreamReader fs = new StreamReader(filename, Encoding.GetEncoding(1251)))
            {
                while (!fs.EndOfStream)
                {
                    string[] tmp = fs.ReadLine().Split('^');
                    lst.Add(new Genre(tmp[1], tmp[0]));
                }
            }
            InsertRows(lst);
        }

        private static void FullLanguage(string filename)
        {
            List<string> text = new List<string>();
            using (StreamReader fs = new StreamReader(filename, Encoding.GetEncoding(1251)))
            {
                while (!fs.EndOfStream)
                    text.Add(fs.ReadLine());
            }

            List<Language> lst = new List<Language>();

            for (int i = 4; i < text.Count - 1; i += 4)
            {
                for (int j = 1; j < 4; j++)
                {
                    if (text[i + j] != "-")

                    {
                        Language lan = new Language(text[i], text[i + j]);
                        var tmp = lst.Where(x => x.Value == lan.Value && x.Code == lan.Code);
                        if (tmp.Count() == 0)
                            lst.Add(lan);
                    }
                }
            }
            InsertRows(lst);
        }

        public static void SaveData<T>(T obj)
        {
            InsertRow(obj);
        }

        public static void SaveListData<T>(List<T> obj)
        {
            InsertRows(obj);
        }

        private static void InsertRows<T>(List<T> lstObj)
        {
            Type type = typeof(T);
            List<int> lstId = new List<int>();
            List<string> fields = GetNameProperties(type);
            string comm = GetRowINSERT<T>(type, fields);
            comm += "SELECT last_insert_rowid();";

            using (SQLiteConnection conn = new SQLiteConnection(stringConnection))
            {
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = comm;
                conn.Open();

                SQLiteTransaction transaction = conn.BeginTransaction();
                try
                {
                    foreach (var obj in lstObj)
                    {
                        int id;
                        for (int i = 0; i < fields.Count; i++)
                        {
                            PropertyInfo fi = type.GetProperty(fields[i]);
                            command.Parameters.AddWithValue(fields[i], fi.GetValue(obj));
                        }
                        id = int.Parse(command.ExecuteScalar().ToString());
                        PropertyInfo fi_id = type.GetProperty("ID");
                        fi_id.SetValue(obj, id);
                    }
                    transaction.Commit();
                    conn.Close();
                }
                catch (SQLiteException ex)
                {
                    transaction.Rollback();
                }

                conn.Close();
            }
        }

        private static void InsertRow<T>(T obj)
        {
            Type type = typeof(T);
            List<string> fields = GetNameProperties(type);
            string comm = GetRowINSERT<T>(type, fields);
            comm += "SELECT last_insert_rowid();";

            int id;

            using (SQLiteConnection conn = new SQLiteConnection(stringConnection))
            {
                SQLiteCommand command = new SQLiteCommand(conn);
                command.CommandText = comm;

                for (int i = 0; i < fields.Count; i++)
                {
                    PropertyInfo fi = type.GetProperty(fields[i]);
                    command.Parameters.AddWithValue(fields[i], fi.GetValue(obj));
                }

                conn.Open();
                //command.ExecuteNonQuery();
                id = int.Parse(command.ExecuteScalar().ToString());
                PropertyInfo fi_id = type.GetProperty("ID");
                fi_id.SetValue(obj, id);

                conn.Close();
            }
        }

        private static string GetRowINSERT<T>(Type type, List<string> fields)
        {
            string comm = $"INSERT INTO {type.Name} (";

            for (int i = 0; i < fields.Count; i++)
            {
                if (i != 0)
                    comm += ", ";
                comm += $"{fields[i]}";
            }

            comm += ") VALUES (";

            for (int i = 0; i < fields.Count; i++)
            {
                if (i != 0)
                    comm += ", ";
                comm += $"@{fields[i]}";
            }

            comm += ");";
            return comm;
        }

        private static List<string> GetNameProperties(Type type)
        {
            List<string> fields = new List<string>();
            PropertyInfo[] propertyInfo = type.GetProperties();
            foreach (var prop in propertyInfo)
            {
                if (prop.Name == "ID") continue;
                else fields.Add(prop.Name);
            }
            return fields;
        }

        private static void CreateTables()
        {
            Assembly asmbly = Assembly.GetExecutingAssembly();
            List<Type> typeList = asmbly.GetTypes().Where(t => t.GetCustomAttributes(typeof(DataAttribute), true).Length > 0 ).ToList();
            foreach (var temp in typeList)
            {
                CreateTable(temp);
            }
        }

        private static void CreateTable(Type type)
        {
            PropertyInfo[] propertyInfo = type.GetProperties();
            string titleRequest = $"CREATE TABLE IF NOT EXISTS {type.Name} ";
            string request = "(";
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                if (propertyInfo[i].Name == "ID")
                    request += "id INTEGER PRIMARY KEY";
                else
                {
                    switch (propertyInfo[i].PropertyType.Name)
                    {
                        case "String":
                            request += $", {propertyInfo[i].Name.ToLower()} TEXT";
                            break;
                        case "Int32":
                            request += $", {propertyInfo[i].Name.ToLower()} INTEGER";
                            break;
                        default:
                            request += $", {propertyInfo[i].Name.ToLower()} TEXT";
                            break;
                    }
                }
            }
            request += ");";

            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={fullPathToDB}; Version=3;"))
            {
                SQLiteCommand command = new SQLiteCommand(titleRequest + request, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}
