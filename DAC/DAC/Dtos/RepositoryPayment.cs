using DAC.Entities;
using DAC.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Dtos
{

        public interface IPaymentRepository : IRepositoryBase<UserPayment>
        {
            //UserAddres GetUserByUsername(string name);
            UserPayment GetUserById(Guid? ID);

        }

        public class PaymentRepository : RepositoryBase<UserPayment>, IPaymentRepository
        {

            public PaymentRepository(ShopContext context) : base(context) { }

            public UserPayment GetUserById(Guid? ID)
            {
                //SELECT TOP(1) * from Users as u
                var result = GetRecords()

                    // FULL JOIN Notifications as n on n.UserId = u.Id


                    // WHERE u.Email = @email 
                    // IQueryable pana aici -> rezultatul nu e concret             
                    .Where(u => u.Id == ID)

                    .FirstOrDefault();
                // -> rezultat concret
                return result;
            }

            /* public UserAddres GetUserByUsername(string username)
             {
                 //SELECT TOP(1) * from Users as u
                 var result = GetRecords()

                     // FULL JOIN Notifications as n on n.UserId = u.Id


                     // WHERE u.Email = @email 
                     // IQueryable pana aici -> rezultatul nu e concret
                     .Where(u => u.Username == username)

                     .FirstOrDefault();
                 // -> rezultat concret
                 return result;
             }
     */

        }
    }

