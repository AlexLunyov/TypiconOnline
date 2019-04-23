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
    public abstract class EditRuleCommandHandlerBase<T> : DbContextCommandBase where T : RuleEntity, new()
    {
        protected EditRuleCommandHandlerBase(TypiconDBContext dbContext) : base(dbContext)
        {
        }

        protected async Task<Result> ExecuteAsync(EditRuleCommandBase<T> command) 
        {
            var found = DbContext.Set<T>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Объект с Id {command.Id} не найден.");
            }

            UpdateValues(found, command);

            DbContext.Set<T>().Update(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        protected abstract void UpdateValues(T found, EditRuleCommandBase<T> command);
    }
}
