using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public abstract class ScheduleSettingsCommandHandlerBase : DbContextCommandBase
    {
        protected ScheduleSettingsCommandHandlerBase(TypiconDBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// ScheduleSettings from Draft by TypiconId
        /// </summary>
        /// <param name="typiconId"></param>
        /// <returns></returns>
        protected ScheduleSettings GetScheduleSettings(int typiconId)
        {
            var draft = DbContext.Set<TypiconVersion>()
                .Where(TypiconVersion.IsDraft)
                .FirstOrDefault(c => c.TypiconId == typiconId);

            if (draft == null)
            {
                return default;
            }

            if (draft.ScheduleSettings == null)
            {
                draft.ScheduleSettings = new ScheduleSettings()
                {
                    TypiconVersion = draft
                };
            }

            return draft.ScheduleSettings;
        }
    }
}
