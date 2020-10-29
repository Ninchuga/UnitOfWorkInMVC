using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitOfWorkInMVC.Extensions
{
    public static class OrderExtensions
    {
        public static OrderItem ToOrderItem(this Product product, Guid orderId)
        {
            return new OrderItem
            {
                Id = Guid.NewGuid(),
                Description = product.Description,
                Number = "11111",
                Price = product.Price,
                Quantity = product.Quantity,
                Seller = product.Seller,
                OrderId = orderId
            };
        }

        public static List<OrderItem> ToOrderItems(this IEnumerable<Product> products, Guid orderId)
        {
            return products.Select(product => product.ToOrderItem(orderId)).ToList();
        }
    }
}