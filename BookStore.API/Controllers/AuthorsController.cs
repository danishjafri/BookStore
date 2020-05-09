using AutoMapper;
using BookStore.API.Contracts;
using BookStore.API.DTOs.Authors;
using BookStore.Domain.Models;
using BookStore.Services.Generics;
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
    public class AuthorsController : ControllerBase
    {
        private readonly IGenericService<Author> _authorService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public AuthorsController(
            IGenericService<Author> authorService,
            ILoggerService loggerService,
            IMapper mapper
            )
        {
            _authorService = authorService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAuthors()
        {
            var authorsWithPagination = _authorService.GetListWithPagination();
            return Ok(_mapper.Map<IReadOnlyList<AuthorDto>>(authorsWithPagination.Items.ToList()));
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
                return NotFound();

            return _mapper.Map<AuthorDto>(author);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (id < 1 || id != authorDto.Id || authorDto == null)
                return BadRequest("Invalid author details provided.");

            if (!ModelState.IsValid)
                return BadRequest(new { Author = ModelState, Message = "Incomplete author details." });

            await _authorService.Update(_mapper.Map<Author>(authorDto));

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreationDto authorDto)
        {
            if (authorDto == null)
                return BadRequest("Invalid author details provided.");

            if (!ModelState.IsValid)
                return BadRequest(new { Author = ModelState, Message = "Incomplete author details." });

            var author = await _authorService.CreateAsync(_mapper.Map<Author>(authorDto));
            return CreatedAtAction("GetAuthor", new { id = author.Id }, authorDto);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthorDto>> DeleteAuthor(int id)
        {
            try
            {
                await _authorService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                InternalError($"{ex.Message} - {ex.InnerException}");
                throw;
            }
        }

        private ObjectResult InternalError(string message)
        {
            _loggerService.LogError(message);
            return StatusCode(500, "Something went wrong, please contact the administrator.");
        }
    }
}