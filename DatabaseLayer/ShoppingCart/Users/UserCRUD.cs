using AutoMapper;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DTOs;

namespace ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Users
{
    public class UserCRUD
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;       

        public UserCRUD(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        public List<UserGetOrderDTO> GetAllOrdersForUser(Guid userGuid)
        {
            try
            {
                UserGetOrderDTO userGetOrderDTO;
                List<UserGetOrderDTO> listOrders = new List<UserGetOrderDTO>();

                var userRecord = shoppingDbContext.Users.Where(c => c.UserId == userGuid).FirstOrDefault();
                if (userRecord != null)
                {
                    var userOrders = shoppingDbContext.Orders.Where(c => c.UserId == userGuid).ToList();
                    if (userOrders.Count > 0)
                    {
                        foreach (var order in userOrders)
                        {
                            userGetOrderDTO = new UserGetOrderDTO();

                            userGetOrderDTO.Name = order.User?.FirstName + " " + order.User?.LastName;
                            userGetOrderDTO.PlacedQuantity = order.PlacedQuantity;
                            userGetOrderDTO.PlacedAt = order.PlacedAt;

                            //For product name and categories name we need to search in Categories and Product table
                            var product = shoppingDbContext.Products.Where(p => p.ProductId == order.ProductId).FirstOrDefault();
                            if (product != null)
                            {
                                userGetOrderDTO.ProductName = product.ProductName;

                                var category = shoppingDbContext.Categories.Where(c => c.CategoryId == product.CategoryId).FirstOrDefault();
                                if (category != null)
                                {
                                    userGetOrderDTO.CategoryName = category.CategoryName;
                                }
                            }
                            listOrders.Add(userGetOrderDTO);
                        }
                    }
                }

                return listOrders;
            }
            catch (Exception)
            {
                throw;
            }
        }
               
    }
}
