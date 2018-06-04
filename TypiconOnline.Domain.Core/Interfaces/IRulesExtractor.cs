using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Domain.Core.Interfaces
{
    /// <summary>
    /// Извлекает связанные Правила для конкретного Устава
    /// </summary>
    public interface IRulesExtractor
    {
        MenologyRule GetMenologyRule(int typiconId, DateTime date);
        IEnumerable<MenologyRule> GetAllMenologyRules(int typiconId);
        TriodionRule GetTriodionRule(int typiconId, int daysFromEaster);
        IEnumerable<TriodionRule> GetAllTriodionRules(int typiconId);
        CommonRule GetCommonRule(int typiconId, string name);
        /// <summary>
        /// ModifiedRule с наивысшим приоритетом для заданной даты
        /// </summary>
        /// <param name="typiconId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        //ModifiedRule GetModifiedRule(int typiconId, DateTime date);
    }
}
