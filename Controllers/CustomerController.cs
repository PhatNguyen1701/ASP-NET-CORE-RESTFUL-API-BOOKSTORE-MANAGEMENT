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
    public class CustomerController : ControllerBase
    {
        private readonly IRepository _repository;

        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(string search, string sortBy, int page = 1)
        {
            try
            {
                return Ok(_repository.GetAllCustomer(search, sortBy, page));
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
                var data = _repository.GetByIdCustomer(id);
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
        public IActionResult Create(CustomersVM customer)
        {
            try
            {
                return Ok(_repository.AddCustomer(customer));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit(int id, CustomersVM customers)
        {
            if (id != customers.CustomerId)
            {
                return NotFound();
            }
            try
            {
                _repository.UpdateCustomer(customers);
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
                _repository.DeleteCustomer(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
