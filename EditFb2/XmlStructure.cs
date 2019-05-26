using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Fb2Editing
{
 
    [Serializable]
    [XmlRoot("title-info", Namespace = "")]
    public class TitleInfo
    {
        [XmlElement("genre")]
        public List<string> Genre { get; set; }
        [XmlElement("author")]
        public List<Author> AuthorBook { get; set; }
        [XmlElement("book-title")]
        public string BookTitle { get; set; }

        [XmlElement("keywords")]
        public string Keywords { get; set; }
        [XmlElement("date")]
        public string Date { get; set; }
        [XmlElement("lang")]
        public string Language { get; set; }


        private XmlElement annotation;
        [XmlElement("annotation")]
        public XmlElement Annotation //{ get; set; }
        {
            get
            {
                return annotation;
            }
            set
            {
                XmlDocument xdoc = new XmlDocument();
                XmlNode songNode = xdoc.CreateNode(XmlNodeType.Element, "annotation", "");

                XmlElement e = value.OwnerDocument.CreateElement("MyNewElement");
                e.InnerText = value.InnerText;
                songNode.AppendChild(e);

                //songNode.AppendChild(value);
                //XmlNode node = xdoc.ReadNode(value);
                //annotation = songNode;
            }
        }


        public TitleInfo() { }

        //public TitleInfo(string xml)
        //{
        //    XmlSerializer mySerializer = new XmlSerializer(this.GetType(), new XmlRootAttribute("title-info"));
        //    TitleInfo myObject;
        //    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
        //    {
        //        myObject = (TitleInfo)mySerializer.Deserialize(ms);
        //    }

        //    this.AuthorBook = myObject.AuthorBook;
        //}


        //public void LoadXml(string source)
        //{
        //    XmlSerializer mySerializer = new XmlSerializer(this.GetType());
        //    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(source)))
        //    {
        //        object obj = mySerializer.Deserialize(ms);
        //        foreach (PropertyInfo p in obj.GetType().GetProperties())
        //        {
        //            PropertyInfo p2 = this.GetType().GetProperty(p.Name);
        //            if (p2 != null && p2.CanWrite)
        //            {
        //                p2.SetValue(this, p.GetValue(obj, null), null);
        //            }
        //        }
        //    }
        //}

    }

    //[Serializable]
    [XmlRoot("author")]
    public class Author
    {
        public int ID { get; set; }
        [XmlElement("first-name")]
        public string First_name { get; set; } //имя
        [XmlElement("last-name")]
        public string Last_name { get; set; } //фамилия
        [XmlElement("middle-name")]
        public string Middle_name { get; set; } //отчество
        [XmlElement("nickname")]
        public string Nickname { get; set; } //ник
        [XmlElement("email")]
        public string Email { get; set; } //


        public Author() { }

        //public Author(string xml)
        //{
        //    LoadXml(xml);
        //}


        //public void LoadXml(string source)
        //{
        //    XmlSerializer mySerializer = new XmlSerializer(this.GetType());
        //    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(source)))
        //    {
        //        object obj = mySerializer.Deserialize(ms);
        //        foreach (PropertyInfo p in obj.GetType().GetProperties())
        //        {
        //            PropertyInfo p2 = this.GetType().GetProperty(p.Name);
        //            if (p2 != null && p2.CanWrite)
        //            {
        //                p2.SetValue(this, p.GetValue(obj, null), null);
        //            }
        //        }
        //    }
        //}

    }

    public class XmlStructure
    {


    }
}
