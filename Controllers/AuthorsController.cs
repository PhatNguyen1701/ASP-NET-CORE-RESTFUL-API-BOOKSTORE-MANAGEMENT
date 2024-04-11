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
    public class AuthorsController : ControllerBase
    {
        private readonly IRepository _repository;

        public AuthorsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(string search, string sortBy, int page = 1)
        {
            try
            {
                return Ok(_repository.GetAllAuthor(search, sortBy, page));
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
                var data = _repository.GetByIdAuthor(id);
                if(data != null)
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
        public IActionResult Create(AuthorVM author)
        {
            try
            {
                return Ok(_repository.AddAuthor(author));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit(int id, AuthorVM author)
        {
            if(id != author.AuthorId)
            {
                return NotFound();
            }
            try
            {
                _repository.UpdateAuthor(author);
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
                _repository.DeleteAuthor(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
