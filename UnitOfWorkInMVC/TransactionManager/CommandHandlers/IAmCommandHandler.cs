using TransactionManager.Commands;

namespace TransactionManager.CommandHandlers
{
    public interface IAmCommandHandler<TCommand> where TCommand : IAmCommand
    {
        void Handle(TCommand command);
    }
}
