using DAL.Models;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionManager.Commands;
using TransactionManager.Providers;

namespace TransactionManager
{
    public class CommandsManager : IAmCommandsManager
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CommandsManager));
        private readonly IAmDbContext _dbContext;
        private readonly CommandHandlersProvider _handlersProvider;
        private readonly IKernel _kernel;
        private readonly List<IAmCommand> _commands;

        public CommandsManager(IAmDbContext dbContext, IKernel kernel, CommandHandlersProvider handlersProvider)
        {
            _dbContext = dbContext;
            _kernel = kernel;
            _handlersProvider = handlersProvider;
            _commands = new List<IAmCommand>();
        }

        public void AddCommand(IAmCommand command) =>
            _commands.Add(command);

        public async Task ExecuteCommands()
        {
            try
            {
                foreach (var command in _commands)
                {
                    var commandHandler = _handlersProvider.CreateCommandHandlerInstance(command, _kernel, _dbContext);
                    commandHandler?.Handle((dynamic)command);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error($"Error while executing commands from {nameof(CommandsManager)}: {ex}");

                // Update original values from the database with the new values (Client wins approach)
                TryReloadEntityValues(ex.Entries);

                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.Error($"Error while executing commands from {nameof(CommandsManager)}: {ex}");
                throw;
            }
            catch (DbEntityValidationException ex)
            {
                _logger.Error($"Error while executing commands from {nameof(CommandsManager)}: {ex}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.Error($"Error while executing commands from {nameof(CommandsManager)}: {ex}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while executing commands from {nameof(CommandsManager)}: {ex}");
                throw;
            }
        }

        private void TryReloadEntityValues(IEnumerable<DbEntityEntry> entityEntries)
        {
            foreach (var entry in entityEntries)
            {
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            }
        }
    }
}
