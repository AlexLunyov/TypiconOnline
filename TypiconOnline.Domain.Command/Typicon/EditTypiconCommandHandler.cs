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
    public class EditTypiconCommandHandler : DbContextCommandBase, ICommandHandler<EditTypiconCommand>
    {
        public EditTypiconCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(EditTypiconCommand command)
        {
            var found = DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Устав с Id {command.Id} не найден.");
            }

            //находим черновик
            var draft = DbContext.Set<TypiconVersion>()
                .FirstOrDefault(c => c.TypiconId == found.Id
                                && c.BDate == null && c.EDate == null);

            if (draft == null)
            {
                return Result.Fail($"Версия черновика Устава с id={found.Id} не найдена.");
            }

            if (draft.IsTemplate != command.IsTemplate)
            {
                draft.IsTemplate = command.IsTemplate;
                
            }

            //не возможно просто присвоить значение, потому как ef core 
            //будет думать, что TypiconEntity удалена
            draft.Name.ReplaceValues(command.Name);
            draft.Description.ReplaceValues(command.Description);

            //считаем, что существует версия Черновика Устава
            //Находим его и выставлем значение свойства IsTemplate
            //UpdateIsTemplateProperty(found, command.IsTemplate);

            draft.IsModified = true;

            found.DefaultLanguage = command.DefaultLanguage;

            DbContext.Set<TypiconEntity>().Update(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        private Result UpdateIsTemplateProperty(TypiconEntity found, bool isTemplate)
        {
            //находим черновик
            var draft = DbContext.Set<TypiconVersion>()
                .FirstOrDefault(c => c.TypiconId == found.Id
                                && c.BDate == null && c.EDate == null);

            if (draft == null)
            {
                return Result.Fail($"Версия черновика Устава с id={found.Id} не найдена.");
            }

            if (draft.IsTemplate != isTemplate)
            {
                draft.IsTemplate = isTemplate;
                draft.IsModified = true;
            }

            return Result.Ok();
        }
    }
}
