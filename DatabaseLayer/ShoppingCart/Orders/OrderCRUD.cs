using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DTOs;
using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Orders
{
    public class OrderCRUD
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;

        public OrderCRUD(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        public Guid PlaceSingleOrder(OrderDTO order)
        {
            try
            {
                var orderRecord = mapper.Map<Order>(order);
                var insertedOrder = shoppingDbContext.Orders.Add(orderRecord);
                orderRecord.OrderId = insertedOrder.Property(e => e.OrderId).CurrentValue;

                //Find the product and its qty and update the product with reminaing qty
                var product = shoppingDbContext.Products.Where(o => o.ProductId == order.ProductId).FirstOrDefault();

                if (product != null)
                {
                    int availableQty = product.AvailableQuantity;
                    int remainingQty = availableQty - order.PlacedQuantity;
                    product.AvailableQuantity = remainingQty;
                }

                // shoppingDbContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                shoppingDbContext.SaveChanges();
                return orderRecord.ProductId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveOrder(Guid orderGuid)
        {
            try
            {
                var orderRecord = shoppingDbContext.Orders.Where(c => c.OrderId == orderGuid).FirstOrDefault();
                if (orderRecord != null)
                {
                    shoppingDbContext.Orders.Remove(orderRecord);
                    shoppingDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PlaceOrder(List<OrderDTO> listOrders)
        {
            try
            {
                foreach (var order in listOrders)
                {
                    var orderRecord = mapper.Map<Order>(order);
                    var insertedOrder = shoppingDbContext.Orders.Add(orderRecord);
                    orderRecord.OrderId = insertedOrder.Property(e => e.OrderId).CurrentValue;

                    //Find the product and its qty and update the product with reminaing qty
                    var product = shoppingDbContext.Products.Where(o => o.ProductId == order.ProductId).FirstOrDefault();

                    if (product != null)
                    {
                        int availableQty = product.AvailableQuantity;
                        int remainingQty = availableQty - order.PlacedQuantity;
                        product.AvailableQuantity = remainingQty;
                    }

                    shoppingDbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
