using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Fb2Editing
{
    public class ReadFb2
    {
        public List<string> TitleInfo_genres { get; private set; } = new List<string>();
        public List<Writers> TitleInfo_writers { get; private set; } = new List<Writers>();
        public string TitleInfo_book_title { get; private set; }
        public string TitleInfo_annotation { get; private set; } = "";
        public string TitleInfo_keyword { get; private set; }
        public string TitleInfo_date { get; private set; }
        public string TitleInfo_coverpage { get; private set; }
        public string TitleInfo_coverpage_binary { get; private set; } //не реализован
        public string TitleInfo_lang { get; private set; }
        public string TitleInfo_src_lang { get; private set; }
        public List<Translators> TitleInfo_translators { get; private set; } = new List<Translators>();
        public List<string> TitleInfo_sequences { get; private set; } = new List<string>(); //не реализован

        public List<Authors> DocumentInfo_authors { get; private set; } = new List<Authors>();
        public string DocumentInfo_program_used { get; private set; }
        public string DocumentInfo_date { get; private set; }
        public List<string> DocumentInfo_src_urls { get; private set; } = new List<string>(); //не реализован
        public string DocumentInfo_src_ocr { get; private set; }
        public string DocumentInfo_id { get; private set; }
        public string DocumentInfo_version { get; private set; }
        public string DocumentInfo_history { get; private set; }
        public string Document_PathToFile { get; private set; }


        public string PublishInfo_book_name { get; private set; }
        public string PublishInfo_publisher { get; private set; }
        public string PublishInfo_city { get; private set; }
        public string PublishInfo_year { get; private set; }

        public ReadFb2(string pathFile)
        {
            ParseFb2(pathFile);
        }

        private void ParseFb2(string pathFile)
        {
            using (XmlReader xml = XmlReader.Create(pathFile))
            {
                Document_PathToFile = pathFile;
                bool continueRead = true;
                while (continueRead)
                {
                    try
                    {
                        xml.Read();
                    }
                    catch
                    {
                        ///TODO: список не загруженных файлов
                    }
                    XNamespace ns = xml.LookupNamespace("");

                    if (!continueRead)
                    {
                        break;
                    }
                    switch (xml.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (xml.Name == "title-info")
                            {
                                ParseTitleInfo(xml.ReadSubtree());
                            }

                            if (xml.Name == "src-title-info")
                            {
                                XElement elSrcTitleInfo = XNode.ReadFrom(xml) as XElement;
                            }

                            if (xml.Name == "document-info")
                            {
                                XElement elDocumentInfo = XNode.ReadFrom(xml) as XElement;

                                var authors = elDocumentInfo.Elements(ns + "author");
                                foreach (var item in authors)
                                {
                                    Authors author = new Authors()
                                    {
                                        First_name = item.Element(ns + "first-name")?.Value,
                                        Last_name = item.Element(ns + "last-name")?.Value,
                                        Middle_name = item.Element(ns + "middle-name")?.Value,
                                        Nickname = item.Element(ns + "nickname")?.Value,
                                        Email = item.Element(ns + "email")?.Value,
                                        Home_page = item.Element(ns + "home-page")?.Value,
                                        UniqID = item.Element(ns + "id")?.Value
                                    };
                                    DocumentInfo_authors.Add(author);
                                }

                                DocumentInfo_date = elDocumentInfo.Element(ns + "date")?.Attribute("value") == null ? elDocumentInfo.Element(ns + "date")?.Value : elDocumentInfo.Element(ns + "date")?.Attribute("value")?.Value;
                                DocumentInfo_history = elDocumentInfo.Element(ns + "history")?.Value;
                                DocumentInfo_program_used = elDocumentInfo.Element(ns + "program-used")?.Value;
                                DocumentInfo_src_ocr = elDocumentInfo.Element(ns + "src-ocr")?.Value;
                                DocumentInfo_id = elDocumentInfo.Element(ns + "id")?.Value;
                                DocumentInfo_version = elDocumentInfo.Element(ns + "version")?.Value;
                            }

                            if (xml.Name == "publish-info")
                            {
                                XElement elPublishInfo = XNode.ReadFrom(xml) as XElement;

                                PublishInfo_city = elPublishInfo.Element(ns + "city")?.Value;
                                PublishInfo_book_name = elPublishInfo?.Element(ns + "book-name")?.Value;
                                PublishInfo_publisher = elPublishInfo?.Element(ns + "publisher")?.Value;
                                PublishInfo_year = elPublishInfo?.Element(ns + "year")?.Value;
                            }

                            if (xml.Name == "body")
                            {
                                continueRead = false;
                            }

                            break;
                    }
                }
            }
        }

        private void ParseTitleInfo(XmlReader xml)
        {
            string lastNodeName = "";
            while (xml.Read())
            {
                switch (xml.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xml.Name == "annotation")
                        {
                            lastNodeName = xml.Name;
                        }
                        if (xml.Name == "book-title")
                        {
                            lastNodeName = xml.Name;
                        }
                        if (xml.Name == "keywords")
                        {
                            lastNodeName = xml.Name;
                        }
                        if (xml.Name == "lang")
                        {
                            lastNodeName = xml.Name;
                        }
                        if (xml.Name == "src-lang")
                        {
                            lastNodeName = xml.Name;
                        }

                        break;
                    case XmlNodeType.Text:
                        if (lastNodeName == "annotation")
                        {
                            TitleInfo_annotation += xml.Value;
                        }
                        if (xml.Name == "book-title")
                        {
                            TitleInfo_book_title = xml.Value;
                        }
                        if (xml.Name == "keywords")
                        {
                            TitleInfo_keyword = xml.Value;
                        }
                        if (xml.Name == "lang")
                        {
                            TitleInfo_lang = xml.Value;
                        }
                        if (xml.Name == "src-lang")
                        {
                            TitleInfo_src_lang = xml.Value;
                        }

                        break;
                }
            }
                //XElement elTitleInfo = XNode.ReadFrom(xml) as XElement;

            //TitleInfo_date = elTitleInfo.Element(ns + "date")?.Attribute("value") == null ? elTitleInfo.Element(ns + "date")?.Value : elTitleInfo.Element(ns + "date")?.Attribute("value")?.Value;
            //TitleInfo_coverpage = elTitleInfo.Element(ns + "coverpage")?.Element(ns + "image")?.Attributes()?.Select(p => p.Value.Split('#')[1]).ToList()[0];
            //TitleInfo_genres = elTitleInfo.Elements(ns + "genre").Select(p => p.Value).ToList();
            //var writers = elTitleInfo.Elements(ns + "author");
            //foreach (var item in writers)
            //{
            //    Writers writer = new Writers()
            //    {
            //        First_name = item.Element(ns + "first-name")?.Value,
            //        Last_name = item.Element(ns + "last-name")?.Value,
            //        Middle_name = item.Element(ns + "middle-name")?.Value,
            //        Nickname = item.Element(ns + "nickname")?.Value,
            //        Email = item.Element(ns + "email")?.Value,
            //        Home_page = item.Element(ns + "home-page")?.Value,
            //        UniqID = item.Element(ns + "id")?.Value
            //    };
            //    TitleInfo_writers.Add(writer);
            //}
            //var translators = elTitleInfo.Elements(ns + "translator");
            //foreach (var item in translators)
            //{
            //    Translators translator = new Translators()
            //    {
            //        First_name = item.Element(ns + "first-name")?.Value,
            //        Last_name = item.Element(ns + "last-name")?.Value,
            //        Middle_name = item.Element(ns + "middle-name")?.Value,
            //        Nickname = item.Element(ns + "nickname")?.Value,
            //        Email = item.Element(ns + "email")?.Value,
            //        Home_page = item.Element(ns + "home-page")?.Value,
            //        UniqID = item.Element(ns + "id")?.Value
            //    };
            //    TitleInfo_translators.Add(translator);
            //}


        }
    }
}











//            XElement xmlFile = XElement.Load(pathFile);
//            var ns = xmlFile.GetDefaultNamespace();

//            XElement elDescription = xmlFile.Element(ns + "description");
//            IEnumerable<XElement> imagesBinary = xmlFile.Elements(ns + "binary");

//            XElement elTitleInfo = elDescription.Element(ns + "title-info");
//            XElement elPublishInfo = elDescription.Element(ns + "publish-info");
//            XElement elDocumentInfo = elDescription.Element(ns + "document-info");

//            string nameJpg = elTitleInfo.Element(ns + "coverpage")?.Element(ns + "image")?.Attributes()?.Select(p => p.Value.Split('#')[1]).ToList()[0];

//            string binaryImg = imagesBinary.SingleOrDefault(p => p.Attribute("id").Value == nameJpg)?.Value;
//            //BookFromFb2 bfb2 = new BookFromFb2();
//            //var qAnnotation = elTitleInfo.Element(ns + "annotation")?.Value;
//            //var qBook_name = elPublishInfo?.Element(ns + "book-name")?.Value;
//            //var Book_title = elTitleInfo.Element(ns + "book-title")?.Value;
//            //var qCity = elTitleInfo.Element(ns + "city")?.Value;
//            //var qCoverpage = binaryImg;
//            //var qDate_write = elTitleInfo.Element(ns + "date")?.Attribute("value") == null ? elTitleInfo.Element(ns + "date")?.Value : elTitleInfo.Element(ns + "date")?.Attribute("value")?.Value;
//            //var qKeywords = elTitleInfo.Element(ns + "keywords")?.Value;
//            //var qPublisher = elPublishInfo?.Element(ns + "publisher")?.Value;
//            //var qYear = elPublishInfo?.Element(ns + "year")?.Value;
//            //var qID_Language = DB.GetIDLanguage(elTitleInfo.Element(ns + "lang")?.Value.Replace(" ", string.Empty));
//            //var qID_LanguageAfterTranslate = DB.GetIDLanguage(elTitleInfo.Element(ns + "src-lang")?.Value.Replace(" ", string.Empty));
//            bfb2.book = new Book()
//            {
//                Annotation = elTitleInfo.Element(ns + "annotation")?.Value,
//                Book_name = elPublishInfo?.Element(ns + "book-name")?.Value,
//                Book_title = elTitleInfo.Element(ns + "book-title")?.Value,
//                City = elTitleInfo.Element(ns + "city")?.Value,
//                Coverpage = binaryImg,
//                Date_write = elTitleInfo.Element(ns + "date")?.Attribute("value") == null ? elTitleInfo.Element(ns + "date")?.Value : elTitleInfo.Element(ns + "date")?.Attribute("value")?.Value,
//                Keywords = elTitleInfo.Element(ns + "keywords")?.Value,
//                Publisher = elPublishInfo?.Element(ns + "publisher")?.Value,
//                Year = elPublishInfo?.Element(ns + "year")?.Value,
//                ID_Language = DB.GetIDLanguage(elTitleInfo.Element(ns + "lang")?.Value.Replace(" ", string.Empty)),
//                ID_LanguageAfterTranslate = DB.GetIDLanguage(elTitleInfo.Element(ns + "src-lang")?.Value.Replace(" ", string.Empty))
//            };

//            //DB.SaveData(book);

//            bfb2.document = new Document()
//            {
//                Date_create_document = elDocumentInfo.Element(ns + "date")?.Attribute("value") == null ? elDocumentInfo.Element(ns + "date")?.Value : elDocumentInfo.Element(ns + "date")?.Attribute("value")?.Value,
//                History = elDocumentInfo.Element(ns + "history")?.Value,
//                PathToFile = pathFile,
//                ID_Book = book.ID,
//                Program_used = elDocumentInfo.Element(ns + "program-used")?.Value,
//                Src_ocr = elDocumentInfo.Element(ns + "src-ocr")?.Value,
//                UniqID = elDocumentInfo.Element(ns + "id")?.Value,
//                Version = elDocumentInfo.Element(ns + "version")?.Value,
//                //ID
//            };

//            //DB.SaveData(document);

//            List<BookGenre> lstGenre = new List<BookGenre>();
//            var genres = elTitleInfo.Elements(ns + "genre").Select(p => p.Value);
//            foreach (string item in genres)
//            {
//                string nameGenre = item.Replace(" ", string.Empty);
//                int tmp = DB.GetIDGenre(nameGenre);
//                if (tmp == -1)
//                {
//                    DB.SaveData(new Genre(nameGenre, ""));
//                    tmp = DB.GetIDGenre(nameGenre);
//                }
//                lstGenre.Add(new BookGenre() { ID_Book = book.ID, ID_Genre = tmp });
//            }

//            DB.SaveListData(lstGenre);

//            List<BookWriter> lstBookWriter = new List<BookWriter>();
//            IEnumerable<XElement> writers = elTitleInfo.Elements(ns + "author");
//            foreach (XElement item in writers)
//            {
//                Writers writer = new Writers()
//                {
//                    First_name = item.Element(ns + "first-name")?.Value,
//                    Last_name = item.Element(ns + "last-name")?.Value,
//                    Middle_name = item.Element(ns + "middle-name")?.Value,
//                    Nickname = item.Element(ns + "nickname")?.Value,
//                    Email = item.Element(ns + "email")?.Value,
//                    Home_page = item.Element(ns + "home-page")?.Value,
//                    UniqID = item.Element(ns + "id")?.Value
//                };
//                DB.SaveData(writer);

//                lstBookWriter.Add(new BookWriter() { ID_Book = book.ID, ID_Writer = writer.ID });
//            }

//            DB.SaveListData(lstBookWriter);


//            List<DocumentAuthor> lstDocumentAuthor = new List<DocumentAuthor>();
//            IEnumerable<XElement> authors = elDocumentInfo.Elements(ns + "author");
//            foreach (XElement item in authors)
//            {
//                Authors author = new Authors()
//                {
//                    First_name = item.Element(ns + "first-name")?.Value,
//                    Last_name = item.Element(ns + "last-name")?.Value,
//                    Middle_name = item.Element(ns + "middle-name")?.Value,
//                    Nickname = item.Element(ns + "nickname")?.Value,
//                    Email = item.Element(ns + "email")?.Value,
//                    Home_page = item.Element(ns + "home-page")?.Value,
//                    UniqID = item.Element(ns + "id")?.Value
//                };
//                DB.SaveData(author);

//                lstDocumentAuthor.Add(new DocumentAuthor() { ID_Book = book.ID, ID_Author = author.ID });
//            }

//            DB.SaveListData(lstDocumentAuthor);


//            List<BookTranslator> lstBookTranslator = new List<BookTranslator>();
//            IEnumerable<XElement> translators = elTitleInfo.Elements(ns + "translator");
//            foreach (XElement item in authors)
//            {
//                Translators translator = new Translators()
//                {
//                    First_name = item.Element(ns + "first-name")?.Value,
//                    Last_name = item.Element(ns + "last-name")?.Value,
//                    Middle_name = item.Element(ns + "middle-name")?.Value,
//                    Nickname = item.Element(ns + "nickname")?.Value,
//                    Email = item.Element(ns + "email")?.Value,
//                    Home_page = item.Element(ns + "home-page")?.Value,
//                    UniqID = item.Element(ns + "id")?.Value
//                };
//                DB.SaveData(translator);

//                lstBookTranslator.Add(new BookTranslator() { ID_Book = book.ID, ID_Translator = translator.ID });
//            }

//            DB.SaveListData(lstBookTranslator);

//            List<BookSequence> lstSequence = new List<BookSequence>();
//            IEnumerable<XElement> Sequences = elTitleInfo.Elements(ns + "sequence");
//            foreach (XElement item in Sequences)
//            {
//                Sequence sequence = new Sequence()
//                {
//                    Name = item.Attribute("name")?.Value
//                };
//                DB.SaveData(sequence);

//                string number = item.Attribute("number")?.Value;
//                int numberSequence = number == null ? -1 : int.Parse(number);
//                lstSequence.Add(new BookSequence() { ID_Book = book.ID, ID_Sequence = sequence.ID, Number_in_sequence = numberSequence });
//            }

//            DB.SaveListData(lstSequence);

//            List<Src_urls> lstSrc_urls = new List<Src_urls>();
//            IEnumerable<XElement> lstUrl = elDocumentInfo.Elements(ns + "src-url");
//            foreach (var item in lstUrl)
//            {
//                lstSrc_urls.Add(new Src_urls() { ID_Book = book.ID, Url = item.Value });
//            }

//            DB.SaveListData(lstSrc_urls);

//            return bfb2;
//        }
//    }
//}
