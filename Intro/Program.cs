using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            AddLogging();
            Task.Run(async () => await AddBooksAsync()).Wait();
        }

        public static async Task CreateTheDatabaseAsync()
        {
            using (var context = new BooksContext())
            {
                bool created = await context.Database.EnsureCreatedAsync();
                string creationInfo = created ? "created" : "exists";
                Console.WriteLine($"database {creationInfo}");
            }
        }

        public static async Task DeleteDatabaseAsync()
        {
            using (var context = new BooksContext())
            {
                bool deleted = await context.Database.EnsureDeletedAsync();
                string deletingInfo = deleted ? "deleted" : "not deleted";
                Console.WriteLine($"database {deletingInfo}");
            }
        }

        public static async Task AddBookAsync(string title, string publisher)
        {
            using (var context = new BooksContext())
            {
                Book book = new Book()
                {
                    Title = title,
                    Publisher = publisher
                };
                await context.Books.AddAsync(book);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} record added");
            }
            Console.WriteLine();
        }

        public static async Task AddBooksAsync()
        {
            using (var context = new BooksContext()) {
                var b1 = new Book
                {
                    Title = "Professional C# 6 and .NET Core 1.0",
                    Publisher = "Wrox Press"
                };
                var b2 = new Book
                {
                    Title = "Professional C# 5 and .NET 4.5.1",
                    Publisher = "Wrox Press"
                };
                var b3 = new Book
                {
                    Title = "JavaScript for Kids",
                    Publisher = "Wrox Press"
                };
                var b4 = new Book
                {
                    Title = "Web Design with HTML and CSS",
                    Publisher = "For Dummies"
                };
                await context.AddRangeAsync(b1, b2, b3, b4);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }

        public static async Task ReadBooksAsync()
        {
            using (var context = new BooksContext())
            {
                List<Book> books = await context.Books.ToListAsync();
                foreach (Book book in books)
                {
                    Console.WriteLine($"{book.Title} {book.Publisher}");
                }
            }
            Console.WriteLine();
        }

        public static async Task QueryBookAsync()
        {
            using (var context = new BooksContext())
            {
                var books = await (from book in context.Books
                                   where book.Publisher == "Wrox Press"
                                   select book).ToListAsync();
                foreach (Book book in books)
                {
                    Console.WriteLine($"{book.Title} {book.Publisher}");
                }
            }
            Console.WriteLine();
        }

        public static async Task UpdateBookAsync()
        {
            using (var context = new BooksContext())
            {
                int records = 0;
                Book book = await context.Books
                .Where(b => b.Title == "C#")
                .FirstOrDefaultAsync();
                if (book != null)
                {
                    book.Title = "Professional C# 7 and .NET Core 2.0";
                    records = await context.SaveChangesAsync();
                }
                Console.WriteLine($"{records} record updated");
            }
            Console.WriteLine();
        }

        public static async Task DeleteBooksAsync()
        {
            using (var context = new BooksContext())
            {
                var books = context.Books;
                context.Books.RemoveRange(books);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} records deleted");
            }
            Console.WriteLine();
        }

        public static void AddLogging()
        {
            using (var context = new BooksContext())
            {
                IServiceProvider provider = context.GetInfrastructure<IServiceProvider>();
                ILoggerFactory loggerFactory = provider.GetService<ILoggerFactory>();
                loggerFactory.AddConsole(LogLevel.Information);
            }
        }
    }
}
