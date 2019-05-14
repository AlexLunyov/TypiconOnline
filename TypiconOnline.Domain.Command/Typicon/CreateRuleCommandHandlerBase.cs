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
    public abstract class CreateRuleCommandHandlerBase<T> : DbContextCommandBase where T : RuleEntity, new()
    {
        protected CreateRuleCommandHandlerBase(TypiconDBContext dbContext) : base(dbContext)
        {
        }

        protected async Task<Result> ExecuteAsync(CreateRuleCommandBase<T> command) 
        {
            var version = DbContext.Set<TypiconVersion>()
                .FirstOrDefault(c => c.TypiconId == command.TypiconId
                                    && c.IsDraft);

            if (version == null)
            {
                return Result.Fail($"Версия Устава с Id {command.TypiconId} не найдена.");
            }

            var obj = Create(command, version.Id);

            DbContext.Set<T>().Add(obj);

            version.IsModified = true;

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        protected abstract T Create(CreateRuleCommandBase<T> command, int typiconVersionId);
    }
}
