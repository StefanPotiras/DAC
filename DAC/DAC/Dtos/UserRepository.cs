using DAC.Entities;
using DAC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Dtos
{

    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByUsername(string name);
        User GetUserById(Guid ID);
      
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(ShopContext context) : base(context) { }

        public User GetUserById(Guid ID)
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

        public User GetUserByUsername(string username)
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

       
    }
}
