using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Users;

namespace ShoppingCartAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;

        public UserController(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{userGuid}")]
        public ActionResult GetOrdersForUser(Guid userGuid)
        {

            UserCRUD userCRUD = new UserCRUD(shoppingDbContext, mapper);
            var listOrders = userCRUD.GetAllOrdersForUser(userGuid);
            return Ok(listOrders);           
        }
    }
}
