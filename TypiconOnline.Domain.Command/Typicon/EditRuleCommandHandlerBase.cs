using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public abstract class EditRuleCommandHandlerBase<T> : DbContextCommandBase where T : class, ITypiconVersionChild, new()
    {
        protected EditRuleCommandHandlerBase(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext)
        {
            SerializerRoot = serializerRoot ?? throw new ArgumentNullException(nameof(serializerRoot));
        }

        protected CollectorSerializerRoot SerializerRoot { get; }

        protected async Task<Result> ExecuteAsync(EditRuleCommandBase<T> command) 
        {
            var found = DbContext.Set<T>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Объект с Id {command.Id} не найден.");
            }

            var version = found.TypiconVersion;

            //можно редактировать только правила черновика
            if (!(version.BDate == null && version.EDate == null))
            {
                return Result.Fail($"Правило относится к Версии Устава, находящейся не в статусе Черновика.");
            }

            var updResult = UpdateValues(found, command);
            if (updResult.Failure)
            {
                return updResult;
            }

            DbContext.Set<T>().Update(found);

            found.TypiconVersion.IsModified = true;

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        protected abstract Result UpdateValues(T found, EditRuleCommandBase<T> command);
    }
}
