using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fb2Editing
{
    [Data]
    public class AllBook
    {
        public int ID { get; set; }
        public string PathToFile { get; set; } //путь к файлу документа
        public string UniqID { get; set; } //уникальный идентификатор документа FB2 (1 вхождение) <document-info>:<id>  
        public string Program_used { get; set; } //перечисляет программы, использованные при создании FB2-документа(От 0 до 1 вхождения) <document-info>:<program-used> 
        public string Date_create_document { get; set; } //дата создания документа(1 вхождение) <document-info>:<date>
        public string Src_ocr { get; set; } //автор текста, использованного при подготовке документа. Тот, кто сканировал ее и подготовил электронный текст(От 0 до 1 вхождения)<document-info>:<src-ocr> 
        public string Version { get; set; } //версия документа(1 вхождение) <document-info>:<version> 
        public string History { get; set; } //история создания и изменения документа (От 0 до 1 вхождения) <document-info>:<history>
        public string Book_title { get; set; } //название книги (1 вхождение) <title-info>:<book-title> 
        public string Annotation { get; set; } //аннотация книги (От 0 до 1 вхождения) <title-info>:<annotation> 
        public string Date_write { get; set; } //дата написания книги (От 0 до 1 вхождения) <title-info>:<date>
        //public string Coverpage { get; set; } //обложка книги(От 0 до 1 вхождения) <title-info>:<coverpage>  
        public string Book_name { get; set; } //название оригинальной (бумажной) книги(От 0 до 1 вхождения) <publish-info>:<book-name>
        public string Publisher { get; set; } // название издательства(От 0 до 1 вхождения) <publish-info>:<publisher>
        public string City { get; set; } //город, в котором издана книга(От 0 до 1 вхождения) <publish-info>:<city>
        public string Year { get; set; } //год издания книги(От 0 до 1 вхождения) <publish-info>:<year>
        public string Keywords { get; set; } // ключевые слова к данной книге для поисковых систем (От 0 до 1 вхождения) <title-info>:<keywords> 
        public string Code_Language { get; set; } // язык книги в документе, то есть язык после перевода(1 вхождение) <title-info>:<lang>
        public string Code_LanguageAfterTranslate { get; set; } // язык книги в документе, то есть язык после перевода(От 0 до 1 вхождения) <title-info>:<src-lang>
        public string Code_Genres { get; set; } //<title-info>:<genre> (1 и больше вхождений)
        //public string Writers { get; set; } //<title-info>:<author> (1 и более вхождений)
        //public string Authors { get; set; } //<document-info>:<author> (1 и более вхождений)
        //public string Translator { get; set; } //<document-info>:<author> (1 и более вхождений)
        //public string Sequences { get; set; } //<title-info>:<sequence> (любое число вхождений)
        //public string Src_urls { get; set; } //<document-info>:<src-url> (Любое число вхождений)
    }

    //[Data]
    //public class Genre
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public string Code { get; set; }

    //    public Genre(string name, string code)
    //    {
    //        Name = name;
    //        Code = code;
    //    }
    //}

    //[Data]
    //public class Language
    //{
    //    public int ID { get; set; }
    //    public string Value { get; set; }
    //    public string Code { get; set; }

    //    public Language(string value, string code)
    //    {
    //        Value = value;
    //        Code = code;
    //    }
    //}


    //[Data]
    //public class SettingsApp
    //{
    //    public int ID { get; set; }
    //    public string Name_Setting { get; set; }
    //    public string Value_Setting { get; set; }

    //    public SettingsApp(string name_Setting, string value_Setting)
    //    {
    //        Name_Setting = name_Setting;
    //        Value_Setting = value_Setting;
    //    }

    //}
}

