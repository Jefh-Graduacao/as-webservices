using System.Collections.Generic;
using System.Linq;
using WebServices.Models;
using WebServices.SoapServices;

namespace WebServices.Services
{
    public sealed class BooksService : IBooksService
    {
        private static readonly List<Book> _books = new()
        {
            new Book(9780545069670, "Harry Potter and the Sorcerer's Stone", "J. K. Rowling"),
            new Book(9780261103207, "The Lord of the Rings", "J. R. R. Tolkien")
        };

        public Book GetBookByIsbn(long isbn)
            => _books.FirstOrDefault(books => books.Isbn == isbn);

        public void RegisterNewBook(Book book)
        {
            _books.Add(book);
        }

        public string GetAuthorByIsbn(long isbn)
        {
            var book = _books.FirstOrDefault(books => books.Isbn == isbn);
            return book?.Author;
        }
    }
}