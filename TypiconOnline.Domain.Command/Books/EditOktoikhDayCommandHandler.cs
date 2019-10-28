using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Books
{
    public class EditOktoikhDayCommandHandler : DbContextCommandBase, ICommandHandler<EditOktoikhDayCommand>
    {
        public EditOktoikhDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(EditOktoikhDayCommand command)
        {
            var found = DbContext.Set<OktoikhDay>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Объект с Id {command.Id} не найден.");
            }

            //found.Ihos = command.Ihos;
            //found.DayOfWeek = command.DayOfWeek;
            found.Definition = command.Definition;

            DbContext.Set<OktoikhDay>().Update(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
