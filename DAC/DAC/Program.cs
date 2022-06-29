using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
           /* var options = new DbContextOptionsBuilder<ShopContext>()
              .UseSqlServer(@"Data Source=LAPTOP-RBJPENMM;Initial Catalog=Shop;Integrated Security=True;Connect Timeout=30;")
              .Options;
            using (var db = new ShopContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }*/
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
