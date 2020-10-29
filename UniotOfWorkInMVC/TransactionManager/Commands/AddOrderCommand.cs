using DAL.Models;

namespace TransactionManager.Commands
{
    public class AddOrderCommand : IAmCommand
    {
        public AddOrderCommand(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
