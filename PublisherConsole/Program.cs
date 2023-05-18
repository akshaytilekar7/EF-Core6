//using Microsoft.EntityFrameworkCore;
//using PublisherData;
//using PublisherDomain;

//PubContext _context = new PubContext();
////this assumes you are working with the populated
////database created in previous module

////InsertAuthor();
////RetrieveAndUpdateAuthor();
////DeleteAnAuthor();
//RetrieveAndUpdateMultipleAuthors();
////CoordinatedRetrieveAndUpdateAuthor();
////VariousOperations();
////InsertMultipleAuthors();

//void InsertMultipleAuthors()
//{
//    var newAuthors = new Author[]{
//       new Author { FirstName = "Ruth", LastName = "Ozeki" },
//       new Author { FirstName = "Sofia", LastName = "Segovia" },
//       new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
//       new Author { FirstName = "Hugh", LastName = "Howey" },
//       new Author { FirstName = "Isabelle", LastName = "Allende" }
//    };
//    _context.AddRange(newAuthors);
//    _context.SaveChanges();
//}

//void DeleteAnAuthor()
//{
//    var extraJL = _context.Authors.Find(1);
//    if (extraJL != null)
//    {
//        _context.Authors.Remove(extraJL);
//        _context.SaveChanges();
//    }
//}

//void VariousOperations()
//{
//    var author = _context.Authors.Find(2); //this is currently Josie Newf
//    author.LastName = "Newfoundland";
//    var newauthor = new Author { LastName = "Appleman", FirstName = "Dan" };
//    _context.Authors.Add(newauthor);
//    _context.SaveChanges();
//}

//void RetrieveAndUpdateMultipleAuthors()
//{
//    var LermanAuthors = _context.Authors.Where(a => a.LastName == "Lerman").ToList();
//    //foreach (var la in LermanAuthors)
//    //{
//    //    la.LastName = "Lermann";
//    //}
//    var a1=LermanAuthors[0];
//    var a2=LermanAuthors[1];
//    a1 = null;
//    Console.WriteLine("Before" + _context.ChangeTracker.DebugView.ShortView);

//    //_context.ChangeTracker.DetectChanges();
//    //Console.WriteLine("After:" + _context.ChangeTracker.DebugView.ShortView);
//   // LermanAuthors.RemoveAt(0);
//    _context.ChangeTracker.DetectChanges();
//   // _context.SaveChanges();
//    Console.WriteLine("After:" + _context.ChangeTracker.DebugView.ShortView);
//}


//void InsertAuthor()
//{
//    var author = new Author { FirstName = "Frank", LastName = "Herbert" };
//    _context.Authors.Add(author);
//    _context.SaveChanges();
//}


//void InsertMultipleAuthorsPassedIn(List<Author> listOfAuthors)
//{
//    _context.Authors.AddRange(listOfAuthors);
//    _context.SaveChanges();
//}

//void BulkAddUpdate()
//{
//    var newAuthors = new Author[]{
//     new Author { FirstName = "Tsitsi", LastName = "Dangarembga" },
//     new Author { FirstName = "Lisa", LastName = "See" },
//     new Author { FirstName = "Zhang", LastName = "Ling" },
//     new Author { FirstName = "Marilynne", LastName="Robinson"}
//    };
//    _context.Authors.AddRange(newAuthors);
//    var book = _context.Books.Find(2);
//    book.Title = "Programming Entity Framework 2nd Edition";
//    _context.SaveChanges();
//}