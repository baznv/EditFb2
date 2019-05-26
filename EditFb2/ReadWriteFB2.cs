using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Fb2Editing
{
    public class ReadWriteFB2
    {
        public List<string> Title_info_genre { get; private set; } = new List<string>();
        public List<Writers> Title_info_writer { get; private set; } = new List<Writers>();
        public string Title_info_book_title { get; private set; }
        public string Title_info_annotation { get; private set; }
        public string Title_info_keywords { get; private set; }
        public string Title_info_date { get; private set; }
        public string Title_info_coverpage { get; private set; }
        public string Title_info_lang { get; private set; }
        public string Title_info_src_lang { get; private set; }
        public List<Translator> Title_info_translator { get; private set; } = new List<Translator>();
        public List<string> Title_info_sequence { get; private set; } = new List<string>();

        public List<Author> Document_info_author { get; private set; } = new List<Author>();
        public string Document_info_program_used { get; private set; }
        public string Document_info_date { get; private set; }
        public List<string> Document_info_src_url { get; private set; } = new List<string>();
        public string Document_info_src_ocr { get; private set; }
        public string Document_info_id { get; private set; }
        public string Document_info_version { get; private set; }
        public string Document_info_history { get; private set; }

        public string Publish_info_book_name { get; private set; }
        public string Publish_info_publisher { get; private set; }
        public string Publish_info_city { get; private set; }
        public string Publish_info_year { get; private set; }

        public Document DocumentInfo { get; private set; }

        public void GetFieldFromFile(string filename)
        {


            using (StreamReader sr = new StreamReader(filename))
            {
                //    string str = null;
                //    List<string> lstNamespace = new List<string>();

                //    while (!sr.EndOfStream)
                //    {
                //        str = sr.ReadLine();
                //        Regex reg = new Regex("xmlns([\\s\\S]*?[\"]){2}");
                //        MatchCollection matches = reg.Matches(str);
                //        foreach (Match match in matches)
                //        {
                //            lstNamespace.Add(match.ToString()); 
                //        }
                //        if (lstNamespace.Count != 0)
                //            break;
                //    }


                using (XmlReader xml = XmlReader.Create(filename))
                {
                    bool continueRead = true;

                    while (xml.Read())
                    {
                        string tmp = xml.BaseURI;
                    string tmp0 = xml.LookupNamespace("");
                    string tmp1 = xml.NamespaceURI;
                    //xml.ReadSubtree();
                    //string tmp2 = xml.na

                    //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    //ns.Add("myNamespace", tmp0);

                    
                    if (!continueRead)
                        {
                            break;
                        }
                        switch (xml.NodeType)
                        {
                            case XmlNodeType.Element:
                                {
                                    if (xml.Name == "title-info")
                                    {
                                    }

                                    if (xml.Name == "src-title-info")
                                    {
                                        var t = xml.ReadInnerXml();

                                    }
                                    if (xml.Name == "document-info")
                                    {
                                        var t = xml.ReadInnerXml();
                                        Document doc = new Document(t, tmp0);
                                    }
                                    if (xml.Name == "publish-info")
                                    {
                                        var t = xml.ReadInnerXml();

                                    }

                                    //continueRead = false;
                                    break;
                                }
                        }
                    }
                }

            }
        }


    }
}
