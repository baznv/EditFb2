BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Language" (
	"ID"	INTEGER,
	"Value"	TEXT,
	"Code"	TEXT,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "Keywords" (
	"ID"	INTEGER,
	"Word"	TEXT,
	"ID_Book"	INTEGER,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "BookSequence" (
	"ID"	INTEGER,
	"ID_Sequence"	INTEGER,
	"ID_Book"	INTEGER,
	"Number_in_sequence"	INTEGER,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "BookGenres" (
	"ID"	INTEGER,
	"ID_Genre"	INTEGER,
	"ID_Book"	INTEGER,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "Books" (
	"ID"	INTEGER,
	"Book_title"	TEXT,
	"Annotation"	TEXT,
	"UniqID"	TEXT,
	"Date_write"	TEXT,
	"Coverpage"	TEXT,
	"Path"	TEXT,
	"Program_used"	TEXT,
	"Date_create_document"	TEXT,
	"Src_ocr"	TEXT,
	"Version"	TEXT,
	"Book_name"	TEXT,
	"Publisher"	TEXT,
	"City"	TEXT,
	"Year"	TEXT,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "Genres" (
	"ID"	INTEGER,
	"Code"	TEXT,
	"Name"	TEXT,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "BookAuthors" (
	"ID"	INTEGER,
	"ID_Authors"	INTEGER,
	"ID_Books"	INTEGER,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "AuthorsDocument" (
	"ID"	INTEGER,
	"First_name"	TEXT,
	"Last_name"	TEXT,
	"Middle_name"	TEXT,
	"Nickname"	TEXT,
	"Email"	TEXT,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "Src_urls" (
	"ID"	INTEGER,
	"Url"	TEXT,
	"ID_Book"	INTEGER,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "Sequences" (
	"ID"	INTEGER,
	"Name"	TEXT,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "BookTranslators" (
	"ID"	INTEGER,
	"ID_Translators"	INTEGER,
	"ID_Books"	INTEGER,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "Translators" (
	"ID"	INTEGER,
	"First_name"	TEXT,
	"Last_name"	TEXT,
	"Middle_name"	TEXT,
	"Nickname"	TEXT,
	"Email"	TEXT,
	PRIMARY KEY("ID")
);
CREATE TABLE IF NOT EXISTS "AuthorsBook" (
	"ID"	INTEGER,
	"First_Name"	TEXT,
	"Last_Name"	TEXT,
	"Middle_Name"	TEXT,
	"Nickname"	TEXT,
	"Email"	TEXT,
	PRIMARY KEY("ID")
);
COMMIT;
