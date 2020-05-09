using AutoMapper;
using BookStore.API.Contracts;
using BookStore.API.DTOs.Authors;
using BookStore.Domain.Helpers;
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
    public class AuthorsController : ControllerBase
    {
        private readonly IGenericService<Author> _authorService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public AuthorsController(IGenericService<Author> authorService,ILoggerService loggerService,IMapper mapper)
        {
            _authorService = authorService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAuthors()
        {
            var authorsWithPagination = _authorService.GetListWithPagination();
            return Ok(_mapper.Map<IReadOnlyList<AuthorDto>>(authorsWithPagination.Items.ToList()));
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetAuthorWithBooks>> GetAuthor(int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
                return NotFound();

            return _mapper.Map<GetAuthorWithBooks>(author);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Customer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreationDto authorDto)
        {
            if (authorDto == null)
                return BadRequest("Invalid author details provided.");

            if (!ModelState.IsValid)
                return BadRequest(new { Author = ModelState, Message = "Incomplete author details." });

            var id = await _authorService.CreateAsync(_mapper.Map<Author>(authorDto));
            var author = await _authorService.GetById(id);
            return CreatedAtAction("GetAuthor", new { id = author.Id }, _mapper.Map<GetAuthorWithBooks>(author));
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Customer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthorDto>> DeleteAuthor(int id)
        {
            await _authorService.Delete(id);
            return NoContent();
        }
    }
}