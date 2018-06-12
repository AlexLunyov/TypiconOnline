using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class TypiconRule : RuleEntity, ITemplateHavingEntity
    {
        public virtual int TemplateId { get; set; }
        public virtual Sign Template { get; set; }
        /// <summary>
        /// Признак, использовать ли определение RuleDefinition как дополнение к шаблону Template
        /// </summary>
        public virtual bool IsAddition { get; set; }
        
        /// <summary>
        /// Возвращает Правило, любо свое, любо шаблонное
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializerRoot"></param>
        /// <returns></returns>
        public override T GetRule<T>(IRuleSerializerRoot serializerRoot) 
        {
            T baseRule = base.GetRule<T>(serializerRoot);

            if ((baseRule == null) && string.IsNullOrEmpty(RuleDefinition))
            {
                return (Template != null ) ? Template.GetRule<T>(serializerRoot) : default(T);
            }

            return baseRule;
        }

        public abstract string GetNameByLanguage(string language);
    }
}
