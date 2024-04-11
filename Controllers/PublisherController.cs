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
    public class PublisherController : ControllerBase
    {
        private readonly IRepository _repository;

        public PublisherController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_repository.GetAllPublisher());
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
                var data = _repository.GetByIdPublisher(id);
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
        public IActionResult Create(PublisherVM publisher)
        {
            try
            {
                return Ok(_repository.AddPublisher(publisher));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit(int id, PublisherVM publisher)
        {
            if (id != publisher.PublisherId)
            {
                return NotFound();
            }
            try
            {
                _repository.UpdatePublisher(publisher);
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
                _repository.DeletePublisher(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
