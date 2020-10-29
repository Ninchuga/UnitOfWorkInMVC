using DAL.Models;
using System.Data.Entity;
using TransactionManager.Commands;

namespace TransactionManager.CommandHandlers
{
    public class AddOrderCommandHandler : IAmCommandHandler<AddOrderCommand>
    {
        private readonly IAmDbContext _dbContext;
        private readonly DbSet<Order> _orders;

        public AddOrderCommandHandler(IAmDbContext dbContext)
        {
            _dbContext = dbContext;
            _orders = _dbContext.Set<Order>();
        }

        public void Handle(AddOrderCommand command) =>
            _orders.Add(command.Order);
    }
}
