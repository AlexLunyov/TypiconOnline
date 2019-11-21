using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteMenologyDayCommandHandler : DbContextCommandBase, ICommandHandler<DeleteEditorCommand>
    {
        public DeleteMenologyDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(DeleteEditorCommand command)
        {
            var found = DbContext.Set<UserTypicon>().FirstOrDefault(c => c.TypiconId == command.TypiconId && c.UserId == command.UserId);

            if (found == null)
            {
                return Result.Fail($"Редактор у Устава с Id {command.TypiconId} не найден.");
            }

            DbContext.Set<UserTypicon>().Remove(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
