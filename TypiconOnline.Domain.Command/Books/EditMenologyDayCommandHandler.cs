using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Books
{
    public class EditMenologyDayCommandHandler : DbContextCommandBase, ICommandHandler<EditMenologyDayCommand>
    {
        public EditMenologyDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(EditMenologyDayCommand command)
        {
            var found = DbContext.Set<DayWorship>().FirstOrDefault(c => c.Id == command.Id);

            if (found == null)
            {
                return Result.Fail($"Объект с Id {command.Id} не найден.");
            }

            found.WorshipName.ReplaceValues(command.Name);
            found.WorshipShortName.ReplaceValues(command.ShortName);

            //LeapDate
            EditParent(found, command.LeapDate);
            found.IsCelebrating = command.IsCelebrating;
            found.UseFullName = command.UseFullName;
            found.Definition = command.Definition;

            DbContext.Set<DayWorship>().Update(found);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        private void EditParent(DayWorship worship, DateTime? leapDate)
        {
            //MenologyDay
            ItemDate date = (leapDate != null)
                ? new ItemDate(leapDate.Value.Month, leapDate.Value.Day)
                : new ItemDate();

            var parent = worship.Parent as MenologyDay;
            if (parent.LeapDate.ToString() != date.ToString())
            {
                //значит ищем MenologyDay и задаем его как Parent для worship
                var newParent = DbContext.Set<MenologyDay>()
                    .FirstOrDefault(c => c.LeapDate.Day == date.Day && c.LeapDate.Month == date.Month);

                worship.Parent = newParent;
            }
        }
    }
}
