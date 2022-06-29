using DAC.Dtos;
using DAC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }
        ICartRepository Carts { get; }
        Task<bool> SaveChangesAsync();

    }
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DbContext _efDbContext;
        public IUserRepository Users { get; set; }

        public IProductRepository Products { get; set; }

        public ICartRepository Carts { get; set; }

        public UnitOfWork
        (
            ShopContext efDbContext,
            IUserRepository users,
            IProductRepository productRepository,
            ICartRepository cartRepository
                )
        {
            _efDbContext = efDbContext;
            Users = users;
            Products = productRepository;
            Carts = cartRepository;


        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var savedChanges = await _efDbContext.SaveChangesAsync();
                return savedChanges > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
