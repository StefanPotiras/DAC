using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAC;
using DAC.Entities;
using DAC.Repositories;
using Microsoft.AspNetCore.Authorization;
using DAC.Dtos;

namespace DAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : WebApiController
    {
        private readonly ShopContext _context;

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;

        public CartsController(ShopContext context, IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("cart")]
        public ActionResult<bool> GetOrderHist()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            var cart = _unitOfWork.Carts.GetCartById(userId);
            return Ok(cart);

        }


        [HttpPut]
        [Authorize(Roles = "User")]
        [Route("updateCart")]
         public async Task<IActionResult> UpdateCart(ProductAdd product)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            
            var cart = _unitOfWork.Carts.GetCartBy(userId);
            if (cart == null)
                return BadRequest("No cart");
            else
                if (cart.Products.Count == 0)
                return BadRequest("No product in cart");
            var productDb = _unitOfWork.Products.GetProductBy(Guid.Parse(product.Product));
            if (productDb == null)
                return BadRequest("Productul nu exista");

            cart.Products.Remove(productDb);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return Ok(saveResult);

          
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Cart>> GetCart(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

   
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCart(Guid id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
       
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(Guid id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
