using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Books
{
    public class DeleteMenologyDayCommandHandler : DbContextCommandBase, ICommandHandler<DeleteMenologyDayCommand>
    {
        public DeleteMenologyDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(DeleteMenologyDayCommand command)
        {
            var found = DbContext.Set<DayWorship>().FirstOrDefault(c => c.Id == command.WorshipId);

            if (found == null)
            {
                return Result.Fail($"Текст службы Минеи с Id {command.WorshipId} не найден.");
            }

            DbContext.Set<DayWorship>().Remove(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
