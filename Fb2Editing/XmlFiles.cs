using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Fb2Editing
{
    public class XmlFiles
    {

        //internal static BookFromFb2 ReadDescriptionBook()
        //{
        //    throw new NotImplementedException();
        //}


        public static AllBook GetFieldFromFile(string filename)
        {
            AllBook ab = new AllBook();
            ab.PathToFile = filename;

            //using (StreamReader sr = new StreamReader(filename))
            //{
            using (XmlReader xml = XmlReader.Create(filename))
                {
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
                        //string tmp = xml.BaseURI;
                        //string tmp0 = xml.LookupNamespace("");
                        //string tmp1 = xml.NamespaceURI;
                    XNamespace ns = xml.LookupNamespace("");

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

                                    XElement elTitleInfo = XNode.ReadFrom(xml) as XElement;

                                    ab.Annotation = elTitleInfo.Element(ns + "annotation")?.Value;
                                        ab.Book_title = elTitleInfo.Element(ns + "book-title")?.Value;
                                        ab.City = elTitleInfo.Element(ns + "city")?.Value;
                                        ab.Date_write = elTitleInfo.Element(ns + "date")?.Attribute("value") == null ? elTitleInfo.Element(ns + "date")?.Value : elTitleInfo.Element(ns + "date")?.Attribute("value")?.Value;
                                        ab.Keywords = elTitleInfo.Element(ns + "keywords")?.Value;
                                        ab.Code_Language = elTitleInfo.Element(ns + "lang")?.Value.Replace(" ", string.Empty);
                                        ab.Code_LanguageAfterTranslate = elTitleInfo.Element(ns + "src-lang")?.Value.Replace(" ", string.Empty);
                                        //ab.Coverpage = elTitleInfo.Element(aw + "coverpage")?.Element(aw + "image")?.Attributes()?.Select(p => p.Value.Split('#')[1]).ToList()[0];
                                        ab.Code_Genres = String.Join("|", elTitleInfo.Elements(ns + "genre").Select(p => p.Value));

                                    }

                                    if (xml.Name == "src-title-info")
                                    {
                                        //var t = xml.ReadInnerXml();

                                    }
                                    if (xml.Name == "document-info")
                                    {
                                        XElement elDocumentInfo = XNode.ReadFrom(xml) as XElement;

                                        ab.Date_create_document = elDocumentInfo.Element(ns + "date")?.Attribute("value") == null ? elDocumentInfo.Element(ns + "date")?.Value : elDocumentInfo.Element(ns + "date")?.Attribute("value")?.Value;
                                        ab.History = elDocumentInfo.Element(ns + "history")?.Value;
                                        ab.Program_used = elDocumentInfo.Element(ns + "program-used")?.Value;
                                        ab.Src_ocr = elDocumentInfo.Element(ns + "src-ocr")?.Value;
                                        ab.UniqID = elDocumentInfo.Element(ns + "id")?.Value;
                                        ab.Version = elDocumentInfo.Element(ns + "version")?.Value;
                                    }
                                    if (xml.Name == "publish-info")
                                    {
                                        XElement elPublishInfo = XNode.ReadFrom(xml) as XElement;

                                        ab.Book_name = elPublishInfo?.Element(ns + "book-name")?.Value;
                                        ab.Publisher = elPublishInfo?.Element(ns + "publisher")?.Value;
                                        ab.Year = elPublishInfo?.Element(ns + "year")?.Value;

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

            //}
            return ab;
        }

    }
}
