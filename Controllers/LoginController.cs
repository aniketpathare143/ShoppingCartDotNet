using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DatabaseLayer.Login;
using ShoppingCartAPIs.DTOs;

namespace ShoppingCartAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;

        public LoginController(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }


        [HttpPost("register")]
        public ActionResult Register(UserDTO user)
        {
            Registration registration = new Registration(shoppingDbContext, mapper);
            var userKey = registration.AddUser(user);
            return Ok(userKey);
        }

        [HttpGet]
        [Route("{email}/{password}")]
        public ActionResult Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Registration registration = new Registration(shoppingDbContext, mapper);
                var authenticatedUser = registration.AuthenticateUser(email, password);
                return Ok(authenticatedUser);
            }
            return BadRequest();
        }
    }
}
