using System.Threading.Tasks;
using TransactionManager.Commands;

namespace TransactionManager
{
    public interface IAmCommandsManager
    {
        void AddCommand(IAmCommand command);
        Task ExecuteCommands();
    }
}