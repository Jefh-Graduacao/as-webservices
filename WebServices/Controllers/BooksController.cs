using Microsoft.AspNetCore.Mvc;
using WebServices.Models;
using WebServices.Services;

namespace WebServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("{isbn}")]
        public ActionResult<Book> GetBookByIsbn(long isbn)
            => _booksService.GetBookByIsbn(isbn) is { } book
                ? Ok(book)
                : NotFound();

        [HttpPost]
        public IActionResult RegisterNewBook(Book book)
        {
            _booksService.RegisterNewBook(book);
            return NoContent();
        }

        [HttpGet("{isbn}/author")]
        public ActionResult<string> GetAuthorByIsbn(long isbn)
            => _booksService.GetAuthorByIsbn(isbn) is { Length: > 0 } author
                ? Ok(author)
                : NotFound();

    }
}