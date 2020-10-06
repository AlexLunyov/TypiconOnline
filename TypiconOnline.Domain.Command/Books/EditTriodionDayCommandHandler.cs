using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Books
{
    public class EditTriodionDayCommandHandler : DbContextCommandBase, ICommandHandler<EditTriodionDayCommand>
    {
        public EditTriodionDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(EditTriodionDayCommand command)
        {
            var found = DbContext.Set<DayWorship>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Объект с Id {command.Id} не найден.");
            }

            //found.WorshipName.ReplaceValues(command.Name);
            //found.WorshipShortName.ReplaceValues(command.ShortName);
            found.WorshipName.ReplaceValues(new ItemText(new ItemTextUnit(CommonConstants.DefaultLanguage, command.Name)));
            found.WorshipShortName.ReplaceValues(new ItemText(new ItemTextUnit(CommonConstants.DefaultLanguage, command.ShortName)));

            //DaysFromEaster
            EditParent(found, command.DaysFromEaster);

            found.IsCelebrating = command.IsCelebrating;
            found.UseFullName = command.UseFullName;
            found.Definition = command.Definition;

            DbContext.Set<DayWorship>().Update(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        private void EditParent(DayWorship worship, int daysFromEaster)
        {
            var parent = worship.Parent as TriodionDay;
            if (parent.DaysFromEaster != daysFromEaster)
            {
                //значит ищем TriodionDay и задаем его как Parent для worship
                //или же создаем новый
                var newParent = DbContext.Set<TriodionDay>()
                    .FirstOrDefault(c => c.DaysFromEaster == daysFromEaster)
                    ?? new TriodionDay() { DaysFromEaster = daysFromEaster};

                worship.Parent = newParent;
            }
        }
    }
}
