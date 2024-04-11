using ASPNetCore_WebAPI_BookStore_Website.Servises.Repository;
using ASPNetCore_WebAPI_BookStore_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository _repository;

        public BookController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(string search, string sortBy, int page = 1)
        {
            try
            {
                var result = _repository.GetAllBook(search, sortBy, page);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _repository.GetByIdBook(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(BookVM book)
        {
            try
            {
                return Ok(_repository.AddBook(book));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit(int id, BookVM book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }
            try
            {
                _repository.UpdateBook(book);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteBook(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
