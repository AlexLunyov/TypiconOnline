﻿using System;
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
    public class DeleteScheduleMenologyCommandHandler : DbContextCommandBase, ICommandHandler<DeleteScheduleMenologyCommand>
    {
        public DeleteScheduleMenologyCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Task<Result> ExecuteAsync(DeleteScheduleMenologyCommand command)
        {
            var found = DbContext.Set<ModRuleEntitySchedule<MenologyRule>>().FirstOrDefault(c => c.RuleId == command.RuleId);

            if (found == null)
            {
                return Task.FromResult(Result.Fail($"Объект с Id {command.RuleId} не найден."));
            }

            var version = found.Rule.TypiconVersion;

            //можно редактировать только правила черновика
            if (!(version.BDate == null && version.EDate == null))
            {
                return Task.FromResult(Result.Fail($"Операция удаления невозможна. Правило относится к Версии Устава, находящейся не в статусе Черновика."));
            }

            DbContext.Set<ModRuleEntitySchedule<MenologyRule>>().Remove(found);

            version.IsModified = true;

            return Task.FromResult(Result.Ok());
        }
    }
}
