using DAL.Models;
using System.Collections.Generic;

namespace UnitOfWorkInMVC.Domain
{
    public class Basket
    {
        public Basket()
        {
            Products = new List<Product>();
        }

        public List<Product> Products { get; }

        public void Add(Product product) =>
            Products.Add(product);

        public void Remove(Product product) =>
            Products.Remove(product);
    }
}