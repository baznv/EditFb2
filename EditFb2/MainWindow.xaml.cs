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
using System.Xml;

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
            DB.Init();

            int count = DB.GetCountRows(typeof(Book));
            if (count == 0)
                CreateLibrary();

            //Parse.InitLibrary();


            //string[] arrFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

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

            //string readText = File.ReadAllText(bin1);

            //byte[] newBytes = Convert.FromBase64String(readText);

            //BitmapSource bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(newBytes);
            //imgI.Source = bitmap;
        }

        private static void CreateLibrary()
        {
            string path = DB.GetPathToLibrary();
            string[] arrFiles;
            if (Directory.Exists(path))
            {
                arrFiles = Directory.GetFiles(path, "*.fb2", SearchOption.AllDirectories);
                List<Book> books = new List<Book>();
                List<Document> documents = new List<Document>();
                List<BookGenre> bookGenres = new List<BookGenre>();
                List<BookWriter> bookWriters = new List<BookWriter>();
                List<DocumentAuthor> documentAuthors = new List<DocumentAuthor>();
                List<BookTranslator> bookTranslators = new List<BookTranslator>();
                List<Parse> parses = new List<Parse>();
                List<int> ids = new List<int>();


                /*foreach (string name in arrFiles)
                {
                    using (StreamReader fs = new StreamReader(name))
                    {
                        string readText = fs.ReadToEnd();
                        Parse parse = new Parse(readText);

                        Book book = new Book() {
                            Book_title = parse.ti_book_title,
                            Annotation = parse.ti_annotation,
                            Date_write = parse.ti_date,
                            Coverpage = parse.ti_coverpage,
                            Book_name = parse.pi_book_name,
                            Publisher = parse.pi_publisher,
                            City = parse.pi_city,
                            Year = parse.pi_year,
                            ID_Language = DB.GetIDLanguage(parse.ti_lang),
                            ID_LanguageAfterTranslate = DB.GetIDLanguage(parse.ti_src_lang)
                        };
                        books.Add(book);
                        //int idBook = DB.SaveData(book);

                        Document document = new Document()
                        {
                            Path = name,
                            UniqID = parse.di_id,
                            Program_used = parse.di_program_used,
                            Date_create_document = parse.di_date,
                            Src_ocr = parse.di_src_ocr,
                            Version = parse.di_version,
                            History = parse.di_history,
                            //ID_Book = idBook
                        };
                        documents.Add(document);

                        foreach (var item in parse.ti_genre)
                        {
                            int idGenre = DB.GetIDGenre(item);
                            BookGenre bookGenre = new BookGenre()
                            {
                                ID_Genre = idGenre,
                                //ID_Book = idBook
                            };
                            bookGenres.Add(bookGenre);
                        }

                        foreach (var item in parse.ti_writer)
                        {
                            //int idWriter = DB.SaveData(item);
                            BookWriter bookWriter = new BookWriter()
                            {
                                //ID_Writer
                                //ID_Book
                            };
                            bookWriters.Add(bookWriter);
                        }


                        foreach (var item in parse.di_author)
                        {
                            //int idWriter = DB.SaveData(item);
                            DocumentAuthor documentAuthor = new DocumentAuthor()
                            {
                                //ID_Writer
                                //ID_Book
                            };
                            documentAuthors.Add(documentAuthor);
                        }

                        foreach (var item in parse.ti_translator)
                        {
                            //int idWriter = DB.SaveData(item);
                            BookTranslator bookTranslator = new BookTranslator()
                            {
                                //ID_Writer
                                //ID_Book
                            };
                            bookTranslators.Add(bookTranslator);
                        }


                        BookSequence bookSequence = new BookSequence()
                        {
                            //ID_Sequence
                            //ID_Book
                            //Number_in_sequence
                        };

                        Keyword keyword = new Keyword()
                        {
                            //ID_Document
                            //Word
                        };

                        foreach (var value in parse.ti_sequence)
                        {
                            Sequence sequence = new Sequence()
                            {
                                Name = value
                            };
                        }

                        Src_urls src_urls = new Src_urls()
                        {
                            //ID_Book
                            //Url
                        };
                    }
                }*/

                //1 этап
                foreach (string name in arrFiles)
                {
                    Encoding encoding = null;
                    using (StreamReader sr = new StreamReader(name, true))
                    {
                        encoding = sr.CurrentEncoding;
                    }
                    List<string> lstStr = new List<string>();

                    XmlTextReader reader = new XmlTextReader(name);
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element: // Узел является элементом.
                                lstStr.Add("<" + reader.Name + ">");
                                break;
                            case XmlNodeType.Text: // Вывести текст в каждом элементе.
                                lstStr.Add(reader.Value);
                                break;
                            case XmlNodeType.EndElement: // Вывести конец элемента.
                                lstStr.Add("</" + reader.Name + ">");
                                break;
                        }
                    }

                    //using (StreamReader fs = new StreamReader(name, Encoding.Default))
                    //{
                    //    string readText = fs.ReadToEnd();
                    //    Parse parse = new Parse(readText);

                    //    Book book = new Book()
                    //    {
                    //        Book_title = parse.ti_book_title,
                    //        Annotation = parse.ti_annotation,
                    //        Date_write = parse.ti_date,
                    //        Coverpage = parse.ti_coverpage,
                    //        Book_name = parse.pi_book_name,
                    //        Publisher = parse.pi_publisher,
                    //        City = parse.pi_city,
                    //        Year = parse.pi_year,
                    //        ID_Language = DB.GetIDLanguage(parse.ti_lang),
                    //        ID_LanguageAfterTranslate = DB.GetIDLanguage(parse.ti_src_lang)
                    //    };
                    //    books.Add(book);

                    //    parses.Add(parse);
                    //}

                }

                ids = DB.SaveListData(books);
                int a = ids.Count;
                int b = parses.Count;

            }
        }


    }
}
