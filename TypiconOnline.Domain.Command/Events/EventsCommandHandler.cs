using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Events;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Events
{
    public class EventsCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICommandHandler<TCommand> _decoratee;
        private readonly TypiconDBContext _dbContext;

        public EventsCommandHandler(IEventDispatcher eventDispatcher, ICommandHandler<TCommand> decoratee, TypiconDBContext dbContext)
        {
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _decoratee = decoratee ?? throw new ArgumentNullException(nameof(decoratee));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Result> ExecuteAsync(TCommand command)
        {
            var result = await _decoratee.ExecuteAsync(command);

            if (result.Success)
            {
                _dbContext.ChangeTracker
                .Entries()
                .Where(c => c.Entity is IHasDomainEvents)
                .SelectMany(c => (c.Entity as IHasDomainEvents).GetDomainEvents())
                .ToList()
                .ForEach(c => _eventDispatcher.Dispatch(c));

                _dbContext.SaveChanges();
            }

            return Result.Ok();
        }
    }
}
