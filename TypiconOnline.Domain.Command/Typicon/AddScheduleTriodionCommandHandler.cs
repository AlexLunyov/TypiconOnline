﻿using System;
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
    public class AddScheduleTriodionCommandHandler : ScheduleSettingsCommandHandlerBase, ICommandHandler<AddScheduleTriodionCommand>
    {
        public AddScheduleTriodionCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(AddScheduleTriodionCommand command)
        {
            var settings = GetScheduleSettings(command.TypiconId);

            if (settings == null)
            {
                return Result.Fail($"Объект с Id {command.TypiconId} не найден.");
            }

            //проверяем, нет ли уже такого
            var existing = settings.TriodionRules.FirstOrDefault(c => c.RuleId == command.RuleId);

            if (existing == null)
            {
                settings.TriodionRules.Add(new ModRuleEntitySchedule<TriodionRule>()
                {
                    ScheduleSettings = settings,
                    RuleId = command.RuleId
                });

                //фиксируем изменения
                settings.TypiconVersion.IsModified = true;

                DbContext.Set<TypiconVersion>().Update(settings.TypiconVersion);

                return Result.Ok();
            }
            else
            {
                return Result.Fail("Данный праздник Триоди уже добавлен в график");
            }
            
        }
    }
}
