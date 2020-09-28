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

namespace TypiconOnline.Domain.Command.Typicon
{
    public class ScheduleSettingsWeekDaysCommandHandler : ScheduleSettingsCommandHandlerBase, ICommandHandler<ScheduleSettingsWeekDaysCommand>
    {
        public ScheduleSettingsWeekDaysCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Task<Result> ExecuteAsync(ScheduleSettingsWeekDaysCommand command)
        {
            var settings = GetScheduleSettings(command.TypiconId);

            if (settings == null)
            {
                return Task.FromResult(Result.Fail($"Объект с Id {command.TypiconId} не найден."));
            }

            settings.IsMonday = command.IsMonday;
            settings.IsTuesday = command.IsTuesday;
            settings.IsWednesday = command.IsWednesday;
            settings.IsThursday = command.IsThursday;
            settings.IsFriday = command.IsFriday;
            settings.IsSaturday = command.IsSaturday;
            settings.IsSunday = command.IsSunday;

            //фиксируем изменения
            settings.TypiconVersion.IsModified = true;

            DbContext.Set<TypiconVersion>().Update(settings.TypiconVersion);

            return Task.FromResult(Result.Ok());
        }

    }
}
