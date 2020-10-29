using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;
using TransactionManager;
using TransactionManager.Commands;
using UnitOfWorkInMVC.Domain;
using UnitOfWorkInMVC.Extensions;

namespace UnitOfWorkInMVC.Controllers
{
    public class OrderController : Controller
    {
        private static readonly Dictionary<string, Basket> _basketCache = new Dictionary<string, Basket>();
        private readonly IAmDbContext _dbContext;
        private readonly IAmCommandsManager _commandsManager;
        private const string DemoCacheKey = "MyUserName";

        public OrderController(IAmDbContext dbContext, IAmCommandsManager commandsManager)
        {
            _dbContext = dbContext;
            _commandsManager = commandsManager;
        }

        public ActionResult Index()
        {
            var products = _dbContext.Set<Product>().ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult UserBasket()
        {
            var userBasket = _basketCache.ContainsKey(DemoCacheKey) ? _basketCache[DemoCacheKey] as Basket : new Basket();

            return View(userBasket);
        }

        [HttpPost]
        public ActionResult AddToBasket(Product product)
        {
            Basket userBasket = _basketCache.ContainsKey(DemoCacheKey) ? _basketCache[DemoCacheKey] as Basket : null;
            if (userBasket == null)
            {
                userBasket = new Basket();
                userBasket.Add(product);
                _basketCache.Add(DemoCacheKey, userBasket);
            }
            else
            {
                userBasket.Add(product);
                _basketCache.Remove(DemoCacheKey);
                _basketCache.Add(DemoCacheKey, userBasket);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromBasket(Product product)
        {
            var userBasket = _basketCache[DemoCacheKey] as Basket;
            userBasket.Remove(product);

            _basketCache.Remove(DemoCacheKey);
            _basketCache.Add(DemoCacheKey, userBasket);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            var userBasket = _basketCache[DemoCacheKey] as Basket;

            Guid orderId = Guid.NewGuid();
            var orderItems = userBasket.Products.ToOrderItems(orderId);
            var orderItemsTotalPrice = orderItems.Select(item => item.Price).Sum();

            Random r = new Random();
            int num = r.Next();

            var order = new Order { Id = orderId, Number = num.ToString(), OrderItems = orderItems, TotalPrice = orderItemsTotalPrice };
            _commandsManager.AddCommand(new AddOrderCommand(order));
            _commandsManager.AddCommand(new AddOrderItemsCommand(orderItems));

            await _commandsManager.ExecuteCommands();

            return RedirectToAction("Index");
        }
    }
}