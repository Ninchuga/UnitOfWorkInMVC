using DAL.Models;
using Ninject;
using TransactionManager.CommandHandlers;
using TransactionManager.Commands;

namespace TransactionManager.Providers
{
    public class CommandHandlersProvider
    {
        public dynamic CreateCommandHandlerInstance(IAmCommand command, IKernel kernel, IAmDbContext dbContext)
        {
            var context = new Ninject.Parameters.ConstructorArgument("dbContext", dbContext);
            var type = typeof(IAmCommandHandler<>).MakeGenericType(command.GetType());

            dynamic handler = kernel.Get(type, context);

            return handler;
        }
    }
}
