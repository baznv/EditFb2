using System;


namespace Fb2Editing
{
    //public class BookFromFb2
    //{
    //    public Document document;
    //    public BookGenre bookGenre;
    //    public Book book;
    //    public BookWriter bookWriter;
    //    public Writers writers;
    //    public DocumentAuthor documentAuthor;
    //    public Authors authors;
    //    public BookTranslator bookTranslator;
    //    public Translator translator;
    //    public BookSequence bookSequence;
    //    public Sequence sequence;
    //    public Src_urls src_urls;

    //}
    [Data]
    public class Document
    {
        public int ID { get; set; }
        public string PathToFile { get; set; } //путь к файлу документа
        public string UniqID { get; set; } //уникальный идентификатор документа FB2 (1 вхождение) <document-info>:<id>  
        public string Program_used { get; set; } //перечисляет программы, использованные при создании FB2-документа(От 0 до 1 вхождения) <document-info>:<program-used> 
        public string Date_create_document { get; set; } //дата создания документа(1 вхождение) <document-info>:<date>
        public string Src_ocr { get; set; } //автор текста, использованного при подготовке документа. Тот, кто сканировал ее и подготовил электронный текст(От 0 до 1 вхождения)<document-info>:<src-ocr> 
        public string Version { get; set; } //версия документа(1 вхождение) <document-info>:<version> 
        public string History { get; set; } //история создания и изменения документа (От 0 до 1 вхождения) <document-info>:<history>
        public int ID_Book { get; set; }
    }
    [Data]
    public class BookGenre //<title-info>:<genre> (1 и больше вхождений)
    {
        public int ID { get; set; }
        public int ID_Genre { get; set; }
        public int ID_Book { get; set; }
    }
    [Data]
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public Genre(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
    [Data]
    public class Book
    {
        public int ID { get; set; }
        public string Book_title { get; set; } //название книги (1 вхождение) <title-info>:<book-title> 
        public string Annotation { get; set; } //аннотация книги (От 0 до 1 вхождения) <title-info>:<annotation> 
        public string Date_write { get; set; } //дата написания книги (От 0 до 1 вхождения) <title-info>:<date>
        public string CoverpageBinary { get; set; } //обложка книги(От 0 до 1 вхождения) <title-info>:<coverpage> (по атрибуту из тэга <binary>) 
        public string Book_name { get; set; } //название оригинальной (бумажной) книги(От 0 до 1 вхождения) <publish-info>:<book-name>
        public string Publisher { get; set; } // название издательства(От 0 до 1 вхождения) <publish-info>:<publisher>
        public string City { get; set; } //город, в котором издана книга(От 0 до 1 вхождения) <publish-info>:<city>
        public string Year { get; set; } //год издания книги(От 0 до 1 вхождения) <publish-info>:<year>
        public int ID_Language { get; set; } // язык книги в документе, то есть язык после перевода(1 вхождение) <title-info>:<lang>
        public int ID_LanguageAfterTranslate { get; set; } // язык книги в документе, то есть язык после перевода(От 0 до 1 вхождения) <title-info>:<src-lang>
        public string Keywords { get; set; } // ключевые слова к данной книге для поисковых систем (От 0 до 1 вхождения) <title-info>:<keywords> 

    }
    [Data]
    public class Language
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }

        public Language(string value, string code)
        {
            Value = value;
            Code = code;
        }
    }
    [Data]
    public class BookWriter //<title-info>:<author> (1 и более вхождений)
    {
        public int ID { get; set; }
        public int ID_Writer { get; set; }
        public int ID_Book { get; set; }
    }
    [Data]
    public class Writers
    {
        public int ID { get; set; }
        public string First_name { get; set; } //имя
        public string Last_name { get; set; } //фамилия
        public string Middle_name { get; set; } //отчество
        public string Nickname { get; set; } //ник
        public string Email { get; set; } //
        public string Home_page { get; set; } //
        public string UniqID { get; set; } //<id>
    }
    [Data]
    public class DocumentAuthor //<document-info>:<author> (1 и более вхождений)
    {
        public int ID { get; set; }
        public int ID_Author { get; set; }
        public int ID_Book { get; set; }
    }
    [Data]
    public class Authors
    {
        public int ID { get; set; }
        public string First_name { get; set; } //имя
        public string Last_name { get; set; } //фамилия
        public string Middle_name { get; set; } //отчество
        public string Nickname { get; set; } //ник
        public string Email { get; set; } //
        public string Home_page { get; set; } //
        public string UniqID { get; set; } //<id>

    }
    [Data]
    public class BookTranslator //<title-info>:<translator> (любое число вхождений)
    {
        public int ID { get; set; }
        public int ID_Translator { get; set; }
        public int ID_Book { get; set; }
    }
    [Data]
    public class Translators
    {
        public int ID { get; set; }
        public string First_name { get; set; } //имя
        public string Last_name { get; set; } //фамилия
        public string Middle_name { get; set; } //отчество
        public string Nickname { get; set; } //ник
        public string Email { get; set; } //
        public string Home_page { get; set; } //
        public string UniqID { get; set; } //<id>

    }
    [Data]
    public class BookSequence //<title-info>:<sequence> (любое число вхождений)
    {
        public int ID { get; set; }
        public int ID_Sequence { get; set; }
        public int ID_Book { get; set; }
        public int Number_in_sequence { get; set; }
    }
    [Data]
    public class Sequence
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    [Data]
    public class Src_urls //<document-info>:<src-url> (Любое число вхождений)
    {
        public int ID { get; set; }
        public int ID_Book { get; set; }
        public string Url { get; set; }
    }

    [Data]
    public class SettingsApp
    {
        public int ID { get; set; }
        public string Name_Setting { get; set; }
        public string Value_Setting { get; set; }

        public SettingsApp(string name_Setting, string value_Setting)
        {
            Name_Setting = name_Setting;
            Value_Setting = value_Setting;
        }

    }

    [AttributeUsage(AttributeTargets.Class)]
    class DataAttribute : Attribute
    {
        //Для указания атрибутов у класса для данных (создание DB), чтобы вручную не перебирать
        public DataAttribute()
        { }
    }

}
