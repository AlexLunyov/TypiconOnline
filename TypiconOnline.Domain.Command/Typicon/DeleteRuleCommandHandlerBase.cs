using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public abstract class DeleteRuleCommandHandlerBase<T> : DbContextCommandBase where T : RuleEntity, new()
    {
        protected DeleteRuleCommandHandlerBase(TypiconDBContext dbContext) : base(dbContext)
        {
        }

        protected async Task<Result> ExecuteAsync(DeleteRuleCommandBase<T> command) 
        {
            var found = DbContext.Set<T>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Объект с Id {command.Id} не найден.");
            }

            DbContext.Set<T>().Remove(found);

            found.TypiconVersion.IsModified = true;

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
