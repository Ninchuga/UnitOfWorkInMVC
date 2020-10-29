using DAL.Models;
using System.Data.Entity;
using TransactionManager.Commands;

namespace TransactionManager.CommandHandlers
{
    public class AddOrderItemCommandHandler : IAmCommandHandler<AddOrderItemsCommand>
    {
        private readonly IAmDbContext _dbContext;
        private readonly DbSet<OrderItem> _orderItems;

        public AddOrderItemCommandHandler(IAmDbContext dbContext)
        {
            _dbContext = dbContext;
            _orderItems = _dbContext.Set<OrderItem>();
        }

        public void Handle(AddOrderItemsCommand command) =>
            command.OrderItems.ForEach(order => _orderItems.Add(order));
    }
}
