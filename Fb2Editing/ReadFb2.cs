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

        public string Description_Src_title_info { get; set; } // данные об исходнике книги (до перевода). (От 0 до 1 вхождения).Содержится в description, структура аналогична title-info 

        public ReadFb2(string pathFile, List<string> badFiles)
        {
            ParseFb2(pathFile, badFiles);
        }

        private void ParseFb2(string pathFile, List<string> badFiles)
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
                        badFiles.Add(pathFile);
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

                            else if (xml.Name == "src-title-info")
                            {
                                //данные об исходнике книги(до перевода)
                                //ParseSrcTitleInfo(xml.ReadSubtree());
                                Description_Src_title_info = xml.ReadInnerXml();
                            }

                            else if (xml.Name == "document-info")
                            {
                                ParseDocumentInfo(xml.ReadSubtree());
                            }

                            else if (xml.Name == "publish-info")
                            {
                                ParsePublishInfo(xml.ReadSubtree());
                            }

                            else if (xml.Name == "body")
                            {
                                continueRead = false;
                            }

                            break;
                    }
                }
            }
        }

        private void ParsePublishInfo(XmlReader xml)
        {
            string lastNodeName = "";
            while (xml.Read())
            {
                switch (xml.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xml.Name == "city")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if (xml.Name == "book-name")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if (xml.Name == "publisher")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if (xml.Name == "year")
                        {
                            lastNodeName = xml.Name;
                        }
                        break;
                    case XmlNodeType.Text:
                        if (lastNodeName == "city")
                        {
                            PublishInfo_city += xml.Value;
                        }
                        else if (lastNodeName == "book-name")
                        {
                            PublishInfo_book_name += xml.Value;
                        }
                        else if (lastNodeName == "publisher")
                        {
                            PublishInfo_publisher += xml.Value;
                        }
                        else if (lastNodeName == "year")
                        {
                            PublishInfo_year += xml.Value;
                        }
                        break;
                }
            }
        }

        private void ParseDocumentInfo(XmlReader xml)
        {
            string lastNodeName = "";
            Authors author = null;

            while (xml.Read())
            {
                switch (xml.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xml.Name == "history")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if(xml.Name == "program-used")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if(xml.Name == "src-ocr")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if(xml.Name == "id")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if(xml.Name == "version")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if (xml.Name == "author")
                        {
                            if (xml.IsStartElement())
                                author = new Authors();
                            ParseDocumentInfoAuthor(xml.ReadSubtree(), author);
                            //if (xml.NodeType == XmlNodeType.EndElement)
                            //    DocumentInfo_authors.Add(author);
                        }
                        break;
                    case XmlNodeType.Text:
                        if (lastNodeName == "history")
                        {
                            DocumentInfo_history += xml.Value;
                        }
                        else if (lastNodeName == "program-used")
                        {
                            DocumentInfo_program_used += xml.Value;
                        }
                        else if (lastNodeName == "src-ocr")
                        {
                            DocumentInfo_src_ocr += xml.Value;
                        }
                        else if (lastNodeName == "id")
                        {
                            DocumentInfo_id += xml.Value;
                        }
                        else if (lastNodeName == "version")
                        {
                            DocumentInfo_version += xml.Value;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (xml.Name == "author")
                        {
                            DocumentInfo_authors.Add(author);
                        }
                        break;

                }
            }
        }

        private void ParseDocumentInfoAuthor(XmlReader xml, Authors author)
        {
            string lastNodeName = "";
            while (xml.Read())
            {
                switch (xml.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xml.Name == "first-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "last-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "middle-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "nickname")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "email")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "home-page")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "id")
                            lastNodeName = xml.Name;
                        break;
                    case XmlNodeType.Text:
                        if (lastNodeName == "first-name")
                            author.First_name += xml.Value;
                        else if (lastNodeName == "last-name")
                            author.Last_name += xml.Value;
                        else if (lastNodeName == "middle-name")
                            author.Middle_name += xml.Value;
                        else if (lastNodeName == "nickname")
                            author.Nickname += xml.Value;
                        else if (lastNodeName == "email")
                            author.Email += xml.Value;
                        else if (lastNodeName == "home-page")
                            author.Home_page += xml.Value;
                        else if (lastNodeName == "id")
                            author.UniqID += xml.Value;
                        break;
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
                        else if(xml.Name == "book-title")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if(xml.Name == "keywords")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if(xml.Name == "lang")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if(xml.Name == "src-lang")
                        {
                            lastNodeName = xml.Name;
                        }
                        else if (xml.Name == "author")
                        {
                            Writers writer = new Writers();
                            ParseTitleInfoWriter(xml.ReadSubtree(), writer);
                            TitleInfo_writers.Add(writer);
                        }
                        else if (xml.Name == "translator")
                        {
                            Translators translator = new Translators();
                            ParseTitleInfoTranslator(xml.ReadSubtree(), translator);
                            TitleInfo_translators.Add(translator);
                        }
                        else if (xml.Name == "coverpage")
                        {
                            if (xml.HasAttributes)
                            {
                                while (xml.MoveToNextAttribute())
                                {
                                    if (xml.Name == "image")
                                    {
                                        TitleInfo_coverpage = xml.Value;
                                        break;
                                    }
                                }
                            }
                        }

                        break;
                    case XmlNodeType.Text:
                        if (lastNodeName == "annotation")
                        {
                            TitleInfo_annotation += xml.Value;
                        }
                        else if(lastNodeName == "book-title")
                        {
                            TitleInfo_book_title = xml.Value;
                        }
                        else if(lastNodeName == "keywords")
                        {
                            TitleInfo_keyword = xml.Value;
                        }
                        else if(lastNodeName == "lang")
                        {
                            TitleInfo_lang = xml.Value;
                        }
                        else if(lastNodeName == "src-lang")
                        {
                            TitleInfo_src_lang = xml.Value;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (xml.Name == "member")
                        {
                        }
                        break;
                }
            }
            //TitleInfo_date = elTitleInfo.Element(ns + "date")?.Attribute("value") == null ? elTitleInfo.Element(ns + "date")?.Value : elTitleInfo.Element(ns + "date")?.Attribute("value")?.Value;
            //TitleInfo_coverpage = elTitleInfo.Element(ns + "coverpage")?.Element(ns + "image")?.Attributes()?.Select(p => p.Value.Split('#')[1]).ToList()[0];

            //TitleInfo_genres = elTitleInfo.Elements(ns + "genre").Select(p => p.Value).ToList();
        }

        private void ParseTitleInfoTranslator(XmlReader xml, Translators translator)
        {
            string lastNodeName = "";
            while (xml.Read())
            {
                switch (xml.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xml.Name == "first-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "last-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "middle-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "nickname")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "email")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "home-page")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "id")
                            lastNodeName = xml.Name;
                        break;
                    case XmlNodeType.Text:
                        if (lastNodeName == "first-name")
                            translator.First_name += xml.Value;
                        else if (lastNodeName == "last-name")
                            translator.Last_name += xml.Value;
                        else if (lastNodeName == "middle-name")
                            translator.Middle_name += xml.Value;
                        else if (lastNodeName == "nickname")
                            translator.Nickname += xml.Value;
                        else if (lastNodeName == "email")
                            translator.Email += xml.Value;
                        else if (lastNodeName == "home-page")
                            translator.Home_page += xml.Value;
                        else if (lastNodeName == "id")
                            translator.UniqID += xml.Value;
                        break;
                }
            }
        }

        private void ParseTitleInfoWriter(XmlReader xml, Writers writer)
        {
            string lastNodeName = "";
            while (xml.Read())
            {
                switch (xml.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xml.Name == "first-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "last-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "middle-name")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "nickname")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "email")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "home-page")
                            lastNodeName = xml.Name;
                        else if (xml.Name == "id")
                            lastNodeName = xml.Name;
                        break;
                    case XmlNodeType.Text:
                        if (lastNodeName == "first-name")
                            writer.First_name += xml.Value;
                        else if (lastNodeName == "last-name")
                            writer.Last_name += xml.Value;
                        else if (lastNodeName == "middle-name")
                            writer.Middle_name += xml.Value;
                        else if (lastNodeName == "nickname")
                            writer.Nickname += xml.Value;
                        else if (lastNodeName == "email")
                            writer.Email += xml.Value;
                        else if (lastNodeName == "home-page")
                            writer.Home_page += xml.Value;
                        else if (lastNodeName == "id")
                            writer.UniqID += xml.Value;
                        break;
                }
            }
        }
    }
}
