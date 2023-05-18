/*

Many TO many Relationship
    -   easy and intersting
    -   3 ways
            1.  Skip Navigations
                -   Most common Direct refs from both ends
            2.  Skip with Payload (not coverd, boaring)
                -   Allows database generated data in extra columns
            3.  Explicit Join Class (not coverd, boaring)
                -   Additional properties accessible via code

Skip Navigations
         just create 2 class and add List<OtherClass> list property in both classes as below
         public class Artist
            {
                public int Id { get; set; }
                public string Name { get; set;}
                public List<Cover> Covers { get; set; }  // many to many
            }

         public class Cover
            {
                public int Id { get; set; }
                public string Name { get; set;}
                public List<Artist> Artists { get; set; }  // many to many
            }

        -   under the hood it will create 3 tables
            -   1.  Artists
            -   2.  Covers
            -   3.  ArtistsCovers 
                    -   ArtistsArtistId
                    -   CoversCoverId
                        -   both are compsite key which enforecr uniqe entry per row.


-   Add Manupulate *.*
    Joining Covers and Artists of Differing States

    1.  Existing Cover + Existing Artist
            -   Existing artist is assigned to a pre-defined book Cover
    2.  New Cover + Existing Artist
            -   New artist is hired to work on a pre-defined book Cover 
    3.  New Cover + New Artist
            -   New artist is hired and declares a new book Cover
                    
    Skip Navigations Require Objects as it dont have Id which we have in One to Many relaitonship
        in one to many we can have followoing   
        public class Book
            {
            ...other properties
            public int AuthorId {get;set;}
            }

    -   for addition see EfMaytoMany.cs // it will make sense easy and interesting

    For delete many to many see EfMayToManyDelete class easy and clean
    also look ReassignACover :- changing joins in M M relationship 
*/ 