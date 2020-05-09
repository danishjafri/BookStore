using AutoMapper;
using BookStore.API.Contracts;
using BookStore.API.DTOs.Books;
using BookStore.Domain.Models;
using BookStore.Services.Generics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IGenericService<Book> _bookService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public BooksController(IGenericService<Book> bookService, ILoggerService loggerService, IMapper mapper)
        {
            _bookService = bookService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetBooks()
        {
            var booksWithPagination = _bookService.GetListWithPagination();
            return Ok(_mapper.Map<IReadOnlyList<BookDto>>(booksWithPagination.Items.ToList()));
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetBookWithAuthorDto>> GetBook(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null)
                return NotFound();

            return _mapper.Map<GetBookWithAuthorDto>(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Customer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if (id < 1 || id != bookDto.Id || bookDto == null)
                return BadRequest("Invalid book details provided.");

            if (!ModelState.IsValid)
                return BadRequest(new { Book = ModelState, Message = "Incomplete book details." });

            await _bookService.Update(_mapper.Map<Book>(bookDto));

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BookDto>> PostBook(BookCreationDto bookDto)
        {
            if (bookDto == null)
                return BadRequest("Invalid book details provided.");

            if (!ModelState.IsValid)
                return BadRequest(new { Book = ModelState, Message = "Incomplete book details." });

            var id = await _bookService.CreateAsync(_mapper.Map<Book>(bookDto));
            var book = await _bookService.GetById(id);

            return CreatedAtAction("GetBook", new { id = book.Id }, _mapper.Map<GetBookWithAuthorDto>(book));
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Customer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDto>> DeleteBook(int id)
        {
            await _bookService.Delete(id);
            return NoContent();
        }
    }

}