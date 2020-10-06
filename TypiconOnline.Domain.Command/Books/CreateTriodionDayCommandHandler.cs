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
    public class CreateTriodionDayCommandHandler : DbContextCommandBase, ICommandHandler<CreateTriodionDayCommand>
    {
        public CreateTriodionDayCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateTriodionDayCommand command)
        {
            TriodionDay parent = FindParent(command.DaysFromEaster);

            var obj = new DayWorship()
            {
                Parent = parent,
                WorshipName = new ItemTextStyled(new ItemTextUnit(CommonConstants.DefaultLanguage, command.Name)),
                WorshipShortName = new ItemText(new ItemTextUnit(CommonConstants.DefaultLanguage, command.ShortName)),
                IsCelebrating = command.IsCelebrating,
                UseFullName = command.UseFullName,
                Definition = command.Definition
            };

            DbContext.Set<DayWorship>().Add(obj);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }

        private TriodionDay FindParent(int daysFromEaster)
        {
            return DbContext.Set<TriodionDay>()
                .FirstOrDefault(c => c.DaysFromEaster == daysFromEaster)
                ?? new TriodionDay()
                {
                    DaysFromEaster = daysFromEaster
                };
        }
    }
}
