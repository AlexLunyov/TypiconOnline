using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TypiconOnline.Infrastructure.Common.Events;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    public class EventsTypiconDBContext : TypiconDBContext
    {
        private readonly IEventDispatcher _eventDispatcher;

        public EventsTypiconDBContext(IEventDispatcher eventDispatcher, DbContextOptions<TypiconDBContext> options) : base(options)
        {
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        public override int SaveChanges()
        {
            base.ChangeTracker
                .Entries()
                .Where(c => c.Entity is IHasDomainEvents)
                .SelectMany(c => (c.Entity as IHasDomainEvents).GetDomainEvents())
                .ToList()
                .ForEach(c => _eventDispatcher.Dispatch(c));

            return base.SaveChanges();
        }
    }
}
