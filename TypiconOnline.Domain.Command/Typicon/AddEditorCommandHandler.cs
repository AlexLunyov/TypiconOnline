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
    public class AddEditorCommandHandler : DbContextCommandBase, ICommandHandler<AddEditorCommand>
    {
        public AddEditorCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(AddEditorCommand command)
        {
            var found = DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.Id == command.TypiconId);

            if (found == null)
            {
                return Result.Fail($"Устав с Id {command.TypiconId} не найден.");
            }

            found.EditableUserTypicons.Add(new UserTypicon() { TypiconId = command.TypiconId, UserId = command.UserId });

            DbContext.Set<TypiconEntity>().Update(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
