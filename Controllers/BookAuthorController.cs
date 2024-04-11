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
    public class BookAuthorController : ControllerBase
    {
        private readonly IRepository _repository;

        public BookAuthorController(IRepository repositoty)
        {
            _repository = repositoty;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_repository.GetAllBA());
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
                var data = _repository.GetByIdBA(id);
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
        public IActionResult Create(BookAuthorVM bookAuthor)
        {
            try
            {
                return Ok(_repository.AddBA(bookAuthor));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit(int id, BookAuthorVM bookAuthor)
        {
            if (id != bookAuthor.BookAuthorsId)
            {
                return NotFound();
            }
            try
            {
                _repository.UpdateBA(bookAuthor);
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
                _repository.DeleteBA(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
