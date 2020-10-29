using DAL.Models;
using System.Collections.Generic;

namespace TransactionManager.Commands
{
    public class AddOrderItemsCommand : IAmCommand
    {
        public AddOrderItemsCommand(List<OrderItem> orderItems)
        {
            OrderItems = orderItems;
        }

        public List<OrderItem> OrderItems { get; }
    }
}
