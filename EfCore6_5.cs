/*
    
    One to one relationship
        -   DbContext must be able to identify a principal ("parent") and a dependent ("child")
        -   how to identfy parent and child
            -   add Type + Id FK in class that is consider as dependant / child class
    
        public class Book
        {
            int Id
            string Name
            Cover Cover;    //
        }

    public class Cover
        {
            int Id
            string Address
            Book Book;      //
            int BookId;     // imples depedndant ie child
        }

    -   A Cavalcade of Cascade Deletes
            Author    ->    Books     ->   Covers     ->   Relationships to Artist

    -   You can edit migrations files that have not yet been applied to the database.

    -   Order of migrations
            Adds new BookId column
            Updates the BookId column values
            Applies Index
            Adds foreign key constraint

    -   Reassigning a Cover to a Different Book
        -   Cover originally assigned to a blue book
            and  Reassigning it to a green book

        greenCover.BookId=3
        greenCover.Book= greenBookObject
        greenCover=blueBook.Cover;
        greenBook.Cover= greenCover ;


*/