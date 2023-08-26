using book.Model;
using book.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository) {
            
            _bookRepository = bookRepository;
        }

        [HttpGet("GetAll")]

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("GetById")]

        public async Task<ActionResult<Book>> GetBooksForId(int id)
        {
            return await _bookRepository.Get(id);
        }

        [HttpPost("Create")]

        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            var newBook = await _bookRepository.create(book);
            return book;
        }

        [HttpDelete("Delete")]

        public async Task<ActionResult<Book>> Delete(int id)
        {
            var bookDelete = await _bookRepository.Get(id);

            if(bookDelete == null) {
                return NotFound();
            }
            else
            {
                await _bookRepository.Delete(bookDelete.id);

            }

            return NoContent();
        }

        private ActionResult<Book> NoContent()
        {
            throw new NotImplementedException();
        }

        private ActionResult<Book> NotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPut("Update")]

        public async Task<ActionResult> UpdateBooks(int id, [FromBody] Book book)
        {
            if(id == book.id)
            {
                await _bookRepository.Update(book);
            }
            return null;
        }
    }
}
