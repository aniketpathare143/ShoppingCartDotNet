﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        //public Guid PlaceSingleOrder(OrderDTO order)
        //{
        //    try
        //    {
        //        var orderRecord = mapper.Map<Order>(order);
        //        var insertedOrder = shoppingDbContext.Orders.Add(orderRecord);
        //        orderRecord.OrderId = insertedOrder.Property(e => e.OrderId).CurrentValue;

        //        //Find the product and its qty and update the product with reminaing qty
        //        var product = shoppingDbContext.Products.Where(o => o.ProductId == order.ProductId).FirstOrDefault();

        //        if (product != null)
        //        {
        //            int availableQty = product.AvailableQuantity;
        //            int remainingQty = availableQty - order.PlacedQuantity;
        //            product.AvailableQuantity = remainingQty;
        //        }

        //        // shoppingDbContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        //        shoppingDbContext.SaveChanges();
        //        return orderRecord.ProductId;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public bool RemoveOrder(Guid orderGuid)
        {
            try
            {
                var orderRecord = shoppingDbContext.Orders.Where(c => c.OrderId == orderGuid).FirstOrDefault();
                if (orderRecord != null)
                {
                    var placedOrders = shoppingDbContext.PlacedOrders.Where(p => p.OrderId == orderGuid).ToList();
                    if (placedOrders != null)
                    {
                        shoppingDbContext.PlacedOrders.RemoveRange(placedOrders);
                    }

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

        public Guid PlaceOrder(OrderDTO order)
        {
            try
            {               
                OrderDTO newOrder = new OrderDTO()
                {
                    UserId = order.UserId,
                    ProductPlacedCount = order.PlacedOrders.Count,
                };
                var orderRecord = mapper.Map<Order>(newOrder);
                var insertedOrder = shoppingDbContext.Orders.Add(orderRecord);
                shoppingDbContext.SaveChanges();
                var insertedOrderId = insertedOrder.Entity.OrderId;

                double priceTotal=0;
                foreach (var placedOrder in order.PlacedOrders)
                {
                    var product = shoppingDbContext.Products.Where(p => p.ProductId == placedOrder.ProductId).FirstOrDefault();
                    if(product != null)
                    {
                        double productPrice = Convert.ToDouble(product.Price);
                        priceTotal += placedOrder.PlacedQuantity * productPrice;

                        //update product quantity
                        product.AvailableQuantity = product.AvailableQuantity - placedOrder.PlacedQuantity;
                        shoppingDbContext.Products.Update(product);
                    }
                    
                    var placedOrderRecord = mapper.Map<PlacedOrder>(placedOrder);
                    placedOrderRecord.OrderId = insertedOrderId ?? Guid.Empty;
                    var insertedPlacedOrder = shoppingDbContext.PlacedOrders.Add(placedOrderRecord);                    
                }

                // Update Order's PriceTotal
                var orderToUpdate = shoppingDbContext.Orders.Find(insertedOrderId);
                if (orderToUpdate != null)
                {
                    orderToUpdate.PriceTotal = priceTotal;
                    shoppingDbContext.Orders.Update(orderToUpdate);
                }

                shoppingDbContext.SaveChanges();
                return insertedOrderId ?? Guid.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
