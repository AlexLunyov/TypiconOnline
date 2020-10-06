using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Domain.Common;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditOutputDayCommandHandler : DbContextCommandBase, ICommandHandler<EditOutputDayCommand>
    {
        public EditOutputDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Task<Result> ExecuteAsync(EditOutputDayCommand command)
        {
            var found = DbContext.Set<OutputDay>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null || found.Header == null)
            {
                return Task.FromResult(Result.Fail($"Объект с Id {command.Id} не найден."));
            }

            found.Header.Name = new ItemTextStyled(new ItemTextUnit(CommonConstants.DefaultLanguage, command.Name))
            {
                IsBold = command.NameStyle.IsBold,
                IsItalic = command.NameStyle.IsItalic,
                IsRed = command.NameStyle.IsRed
            };

            found.Header.PrintDayTemplateId = command.PrintDayTemplateId;

            //фиксируем изменения
            found.ModifiedDate = DateTime.Now;

            DbContext.Set<OutputDay>().Update(found);

            return Task.FromResult(Result.Ok());
        }
    }
}
