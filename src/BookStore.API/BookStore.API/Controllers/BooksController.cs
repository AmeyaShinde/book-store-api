using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] BookModel bookModel)
        {
            var bookId = await _bookRepository.AddBookAsync(bookModel);

            return CreatedAtAction(nameof(GetBookById), new { id = bookId, controller = "Books" }, bookId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookModel bookModel)
        {
            await _bookRepository.UpdateBookAsync(id, bookModel);

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] JsonPatchDocument bookModel)
        {
            await _bookRepository.UpdateBookPatchAsync(id, bookModel);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromRoute] int id)
        {
            await _bookRepository.DeleteBookAsync(id);

            return Ok();
        }
    }
}