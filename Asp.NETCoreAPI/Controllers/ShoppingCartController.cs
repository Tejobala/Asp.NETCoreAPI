using Asp.NETCoreAPI.Models;
using Asp.NETCoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Asp.NETCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartServices _services;

        public ShoppingCartController(IShoppingCartServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _services.GetAllItems();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var item = _services.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ShoppingItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _services.Add(value);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpDelete("{}")]
        public IActionResult Delete(Guid id)
        {
            var existingItem = _services.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _services.Remove(id);
            return NoContent();
        }
    }
}
