using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EditFb2
{
    public class Document
    {
        public int ID { get; set; }
        public string Path { get; set; } //путь к файлу документа
        public string UniqID { get; set; } //уникальный идентификатор документа FB2 (1 вхождение) <document-info>:<id>  
        public string Program_used { get; set; } //перечисляет программы, использованные при создании FB2-документа(От 0 до 1 вхождения) <document-info>:<program-used> 
        public string Date_create_document { get; set; } //дата создания документа(1 вхождение) <document-info>:<date>
        public string Src_ocr { get; set; } //автор текста, использованного при подготовке документа. Тот, кто сканировал ее и подготовил электронный текст(От 0 до 1 вхождения)<document-info>:<src-ocr> 
        public string Version { get; set; } //версия документа(1 вхождение) <document-info>:<version> 
        public string History { get; set; } //история создания и изменения документа (От 0 до 1 вхождения) <document-info>:<history>
        public int ID_Book { get; set; }
    }

    public class BookGenre //<title-info>:<genre>
    {
        public int ID { get; set; }
        public int ID_Genre { get; set; }
        public int ID_Book { get; set; }
    }

    public class Genre 
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Book
    {
        public int ID { get; set; }
        public string Book_title { get; set; } //название книги (1 вхождение) <title-info>:<book-title> 
        public string Annotation { get; set; } //аннотация книги (От 0 до 1 вхождения) <title-info>:<annotation> 
        public string Date_write { get; set; } //дата написания книги (От 0 до 1 вхождения) <title-info>:<date>
        public string Coverpage { get; set; } //обложка книги(От 0 до 1 вхождения) <title-info>:<coverpage>  
        public string Book_name { get; set; } //название оригинальной (бумажной) книги(От 0 до 1 вхождения) <publish-info>:<book-name>
        public string Publisher { get; set; } // название издательства(От 0 до 1 вхождения) <publish-info>:<publisher>
        public string City { get; set; } //город, в котором издана книга(От 0 до 1 вхождения) <publish-info>:<city>
        public string Year { get; set; } //год издания книги(От 0 до 1 вхождения) <publish-info>:<city>
        public int ID_Language { get; set; } // язык книги в документе, то есть язык после перевода(1 вхождение) <title-info>:<lang>
        public int ID_LanguageAfterTranslate { get; set; } // язык книги в документе, то есть язык после перевода(От 0 до 1 вхождения) <title-info>:<src-lang> 
    }

    public class Language
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }

    public class BookWriter //<title-info>:<author> (1 и более вхождений)
    {
        public int ID { get; set; }
        public int ID_Writer { get; set; }
        public int ID_Book { get; set; }
    }

    public class Writers
    {
        public int ID { get; set; }
        public string First_name { get; set; } //имя
        public string Last_name { get; set; } //фамилия
        public string Middle_name { get; set; } //отчество
        public string Nickname { get; set; } //ник
        public string Email { get; set; } //
    }

    public class DocumentAuthor //<document-info>:<author> (1 и более вхождений)
    {
        public int ID { get; set; }
        public int ID_Author { get; set; }
        public int ID_Book { get; set; }
    }

    public class Author
    {
        public int ID { get; set; }
        public string First_name { get; set; } //имя
        public string Last_name { get; set; } //фамилия
        public string Middle_name { get; set; } //отчество
        public string Nickname { get; set; } //ник
        public string Email { get; set; } //
    }


    public class BookTranslator //<title-info>:<translator>  
    {
        public int ID { get; set; }
        public int ID_Author { get; set; }
        public int ID_Book { get; set; }
    }

    public class Translator
    {
        public int ID { get; set; }
        public string First_name { get; set; } //имя
        public string Last_name { get; set; } //фамилия
        public string Middle_name { get; set; } //отчество
        public string Nickname { get; set; } //ник
        public string Email { get; set; } //
    }


    public class BookSequence //<title-info>:<sequence>
    {
        public int ID { get; set; }
        public int ID_Sequence { get; set; }
        public int ID_Book { get; set; }
        public int Number_in_sequence { get; set; }
    }


    public class Keyword //<title-info>:<keywords>
    {
        public int ID { get; set; }
        public int ID_Document { get; set; }
        public string Word { get; set; }
    }

    public class Sequence
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Src_urls //<document-info>:<src-url> (Любое число вхождений)
    {
        public int ID { get; set; }
        public int ID_Book { get; set; }
        public string Url { get; set; }
    }


}
