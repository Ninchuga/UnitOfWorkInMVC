namespace DAL.Migrations
{
    using DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Models.UnitOfWorkInMVCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DAL.Models.UnitOfWorkInMVCContext context)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "Product 1",
                    Name = "Wine glass",
                    Price = 25,
                    Quantity = 6,
                    Seller = "SomeSeller1"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "Product 2",
                    Name = "Toster",
                    Price = 55,
                    Quantity = 1,
                    Seller = "SomeSeller2"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "Product 3",
                    Name = "Ball for football",
                    Price = 18.5,
                    Quantity = 1,
                    Seller = "SomeSeller3"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "Product 4",
                    Name = "Watch",
                    Price = 110,
                    Quantity = 1,
                    Seller = "SomeSeller4"
                }
            };
            products.ForEach(s => context.Products.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }
    }
}
