using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IModifiedRuleService
    {
        IRuleSerializerRoot Serializer { get; }
        /// <summary>
        /// Возвращает список измененных правил для конкретной даты
        /// </summary>
        /// <param name="date">Конкретная дата</param>
        /// <returns></returns>
        IEnumerable<ModifiedRule> GetModifiedRules(TypiconEntity typicon, DateTime date);
        /// <summary>
        /// Возвращает измененное правило с мксимальным приоритетом для веденной даты
        /// </summary>
        /// <param name="date">Дата для поиска</param>
        /// <returns></returns>
        ModifiedRule GetModifiedRuleHighestPriority(TypiconEntity typicon, DateTime date);
        void ClearModifiedYears(TypiconEntity typicon);
        void AddModifiedRule(TypiconEntity typicon, ModificationsRuleRequest request);
    }
}
