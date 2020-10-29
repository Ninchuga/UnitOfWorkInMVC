using System;

namespace DAL.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Seller { get; set; }
        public Guid OrderId { get; set; }
    }
}