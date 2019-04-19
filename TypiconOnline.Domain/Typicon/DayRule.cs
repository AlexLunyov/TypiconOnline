using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class DayRule : TemplateHavingRule, IDayRule
    {
        public DayRule() { }

        /// <summary>
        /// Список последований текстов служб для данного дня Устава
        /// </summary>
        public virtual List<DayRuleWorship> DayRuleWorships { get; set; } = new List<DayRuleWorship>();

        public IReadOnlyList<DayWorship> DayWorships
        {
            get
            {
                (DayRuleWorships as List<DayRuleWorship>).Sort();
                
                return (from drw in DayRuleWorships select drw.DayWorship).ToList();
            }
        }

        /// <summary>
        /// Возвращает объединенную строку всех служб для определенного языка
        /// </summary>
        public override string GetNameByLanguage(string language)
        {
            string result = "";
            if (DayWorships.Count > 0)
            {
                foreach (DayWorship serv in DayWorships)
                {
                    result += serv.WorshipName.FirstOrDefault(language).Text + " ";
                }

                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            base.Validate(serializerRoot);

            if (DayRuleWorships.Count == 0)
            {
                AddBrokenConstraint(new BusinessConstraint("В коллекции Текстов служб должен быть определен хотя бы один элемент", "DayRule"));
            }
        }
    }
}
