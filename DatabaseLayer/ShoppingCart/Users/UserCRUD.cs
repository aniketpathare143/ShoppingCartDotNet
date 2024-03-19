using AutoMapper;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DTOs;
using ShoppingCartAPIs.Models;

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

        public UserGetOrderDTO GetAllOrdersForUser(Guid userGuid)
        {
            try
            {
                UserGetOrderDTO userGetOrderDTO;
                List<RetrievePlacedOrderDTO> listOrders;
                List<RetrieveOrderDTO> listRetrieveOrders;

                userGetOrderDTO = new UserGetOrderDTO();
                var userRecord = shoppingDbContext.Users.Where(c => c.UserId == userGuid).FirstOrDefault();
                if (userRecord != null)
                {
                    userGetOrderDTO.UserName = userRecord.FirstName + " " + userRecord.LastName;
                    userGetOrderDTO.UserId = userRecord.UserId;
                    var userOrders = shoppingDbContext.Orders.Where(c => c.UserId == userGuid).ToList();
                    if (userOrders.Count > 0)
                    {
                        //Iterate Order table here
                        listRetrieveOrders = new List<RetrieveOrderDTO>();
                        foreach (var order in userOrders)
                        {
                            RetrieveOrderDTO retrieveOrder = new RetrieveOrderDTO();
                            retrieveOrder.OrderId = order.OrderId ?? Guid.Empty;
                            retrieveOrder.ProductPlacedCount = order.ProductPlacedCount;
                            retrieveOrder.PlacedAt = order.PlacedAt;

                            //Iterate PlaceOrder table here
                            listOrders = new List<RetrievePlacedOrderDTO>();
                            var userPlacedOrders = shoppingDbContext.PlacedOrders.Where(c => c.OrderId == order.OrderId).ToList();
                            foreach (var placedOrder in userPlacedOrders)
                            {
                                RetrievePlacedOrderDTO retrievePlacedOrderDTO = new RetrievePlacedOrderDTO();
                                //For product name and categories name we need to search in Categories and Product table
                                var product = shoppingDbContext.Products.Where(p => p.ProductId == placedOrder.ProductId).FirstOrDefault();
                                if (product != null)
                                {
                                    retrievePlacedOrderDTO.PlacedOrderId = placedOrder.PlacedOrderId;
                                    retrievePlacedOrderDTO.ProductName = product.ProductName;
                                    retrievePlacedOrderDTO.PlacedQuantity= placedOrder.PlacedQuantity;
                                    var category = shoppingDbContext.Categories.Where(c => c.CategoryId == placedOrder.CategoryId).FirstOrDefault();
                                    if (category != null)
                                    {
                                        retrievePlacedOrderDTO.CategoryName = category.CategoryName;
                                    }
                                }
                                listOrders.Add(retrievePlacedOrderDTO);
                            }
                            retrieveOrder.RetrievePlacedOrders = listOrders;
                            listRetrieveOrders.Add(retrieveOrder);
                        }
                        userGetOrderDTO.RetrieveOrders = listRetrieveOrders;
                    }
                }

                return userGetOrderDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
