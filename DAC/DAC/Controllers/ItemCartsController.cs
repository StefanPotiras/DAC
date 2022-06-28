using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAC.Models;

namespace DAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCartsController : ControllerBase
    {
        private readonly Online_Shop2Context _context;

        public ItemCartsController(Online_Shop2Context context)
        {
            _context = context;
        }

        // GET: api/ItemCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCart>>> GetItemCarts()
        {
            return await _context.ItemCarts.ToListAsync();
        }

        // GET: api/ItemCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCart>> GetItemCart(int id)
        {
            var itemCart = await _context.ItemCarts.FindAsync(id);

            if (itemCart == null)
            {
                return NotFound();
            }

            return itemCart;
        }

        // PUT: api/ItemCarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCart(int id, ItemCart itemCart)
        {
            if (id != itemCart.IdCartItem)
            {
                return BadRequest();
            }

            _context.Entry(itemCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItemCarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCart>> PostItemCart(ItemCart itemCart)
        {
            _context.ItemCarts.Add(itemCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCart", new { id = itemCart.IdCartItem }, itemCart);
        }

        // DELETE: api/ItemCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCart(int id)
        {
            var itemCart = await _context.ItemCarts.FindAsync(id);
            if (itemCart == null)
            {
                return NotFound();
            }

            _context.ItemCarts.Remove(itemCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemCartExists(int id)
        {
            return _context.ItemCarts.Any(e => e.IdCartItem == id);
        }
    }
}
