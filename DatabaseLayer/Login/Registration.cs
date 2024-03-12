using AutoMapper;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DTOs;
using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DatabaseLayer.Login
{
    public class Registration
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;

        public Registration(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        public Guid AddUser(UserDTO user)
        {
            try
            {
                var newUser = mapper.Map<UserDTO, User>(user);
                var insertedUser = shoppingDbContext.Users.Add(newUser);
                newUser.UserId = insertedUser.Property(e => e.UserId).CurrentValue;
                shoppingDbContext.SaveChanges();
                return newUser.UserId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object AuthenticateUser(string email, string password)
        {
            try
            {
                var user = shoppingDbContext.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
                if (user != null)
                {
                    var authenticatedUser = mapper.Map<UserDTO>(user);
                    return authenticatedUser;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
