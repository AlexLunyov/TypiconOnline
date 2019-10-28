using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Oktoikh;
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
    public class DeleteOktoikhDayCommandHandler : DbContextCommandBase, ICommandHandler<DeleteOktoikhDayCommand>
    {
        public DeleteOktoikhDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(DeleteOktoikhDayCommand command)
        {
            var found = DbContext.Set<OktoikhDay>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Текст службы Октоиха с Id {command.Id} не найден.");
            }

            DbContext.Set<OktoikhDay>().Remove(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
