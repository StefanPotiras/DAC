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
using Microsoft.AspNetCore.Authorization;

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

        
        [HttpGet]
        [Authorize(Roles = "Amin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("allOrders")]
        public ActionResult<bool> GetOrderHist()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            var allOrders = _unitOfWork.Orders.GetOrderByUser(userId);
            if (allOrders.Count == 0) return BadRequest("No orders");
            return Ok(allOrders);
           
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
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
        [Authorize(Roles = "User")]
        public async Task<ActionResult<bool>> AddOrdersAsync()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var cart = _unitOfWork.Carts.GetCartBy(userId);
             if(cart.Products.Count==0)
                return BadRequest("Nu ai produse in cos");
            var user = _unitOfWork.Users.GetUserById((Guid)userId);

            Order newOrder = new Order()
            {
                OrderStatus = Order.EOrderStatus.Processing,
                User = user,            
            };
            _unitOfWork.Orders.Insert(newOrder);         
            var saveResult = await _unitOfWork.SaveChangesAsync();

            var order = _unitOfWork.Orders.GetOrderBy(newOrder.Id);

            foreach(var index in cart.Products)           
                order.Products.Add(index);

             cart.Products.Clear();
            _context.Entry(cart).State = EntityState.Modified;
            _context.SaveChanges();


            return Ok(saveResult);          
                  
        }

        // PUT: api/Orders/5     
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id,StatusDtos statusDtos )
        {
            if (statusDtos == null)
            {
                BadRequest(error: "Request must not be empty!");
            }

            var orderDb = _unitOfWork.Orders.GetOrderBy(id);
            orderDb.OrderStatus = statusDtos.OrderStatus;
           
            _unitOfWork.Orders.Update(orderDb);
            var saveResult = await _unitOfWork.SaveChangesAsync();
            if(saveResult==true)
            return Ok(Tuple.Create(saveResult, orderDb));
            else
                return Ok(saveResult);
        }

      
        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = _unitOfWork.Orders.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            _unitOfWork.Orders.Delete(order);

            var saveResult = await _unitOfWork.SaveChangesAsync();
            return Ok(saveResult);
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
