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
using DAC.Dtos;

namespace DAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : WebApiController
    {
        private readonly ShopContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;
        public OrdersController(ShopContext context, IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut]
        [Route("orderItem")]
        public ActionResult<bool> AddOrders()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var cart = _unitOfWork.Carts.GetCartBy(userId);

            var product = _context.Carts
                .Include(cart => cart.Products)
                .Include(roomType => roomType.Id)
                .Include(roomType => roomType.User)
                .Include(roomType => roomType.CreatedAt).Where(rt => rt.User.Id == userId);

            var user = _unitOfWork.Users.GetUserById((Guid)userId);

            Order newOrder = new Order()
            {
                OrderStatus = Order.EOrderStatus.Processing,
                User = user,
                Products = cart.Products
            };
            _unitOfWork.Orders.Insert(newOrder);

            cart.Products.Clear();

            _context.Entry(cart).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();

        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
