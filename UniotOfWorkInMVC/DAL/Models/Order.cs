using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}