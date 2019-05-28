using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Fb2Editing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DB.Init();
            string path = DB.GetPathToLibrary();
            string[] arrFiles;

            List<string> badFiles = new List<string>();
            if (Directory.Exists(path))
            {
                arrFiles = Directory.GetFiles(path, "*.fb2", SearchOption.AllDirectories);
                //List<AllBook> lstAll = new List<AllBook>();
                List<ReadFb2> lstReadfb2 = new List<ReadFb2>();
                foreach (string name in arrFiles)
                {
                    //string pathFile = @"D:\Книги\Experiments.fb2";
                    //ReadWriteFb2.SaveBookToDB(name);
                    //lstAll.Add(XmlFiles.GetFieldFromFile(name));
                    //lstBooks.Add(ReadFb2.SaveBookToDB(name));
                    lstReadfb2.Add(new ReadFb2(name), badFiles);
                }
                //DB.SaveListData(lstAll);
            }

            //XmlDocument doc = new XmlDocument();
            //doc.Load(pathFile);
            //XmlElement root = doc.DocumentElement;
            //XmlNode node = root.SelectSingleNode("/FictionBook/description");
            //foreach (XmlElement child in root)
            //{
            //    var s = child.InnerXml;
            //}

            //XmlSerializer ser = new XmlSerializer(typeof(FictionBook));
            //FictionBook fictionBook;
            //using (XmlReader reader = XmlReader.Create(pathFile))
            //{
            //    fictionBook = (FictionBook)ser.Deserialize(reader);
            //}
            //var description = fictionBook.description;
        }
    }
}
