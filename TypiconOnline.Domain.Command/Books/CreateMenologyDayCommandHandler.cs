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
    public class CreateMenologyDayCommandHandler : DbContextCommandBase, ICommandHandler<CreateMenologyDayCommand>
    {
        public CreateMenologyDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateMenologyDayCommand command)
        {
            MenologyDay parent = FindParent(command.LeapDate);

            var obj = new DayWorship()
            {
                Parent = parent,
                WorshipName = command.Name,
                WorshipShortName = command.ShortName,
                IsCelebrating = command.IsCelebrating,
                UseFullName = command.UseFullName,
                Definition = command.Definition
            };

            DbContext.Set<DayWorship>().Add(obj);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        private MenologyDay FindParent(DateTime? leapDate)
        {
            //MenologyDay
            ItemDate date = (leapDate != null)
                ? new ItemDate(leapDate.Value.Month, leapDate.Value.Day)
                : new ItemDate();

            return DbContext.Set<MenologyDay>()
                    .FirstOrDefault(c => c.LeapDate.Day == date.Day && c.LeapDate.Month == date.Month);
        }
    }
}
