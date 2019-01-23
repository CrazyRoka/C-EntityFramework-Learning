using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UsingDependencyInjection
{
    public class BooksService
    {
        private readonly BooksContext _booksContext;
        public BooksService(BooksContext context) => _booksContext = context;

        public async Task AddBooksAsync()
        {
            var book1 = new Book()
            {
                Title = "Test 1",
                Publisher = "Roka"
            };
            var book2 = new Book()
            {
                Title = "Test 2",
                Publisher = "Roka"
            };
            var book3 = new Book()
            {
                Title = "Test 3",
                Publisher = "Roka"
            };
            var book4 = new Book()
            {
                Title = "Test 4",
                Publisher = "Toch"
            };
            var book5 = new Book()
            {
                Title = "Test 5",
                Publisher = "Programmer"
            };

            _booksContext.AddRange(book1, book2, book3, book4, book5);
            int records = await _booksContext.SaveChangesAsync();
            Console.WriteLine($"{records} records added");
        }

        public async Task ReadBooksAsync()
        {
            List<Book> books = await _booksContext.Books.ToListAsync();
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title, -40} {book.Publisher, -20}");
            }
        }
    }
}
