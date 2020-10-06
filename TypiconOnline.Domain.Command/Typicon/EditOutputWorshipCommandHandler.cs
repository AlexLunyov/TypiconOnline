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
    public class EditOutputWorshipCommandHandler : DbContextCommandBase, ICommandHandler<EditOutputWorshipCommand>
    {
        public EditOutputWorshipCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Task<Result> ExecuteAsync(EditOutputWorshipCommand command)
        {
            var found = DbContext.Set<OutputWorship>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Task.FromResult(Result.Fail($"Объект с Id {command.Id} не найден."));
            }

            found.Name = new ItemTextStyled(new ItemTextUnit(CommonConstants.DefaultLanguage, command.Name))
            {
                IsBold = command.NameStyle.IsBold,
                IsItalic = command.NameStyle.IsItalic,
                IsRed = command.NameStyle.IsRed
            };

            found.AdditionalName = (!string.IsNullOrEmpty(command.AdditionalName)) 
                ? new ItemTextStyled(new ItemTextUnit(CommonConstants.DefaultLanguage, command.AdditionalName))
                {
                    IsBold = command.AdditionalNameStyle.IsBold,
                    IsItalic = command.AdditionalNameStyle.IsItalic,
                    IsRed = command.AdditionalNameStyle.IsRed
                }
                : new ItemTextStyled();

            found.Time = command.Time;

            //фиксируем изменения
            found.ModifiedDate = DateTime.Now;

            DbContext.Set<OutputWorship>().Update(found);

            return Task.FromResult(Result.Ok());
        }
    }
}
