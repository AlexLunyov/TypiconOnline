using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public abstract class DeleteRuleCommandHandlerBase<T> : DbContextCommandBase where T : class, ITypiconVersionChild, new()
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

            var version = found.TypiconVersion;

            //можно редактировать только правила черновика
            if (!(version.BDate == null && version.EDate == null))
            {
                return Result.Fail($"Операция удаления невозможна. Правило относится к Версии Устава, находящейся не в статусе Черновика.");
            }

            var addWorkResult = PerformAdditionalWork(found, command);

            if (addWorkResult.Success)
            {
                DbContext.Set<T>().Remove(found);

                version.IsModified = true;

                await DbContext.SaveChangesAsync();
            }
            
            return addWorkResult;
        }

        /// <summary>
        /// Реализация дополнительных действий. Перегружается в наследниках
        /// </summary>
        /// <param name="found"></param>
        /// <returns></returns>
        protected virtual Result PerformAdditionalWork(T found, DeleteRuleCommandBase<T> command) => Result.Ok();
    }
}
