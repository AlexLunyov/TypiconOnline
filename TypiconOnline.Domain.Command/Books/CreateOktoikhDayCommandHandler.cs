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
    public class CreateOktoikhDayCommandHandler : DbContextCommandBase, ICommandHandler<CreateOktoikhDayCommand>
    {
        public CreateOktoikhDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateOktoikhDayCommand command)
        {
            var obj = new OktoikhDay()
            {
                Ihos = command.Ihos,
                DayOfWeek = command.DayOfWeek,
                Definition = command.Definition
            };

            DbContext.Set<OktoikhDay>().Add(obj);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
