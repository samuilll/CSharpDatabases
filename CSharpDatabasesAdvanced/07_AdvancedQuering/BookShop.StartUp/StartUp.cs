namespace BookShop
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        static void Main()
        {
            using (var db = new BookShopContext())
            {
                // DbInitializer.ResetDatabase(db);
                Console.WriteLine(CountCopiesByAuthor(db));
            }
        }

        public static int RemoveBooks(BookShopContext db)
        {
            var booksToUpdate = db.Books
                .Where(b => b.Copies < 4200)
                .ToList();


            db.RemoveRange(booksToUpdate);
            db.SaveChanges();
            return booksToUpdate.Count();
        }

        public static void IncreasePrices(BookShopContext db)
        {
            var booksToUpdate = db.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in booksToUpdate)
            {
                book.Price += 5;
            }
            db.SaveChanges();
        }

        public static string  GetMostRecentBooks(BookShopContext db)
        {
            var sb = new StringBuilder();

             db.Categories
                .Select(c => new
                {
                    Name = $"--{c.Name}",
                    Books = c.CategoryBooks.OrderByDescending(cb => cb.Book.ReleaseDate)
                    .Take(3)
                    .Select(cb => $"{cb.Book.Title} ({cb.Book.ReleaseDate.Value.Year})")
                    .ToList()
                })
                .OrderBy(c=>c.Name)
                .ToList()
                .ForEach(c => sb.AppendLine($"{c.Name}{Environment.NewLine}{string.Join(Environment.NewLine, c.Books)}"));

            return sb.ToString().TrimEnd('\n', '\r');
        }

        public static string GetTotalProfitByCategory(BookShopContext db)
        {
             var sb = new StringBuilder();
            db.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Profit = c.CategoryBooks.Sum(cb => cb.Book.Price * cb.Book.Copies)
                })
                .OrderByDescending(c => c.Profit)
                .ToList()
                .ForEach(c => sb.AppendLine($"{c.CategoryName} ${c.Profit:f2}"));
                
            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var sb = new StringBuilder();
            db.Books
                .GroupBy(b => b.Author)
                .Select(a=> new {
                    Name = $"{a.Key.FirstName} {a.Key.LastName}",
                    Copies= a.Sum(b => b.Copies)
                })
                .OrderByDescending(a=>a.Copies)
                .ToList()
                .ForEach(a => sb.AppendLine($"{a.Name} - {a.Copies}"));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static int CountBooks(BookShopContext db, int lengthCheck)
        {
           return db.Books
                .Where(b => b.Title.Length>lengthCheck)
                .Count();
        }

        public static string GetBooksByAuthor(BookShopContext db, string input)
        {
            var sb = new StringBuilder();
            db.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new {
                    Title = b.Title,
                    Author = string.Concat(b.Author.FirstName, " ", b.Author.LastName)
                })
                .ToList()
                .ForEach(b => sb.AppendLine($"{b.Title} ({b.Author})"));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string GetBookTitlesContaining(BookShopContext db, string input)
        {
            var sb = new StringBuilder();
            db.Books
                .Where(b => b.Title.EndsWith(input))
                .Select(b =>b.Title )
                .OrderBy(b => b)
                .ToList()
                .ForEach(b => sb.AppendLine(b));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string GetAuthorNamesEndingIn(BookShopContext db, string input)
        {
            var sb = new StringBuilder();
            db.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => String.Concat(a.FirstName, " ", a.LastName))
                .OrderBy(a => a)
                .ToList()
                .ForEach(a => sb.AppendLine(a));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string GetBooksReleasedBefore(BookShopContext db, string dateAsString)
        {
            var limitDate = DateTime.ParseExact(dateAsString, "dd-MM-yyyy",CultureInfo.InvariantCulture);


            var sb = new StringBuilder();

            db.Books
                          .Where(b => DateTime.Compare(b.ReleaseDate.Value, limitDate) == -1)
                          .OrderByDescending(b => b.ReleaseDate)
                          .Select(b => new
                          {
                              Title = b.Title,
                              Type = b.EditionType.ToString(),
                              Price = b.Price
                          })
                          .ToList()
                          .ForEach(b => sb.AppendLine($"{b.Title} - {b.Type} - ${b.Price:f2}"));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string GetBooksByCategory(BookShopContext db, string input)
        {
            string[] categoriesAsStrings = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(c=>c.ToLower()).ToArray();

            var categories = db.Categories.Where(c => categoriesAsStrings.Contains(c.Name.ToLower())).Select(c=>c.CategoryId).ToList();

            var sb = new StringBuilder();

            db.Books
                          .Where(b=>b.BookCategories.Any(bc=>categories.Contains(bc.CategoryId)))
                          .OrderBy(b => b.Title)
                          .Select(b => b.Title)
                          .ToList()
                          .ForEach(bt => sb.AppendLine(bt));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string GetBooksNotRealeasedIn(BookShopContext db, int year)
        {
            var sb = new StringBuilder();

            db.Books
                          .Where(b => !b.ReleaseDate.Value.Year.Equals(year))
                          .OrderBy(b => b.BookId)
                          .Select(b => b.Title)
                          .ToList()
                          .ForEach(bt => sb.AppendLine(bt));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string GetBooksByPrice(BookShopContext db)
        {
            var sb = new StringBuilder();

            db.Books
                          .Where(b => b.Price>40)
                          .OrderByDescending(b => b.Price)
                          .Select(b=>new {
                              Title = b.Title,
                              Price=b.Price
                          })
                          .ToList()
                          .ForEach(b => sb.AppendLine($"{b.Title} - ${b.Price:f2}"));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string GetGoldenBooks(BookShopContext db)
        {
            var sb = new StringBuilder();

            db.Books
                          .Where(b => b.Copies < 5000 && b.EditionType.ToString().Equals("Gold",StringComparison.OrdinalIgnoreCase))
                          .OrderBy(b => b.BookId)
                          .Select(b => b.Title)
                          .ToList()
                          .ForEach(bt => sb.AppendLine(bt));

            return sb.ToString().TrimEnd('\r', '\n');
        }

        public static string GetBooksByAgeRestriction(BookShopContext db, string command)
        {
            bool succeed = Enum.TryParse<AgeRestriction>(command, true, out AgeRestriction commandAsString);

            var sb = new StringBuilder();

            if (succeed)
            {
                db.Books
                              .Where(b => b.AgeRestriction == commandAsString)
                              .OrderBy(t => t.Title)
                              .Select(b => b.Title)
                              .ToList()
                              .ForEach(bt => sb.AppendLine(bt));
            }

            return sb.ToString();
        }


    }

}

