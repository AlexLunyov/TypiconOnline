using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteTypiconCommandHandler : DbContextCommandBase, ICommandHandler<DeleteTypiconCommand>
    {
        public DeleteTypiconCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(DeleteTypiconCommand command)
        {
            var found = DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Устав с Id {command.Id} не найден.");
            }

            DbContext.Set<TypiconEntity>().Remove(found);

            try
            {
                await DbContext.SaveChangesAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
                {
                    return Result.Fail("Произошла ошибка при удалении Устава.");
                }

                throw ex;
            }
        }
    }
}
