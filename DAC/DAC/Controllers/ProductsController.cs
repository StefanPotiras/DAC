using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAC;
using DAC.Entities;
using DAC.Dtos;
using DAC.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace DAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : WebApiController
    {
        private readonly ShopContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;
        public ProductsController(ShopContext context, IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }

       
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut]
        [Route("addProduct")]
        [Authorize(Roles = "User")]
        public ActionResult<bool> AddProducts([FromBody] ProductAdd product)
        {
            var productFull = _unitOfWork.Products.GetProductBy(Guid.Parse(product.Product));
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var user = _unitOfWork.Users.GetUserById((Guid)userId);
            var cart = _unitOfWork.Carts.GetCartBy(user.Id);
            cart.Products.Add(productFull);

            _context.Entry(cart).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();

        }

        // PUT: api/Products/5      
        //Admin
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduct(Guid id, ProductsDto product)
        {
            var productDb = _unitOfWork.Products.GetProductBy(id);
            productDb.Name = product.Name;
            product.Description = product.Description;
            product.Price = product.Price;
            product.Description = product.Description;
            _unitOfWork.Products.Update(productDb);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return Ok(Tuple.Create(saveResult, productDb));
        }

        // POST: api/Products
        //Admin
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody]ProductsDto product)
        {
            if (product == null)
            {
                BadRequest(error: "Request must not be empty!");
            }

            var Product = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                NumberOfItems = product.NumberOfItems,

            };
            _unitOfWork.Products.Insert(Product);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return Ok(Tuple.Create(saveResult,product));
        }

        // DELETE: api/Products/5
        //Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Products.Delete(product);

            var saveResult = await _unitOfWork.SaveChangesAsync();
            return Ok(saveResult);
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
