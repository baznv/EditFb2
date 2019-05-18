using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace EditFb2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string pathLibrary = @"D:\Книги";
        //string dir = @"D:/Projects/EditFb2/EditFb2";
        //string PathToDB = @"Library.db";

        public MainWindow()
        {
            InitializeComponent();

            //string fullPathToDB = Path.Combine(dir, PathToDB);
            DB.Init();
            //string[] arrFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            //string str = File.ReadAllText(@"D:/Projects/EditFb2/EditFb2/123.fb2");

            //Regex reg = new Regex(@"(<description[\s\S]*?[>])([\s\S]*?)</description>");
            ////MatchCollection matches = reg.Matches(str);
            //Match match = reg.Match(str);
            //List<string> lst = new List<string>();
            ////foreach (Match match in matches)
            ////{
            //    int t = match.Groups.Count;
            //    lst.Add(match.Groups[0].ToString()); //весь текст
            //    lst.Add(match.Groups[1].ToString()); //начальный тэг
            //    lst.Add(match.Groups[2].ToString()); //внутреннее содержимое
            ////}

            //try
            //{
            //    using (StreamReader sr = new StreamReader(path))
            //    {
            //        MessageBox.Show(sr.ReadToEnd());
            //    }

            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}


            //string bin1 = $"D:/binary2.txt";

            //string readText = File.ReadAllText(bin1);

            //byte[] newBytes = Convert.FromBase64String(readText);

            //BitmapSource bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(newBytes);
            //imgI.Source = bitmap;
        }

    }
}
