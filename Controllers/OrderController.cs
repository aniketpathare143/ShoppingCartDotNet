using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Orders;
using ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Products;
using ShoppingCartAPIs.DTOs;

namespace ShoppingCartAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;

        public OrderController(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        //[HttpPost]
        //public ActionResult PlaceOrder(OrderDTO order)
        //{
        //    OrderCRUD orderCRUD = new OrderCRUD(shoppingDbContext, mapper);
        //    var orderKey = orderCRUD.PlaceSingleOrder(order);
        //    return Ok(orderKey);
        //}

        [HttpPost]
        public ActionResult PlaceOrder(OrderDTO order)
        {
            OrderCRUD orderCRUD = new OrderCRUD(shoppingDbContext, mapper);
            var orderKey = orderCRUD.PlaceOrder(order);
            return Ok(orderKey);            
        }


        [HttpDelete]
        [Route("{orderGuid}")]
        public ActionResult DeleteOrder(Guid orderGuid)
        {
            OrderCRUD orderCRUD = new OrderCRUD(shoppingDbContext, mapper);
            bool isDeleted = orderCRUD.RemoveOrder(orderGuid);
            if (isDeleted)
                return Ok("Order deleted successfully");
            else
                return NotFound();
        }
    }
}
