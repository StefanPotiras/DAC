using DAC.Entities;
using DAC.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Dtos
{

    public interface IAddressRepository : IRepositoryBase<UserAddres>
    {
        //UserAddres GetUserByUsername(string name);
        UserAddres GetUserById(Guid? ID);

    }

    public class UserAddressRepository : RepositoryBase<UserAddres>, IAddressRepository
    {

        public UserAddressRepository(ShopContext context) : base(context) { }

        public UserAddres GetUserById(Guid? ID)
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

        /*public UserAddres GetUserByUsername(string username)
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
        }*/


    }
}
