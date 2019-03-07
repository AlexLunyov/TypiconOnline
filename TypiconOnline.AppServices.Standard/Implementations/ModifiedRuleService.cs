using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations
{
    public class ModifiedRuleService : IModifiedRuleService
    {
        readonly IUnitOfWork unitOfWork;
        readonly IModifiedYearFactory modifiedYearFactory;

        public ModifiedRuleService(IUnitOfWork unitOfWork, IModifiedYearFactory modifiedYearFactory)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork in ModifiedRuleService");
            this.modifiedYearFactory = modifiedYearFactory ?? throw new ArgumentNullException("modifiedYearFactory in ModifiedRuleService");
        }

        /// <summary>
        /// Возвращает список измененных правил для конкретной даты
        /// </summary>
        /// <param name="date">Конкретная дата</param>
        /// <returns></returns>
        public IEnumerable<ModifiedRule> GetModifiedRules(int typiconId, DateTime date)
        {
            var modifiedYear = unitOfWork.Repository<ModifiedYear>().Get(m => m.TypiconVersionId == typiconId && m.Year == date.Year);

            if (modifiedYear == null)
            {
                modifiedYear = modifiedYearFactory.Create(typiconId, date.Year);
            }

            return modifiedYear.ModifiedRules.Where(d => d.Date.Date == date.Date);
        }

        public ModifiedRule GetModifiedRuleHighestPriority(int typiconId, DateTime date)
        {
            return GetModifiedRules(typiconId, date)?.Min();
        }

    }
}
