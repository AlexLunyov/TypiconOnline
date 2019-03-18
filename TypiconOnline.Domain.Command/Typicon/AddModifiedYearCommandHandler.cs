using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class AddModifiedYearCommandHandler : DbContextCommandBase, ICommandHandler<AddModifiedYearCommand>
    {
        public AddModifiedYearCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public void Execute(AddModifiedYearCommand command)
        {
            DbContext.Set<ModifiedYear>().Add(command.ModifiedYear);

            DbContext.SaveChanges();
        }

        public Task ExecuteAsync(AddModifiedYearCommand command)
        {
            DbContext.Set<ModifiedYear>().Update(command.ModifiedYear);

            return DbContext.SaveChangesAsync();
        }
    }
}
