using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Fb2Editing
{
    public class Parse
    {
        public List<string> ti_genre { get; private set; }
        public List<Writers> ti_writer { get; private set; }
        public string ti_book_title { get; private set; }
        public string ti_annotation { get; private set; }
        public string ti_keywords { get; private set; }
        public string ti_date { get; private set; }
        public string ti_coverpage { get; private set; }
        public string ti_lang { get; private set; }
        public string ti_src_lang  { get; private set; }
        public List<Translator> ti_translator { get; private set; }
        public List<string> ti_sequence { get; private set; }

        public List<Author> di_author { get; private set; }
        public string di_program_used { get; private set; }
        public string di_date { get; private set; }
        public List<string> di_src_url { get; private set; }
        public string di_src_ocr { get; private set; }
        public string di_id { get; private set; }
        public string di_version { get; private set; }
        public string di_history { get; private set; }

        public string pi_book_name { get; private set; }
        public string pi_publisher { get; private set; }
        public string pi_city { get; private set; }
        public string pi_year { get; private set; }

        public Parse(string str)
        {
            //CreateBook(str);
            ParseData(str);
        }

        private void ParseData(string str)
        {
            string lastNodeName = "";

            //using поток

            //using (XmlReader xml = XmlReader.Create(str))
            //{
            //    while (xml.Read())
            //    {
            //        switch (xml.NodeType)
            //        {
            //            case XmlNodeType.Element:
            //                // нашли элемент member
            //                if (xml.Name == "description")
            //                {

            //                    //if (xml.HasAttributes)
            //                    //{
            //                    //    // поиск атрибута kuid
            //                    //    while (xml.MoveToNextAttribute())
            //                    //    {
            //                    //        if (xml.Name == "kuid")
            //                    //        {
            //                    //            Console.WriteLine("KUID: {0}", xml.Value);
            //                    //            break;
            //                    //        }
            //                    //    }
            //                    //}
            //                }
            //                // запоминаем имя найденного элемента
            //                lastNodeName = xml.Name;
            //                break;
            //            case XmlNodeType.Text:
            //                // нашли текст, смотрим по имени элемента, что это за текст
            //                if (lastNodeName == "nickname")
            //                {
            //                    Console.WriteLine("Псевдоним: {0}", xml.Value);
            //                }
            //                else if (lastNodeName == "firstName")
            //                {
            //                    Console.WriteLine("Имя: {0}", xml.Value);
            //                }
            //                else if (lastNodeName == "lastName")
            //                {
            //                    Console.WriteLine("Фамилия: {0}", xml.Value);
            //                }
            //                break;
            //            case XmlNodeType.EndElement:
            //                // закрывающий элемент
            //                if (xml.Name == "member")
            //                {
            //                    Console.WriteLine("------------------------------------------");
            //                }
            //                break;
            //        }
            //    }
            //}
        }

        private void CreateBook(string str)
        {
            string description = GetSubstring("description", str);
            {
                string titleInfo = GetSubstring("title-info", description);
                {
                    ti_genre = GetFewSubstring("genre", titleInfo);
                    List<string> lstWriter = GetFewSubstring("author", titleInfo);
                    {
                        ti_writer = new List<Writers>();
                        foreach (var item in lstWriter)
                        {
                            Writers writer = new Writers()
                            {
                                First_name = GetSubstring("first_name", item),
                                Last_name = GetSubstring("last_name", item),
                                Middle_name = GetSubstring("middle_name", item),
                                Nickname = GetSubstring("nickname", item),
                                Email = GetSubstring("email", item),
                            };
                            ti_writer.Add(writer);
                        }
                    }
                    ti_book_title = GetSubstring("book-title", titleInfo);
                    ti_annotation = GetSubstring("annotation", titleInfo);
                    ti_date = GetSubstring("date", titleInfo);
                    ti_coverpage = GetSubstring("coverpage", titleInfo);
                    ti_lang = GetSubstring("lang", titleInfo);
                    ti_src_lang = GetSubstring("src-lang", titleInfo);
                    //ti_translator = GetFewSubstring("translator", titleInfo);
                    List<string> lstTranslator = GetFewSubstring("translator", titleInfo);
                    {
                        ti_translator = new List<Translator>();
                        foreach (var item in lstTranslator)
                        {
                            Translator translator = new Translator()
                            {
                                First_name = GetSubstring("first_name", item),
                                Last_name = GetSubstring("last_name", item),
                                Middle_name = GetSubstring("middle_name", item),
                                Nickname = GetSubstring("nickname", item),
                                Email = GetSubstring("email", item),
                            };
                            ti_translator.Add(translator);
                        }
                    }

                    ti_sequence = GetFewSubstring("sequence", titleInfo);
                }
                string srcTitleInfo = GetSubstring("src-title-info", description);
                { }
                string documentInfo = GetSubstring("document-info", description);
                {
                    List<string> lstAuthor = GetFewSubstring("author", documentInfo);
                    {
                        di_author = new List<Author>();
                        foreach (var item in lstAuthor)
                        {
                            Author author = new Author()
                            {
                                First_name = GetSubstring("first_name", item),
                                //Last_name = GetSubstring("last_name", item),
                                //Middle_name = GetSubstring("middle_name", item),
                                //Nickname = GetSubstring("nickname", item),
                                //Email = GetSubstring("email", item),
                            };
                            di_author.Add(author);
                        }
                    }

                    di_program_used = GetSubstring("program-used", documentInfo);
                    di_date = GetSubstring("date", documentInfo);
                    di_src_url = GetFewSubstring("src-url", documentInfo);
                    di_src_ocr = GetSubstring("src-ocr", documentInfo);
                    di_id = GetSubstring("id", documentInfo);
                    di_version = GetSubstring("version", documentInfo);
                    di_history = GetSubstring("history", documentInfo);
                }
                string publishInfo = GetSubstring("publish-info", description);
                {
                    pi_book_name = GetSubstring("history", documentInfo);
                    pi_publisher = GetSubstring("publisher", documentInfo);
                    pi_city = GetSubstring("city", documentInfo);
                    pi_year = GetSubstring("year", documentInfo);
                }
                string customInfo = GetSubstring("custom-info", description);
            }
        }

        public string GetSubstring(string nameTag, string str)
        {
            Regex reg = new Regex($"(<{nameTag}[\\s\\S]*?[>])([\\s\\S]*?)</{nameTag}>");
            Match match = reg.Match(str);

            return match.Groups[2].ToString();
        }

        private List<string> GetFewSubstring(string nameTag, string str)
        {
            Regex reg = new Regex($"(<{nameTag}[\\s\\S]*?[>])([\\s\\S]*?)</{nameTag}>");
            MatchCollection matches = reg.Matches(str);
            List<string> lst = new List<string>();
            foreach (Match match in matches)
            {
                lst.Add(match.Groups[2].ToString()); //внутреннее содержимое
            }
            return lst;
        }



    }
}
