    namespace Forum.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // var db = new ForumDbContext();

            // ResetDatabase(db);

            // TryDifferentOutputs(db);

            System.Console.WriteLine("Hello");

        }

        //private static void TryDifferentOutputs(ForumDbContext db)
        //{
        //    //var categories = db.Categories
        //    //    .Include(c => c.Posts)
        //    //    .ThenInclude(p => p.Author)
        //    //    .Include(c => c.Posts)
        //    //    .ThenInclude(p => p.Replies)
        //    //    .ThenInclude(r => r.Author)
        //    //    .ToList();

        //    var categories = db.Categories
        //        .Select(c => new
        //        {
        //            CategoryName = c.Name,
        //            Posts = c.Posts.Select(p => new
        //            {
        //                Title = p.Title,
        //                Author = p.Author,
        //                Replies = p.Replies.Select(r => new
        //                {
        //                    Content = r.Content,
        //                    Author = r.Author
        //                })                          
        //            })               
        //        }).ToList();

        //    foreach (var c in categories)
        //    {
        //        Console.WriteLine($"Category: {c.CategoryName} has {c.Posts.Count()} posts:");

        //        foreach (var p in c.Posts)
        //        {
        //            Console.WriteLine($"--{p.Title} by {p.Author.Username} has {p.Replies.Count()} replies:");

        //            foreach (var r in p.Replies)
        //            {
        //                Console.WriteLine($"----{r.Content} by {r.Author.Username}");
        //            }
        //        }
        //    }
        //}

        //private static void ResetDatabase(ForumDbContext db)
        //{
        //    db.Database.EnsureDeleted();

        //    db.Database.Migrate();

        //    Seed(db);

        //}

        //private static void Seed(ForumDbContext db)
        //{
        //    var users = new List<User>()
        //    {
        //        new User("Ivan","Gringo"),
        //        new User( "Anastas","Bibmo"),
        //        new User( "Lev","Nikol")
        //    };


        //    var categories = new List<Category>()
        //    {
        //        new Category("Philosophy"),
        //        new Category( "Thechnics"),
        //        new Category( "Mind")
        //    };

        //    var posts = new List<Post>()
        //    {
        //        new Post("Kant","Do you know",categories[0],users[0]),
        //        new Post( "AI","No , I don't",categories[1],users[1]),
        //        new Post( "Gehstalt","It's your bussiness",categories[2],users[2])
        //    };

        //    var replies = new List<Reply>()
        //    {
        //        new Reply("KantDo you know",posts[0],users[0]),
        //        new Reply( "AINo , I don't",posts[1],users[1]),
        //        new Reply( "GehstaltIt's your bussiness",posts[2],users[2])
        //    };

        //    var tags = new List<Tag>()
        //    {
        //        new Tag("Svedenborg"),
        //        new Tag( "Silesiuz"),
        //        new Tag( "Bohme")
        //    };


           

        //    db.Users.AddRange(users);
        //    db.Tags.AddRange(tags);
        //    db.Posts.AddRange(posts);
        //    db.Replies.AddRange(replies);
        //    db.Categories.AddRange(categories);
        //    db.SaveChanges();

        //    db.PostsTags.Add(new PostTag(1, 1));
        //    db.PostsTags.Add(new PostTag(1, 2));
        //    db.PostsTags.Add(new PostTag(2, 3));
        //    db.PostsTags.Add(new PostTag(3, 3));
        //    db.PostsTags.Add(new PostTag(1, 3));

        //    db.SaveChanges();

        //}
    }
}
