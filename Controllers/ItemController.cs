using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemManagement.Data;
using ItemManagement.Models;
using ItemManagement.ViewModels;

namespace ItemManagement.Controllers
{
    [ApiController]
    [Route("api")]
    public class ItemController : ControllerBase
    {
        [HttpGet]
        [Route("items")]
        public async Task<IActionResult> GetAsync
        (
            [FromServices] AppDbContext context
        )
        {
            var items = await context.Items.AsNoTracking().ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("item/{id}")]
        public async Task<IActionResult> GetByIdAsync
        (
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            var item = await context.Items.FirstOrDefaultAsync(x => x.Id == id) ;
            return item == null
                ? NotFound()
                : Ok(item);
        }

        [HttpPut]
        [Route("item/{id}")]

        public async Task<IActionResult> PutAsync
        (
            [FromServices] AppDbContext context,
            [FromBody] Item model,
            [FromRoute] int id
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = await context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            try
            {
                item.Description = model.Description;
                item.Quantity = model.Quantity;
                item.Price = model.Price;

                context.Items.Update(item);
                await context.SaveChangesAsync();
                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("item/{id}")]

        public async Task<IActionResult> DeleteAsync
        (
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = await context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            try
            {
                context.Items.Remove(item);
                await context.SaveChangesAsync();
                return Ok("Item deleted!");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("items")]
        public async Task<IActionResult> PostAsync
        (
            [FromServices] AppDbContext context,
            [FromBody] Item model
        )
        {
            var item = new Item
            {
                DateCreated = DateTime.Now,
                Description = model.Description,
                Quantity = model.Quantity,
                Price = model.Price,
            };
            try
            {
                await context.Items.AddAsync(item);
                await context.SaveChangesAsync();
                return Created($"api/item/{item.Id}", item);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

    }
}
