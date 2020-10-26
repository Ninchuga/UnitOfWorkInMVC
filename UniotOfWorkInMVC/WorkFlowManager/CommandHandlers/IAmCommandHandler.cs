using WorkFlowManager.Commands;

namespace WorkFlowManager.CommandHandlers
{
    public interface IAmCommandHandler<TCommand> where TCommand : IAmCommand
    {
        void Handler(TCommand command);
    }
}
