using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fb2Editing
{
    //public class Book
    //{
    //    public string Path { get; set; }
    //    public DescriptionBook Description { get; set; }
    //}

    //public class DescriptionBook
    //{
    //    public TitleInfo Title_info { get; set; } //данные о книге
    //    public string Src_title_info { get; set; } = null; //данные об исходнике книги (до перевода)
    //    public DocumentInfo Document_info { get; set; } //информация об FB2-документе
    //    public PublishInfo Publish_info { get; set; } //сведения об издании книги, которая была использована как источник при подготовке документа.
    //    public string Custom_info { get; set; } //произвольная информация
    //}

    //public class TitleInfo
    //{
    //    //Все возможные по стандарту жанры
    //    public Dictionary<string, string> Genres { get; set; } =
    //        new Dictionary<string, string>
    //        {
    //            { "sf_history", "Альтернативная история"},
    //            { "sf_action", "Боевая фантастика"},
    //            { "sf_epic", "Эпическая фантастика"},
    //            { "sf_heroic", "Героическая фантастика"},
    //            { "sf_detective", "Детективная фантастика"},
    //            { "sf_cyberpunk", "Киберпанк"},
    //            { "sf_space", "Космическая фантастика"},
    //            { "sf_social", "Социально-психологическая фантастика"},
    //            { "sf_horror", "Ужасы и Мистика"},
    //            { "sf_humor", "Юмористическая фантастика"},
    //            { "sf_fantasy", "Фэнтези"},
    //            { "sf", "Научная Фантастика"},
    //            { "det_classic", "Классический детектив"},
    //            { "det_police", "Полицейский детектив"},
    //            { "det_action", "Боевик"},
    //            { "det_irony", "Иронический детектив"},
    //            { "det_history", "Исторический детектив"},
    //            { "det_espionage", "Шпионский детектив"},
    //            { "det_crime", "Криминальный детектив"},
    //            { "det_political", "Политический детектив"},
    //            { "det_maniac", "Маньяки"},
    //            { "det_hard", "Крутой детектив"},
    //            { "thriller", "Триллер"},
    //            { "detective", "Детектив (не относящийся в прочие категории)"},
    //            { "prose_classic", "Классическая проза"},
    //            { "prose_history", "Историческая проза"},
    //            { "prose_contemporary", "Современная проза"},
    //            { "prose_counter", "Контркультура"},
    //            { "prose_rus_classic", "Русская классическая проза"},
    //            { "prose_su_classics", "Советская классическая проза"},
    //            { "love_contemporary", "Современные любовные романы"},
    //            { "love_history", "Исторические любовные романы"},
    //            { "love_detective", "Остросюжетные любовные романы"},
    //            { "love_short", "Короткие любовные романы"},
    //            { "love_erotica", "Эротика"},
    //            { "adv_western", "Вестерн"},
    //            { "adv_history", "Исторические приключения"},
    //            { "adv_indian", "Приключения про индейцев"},
    //            { "adv_maritime", "Морские приключения"},
    //            { "adv_geo", "Путешествия и география"},
    //            { "adv_animal", "Природа и животные"},
    //            { "adventure", "Прочие приключения (то, что не вошло в другие категории)"},
    //            { "child_tale", "Сказка"},
    //            { "child_verse", "Детские стихи"},
    //            { "child_prose", "Детскиая проза"},
    //            { "child_sf", "Детская фантастика"},
    //            { "child_det", "Детские остросюжетные"},
    //            { "child_adv", "Детские приключения"},
    //            { "child_education", "Детская образовательная литература"},
    //            { "children", "Прочая детская литература (то, что не вошло в другие категории)"},
    //            { "poetry", "Поэзия"},
    //            { "dramaturgy", "Драматургия"},
    //            { "antique_ant", "Античная литература"},
    //            { "antique_european", "Европейская старинная литература"},
    //            { "antique_russian", "Древнерусская литература"},
    //            { "antique_east", "Древневосточная литература"},
    //            { "antique_myths", "Мифы. Легенды. Эпос"},
    //            { "antique", "Прочая старинная литература (то, что не вошло в другие категории)"},
    //            { "sci_history", "История"},
    //            { "sci_psychology", "Психология"},
    //            { "sci_culture", "Культурология"},
    //            { "sci_religion", "Религиоведение"},
    //            { "sci_philosophy", "Философия"},
    //            { "sci_politics", "Политика"},
    //            { "sci_business", "Деловая литература"},
    //            { "sci_juris", "Юриспруденция"},
    //            { "sci_linguistic", "Языкознание"},
    //            { "sci_medicine", "Медицина"},
    //            { "sci_phys", "Физика"},
    //            { "sci_math", "Математика"},
    //            { "sci_chem", "Химия"},
    //            { "sci_biology", "Биология"},
    //            { "sci_tech", "Технические науки"},
    //            { "science", "Прочая научная литература (то, что не вошло в другие категории)"},
    //            { "comp_www", "Интернет"},
    //            { "comp_programming", "Программирование"},
    //            { "comp_hard", "Компьютерное \"железо\" (аппаратное обеспечение)"},
    //            { "comp_soft", "Программы"},
    //            { "comp_db", "Базы данных"},
    //            { "comp_osnet", "ОС и Сети"},
    //            { "computers", "Прочая околокомпьтерная литература (то, что не вошло в другие категории)"},
    //            { "ref_encyc", "Энциклопедии"},
    //            { "ref_dict", "Словари"},
    //            { "ref_ref", "Справочники"},
    //            { "ref_guide", "Руководства"},
    //            { "reference", "Прочая справочная литература (то, что не вошло в другие категории)"},
    //            { "nonf_biography", "Биографии и Мемуары"},
    //            { "nonf_publicism", "Публицистика"},
    //            { "nonf_criticism", "Критика"},
    //            { "design", "Искусство и Дизайн"},
    //            { "nonfiction", "Прочая документальная литература (то, что не вошло в другие категории)"},
    //            { "religion_rel", "Религия"},
    //            { "religion_esoterics", "Эзотерика"},
    //            { "religion_self", "Самосовершенствование"},
    //            { "religion", "Прочая религионая литература (то, что не вошло в другие категории)"},
    //            { "humor_anecdote", "Анекдоты"},
    //            { "humor_prose", "Юмористическая проза"},
    //            { "humor_verse", "Юмористические стихи"},
    //            { "humor", "Прочий юмор (то, что не вошло в другие категории)"},
    //            { "home_cooking", "Кулинария"},
    //            { "home_pets", "Домашние животные"},
    //            { "home_crafts", "Хобби и ремесла"},
    //            { "home_entertain", "Развлечения"},
    //            { "home_health", "Здоровье"},
    //            { "home_garden", "Сад и огород"},
    //            { "home_diy", "Сделай сам"},
    //            { "home_sport", "Спорт"},
    //            { "home_sex", "Эротика, Секс"},
    //            { "home", "Прочиее домоводство (то, что не вошло в другие категории)"}
    //        };
    //    //Автор
    //    public Author[] AuthorBook { get; set; }
    //    public string Book_title { get; set; } // название книги 
    //    public string Annotation { get; set; }
    //    public string Keywords { get; set; } //ключевые слова к данной книге для поисковых систем
    //    public string Date { get; set; } //дата написания книги
    //    public string Lang { get; set; } //язык книги в документе, то есть язык после перевода
    //    public string Src_lang { get; set; } //язык, на котором исходно написана книга, то есть язык до перевода
    //    public Author Translator { get; set; } //информация о переводчике книги
    //    public Sequence[] SequenceBook { get; set; } // сведения о том, к каким сериям относится книга
    //}

    //public class DocumentInfo
    //{
    //    public Author AuthorDocument { get; set; } //содержит информацию об авторе документа
    //    public string Program_used { get; set; } //перечисляет программы, использованные при создании FB2-документа.
    //    public string Date { get; set; } //дата создания документа
    //    public string Src_url { get; set; } //URL страницы, откуда взят текст для подготовки документа
    //    public Author Src_ocr { get; set; } // автор, который сканировал книгу и подготовил электронный текст.
    //    public string Id { get; set; } //уникальный идентификатор документа FB2. Каждый отдельный FB2-документ должен иметь собственный ID. Это значит, что при изменении книги, которая есть в библиотеке, ID нужно сохранить
    //    public string Version { get; set; } //версия документа в текстовом виде
    //    public string History { get; set; } //история создания и изменения документа
    //}

    //public class PublishInfo
    //{
    //    public string Book_name { get; set; } //название оригинальной (бумажной) книги.
    //    public string Publisher { get; set; } //название издательства
    //    public string City { get; set; } //город, в котором издана книга
    //    public string Year { get; set; } //год издания книги 

    //}

    //public class Author
    //{
    //    public string First_name { get; set; } //имя
    //    public string Last_name  { get; set; } //фамилия
    //    public string Middle_name { get; set; } //отчество
    //    public string Nickname { get; set; } //ник
    //    public string Email { get; set; } //email
    //}

    //public class Sequence
    //{
    //    public string Name { get; set; } //название серии
    //    public int Number { get; set; } //порядковый номер книги в серии
    //}
}
