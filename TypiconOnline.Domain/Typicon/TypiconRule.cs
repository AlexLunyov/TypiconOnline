using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class TypiconRule : RuleEntityToModify, ITemplateHavingEntity
    {
        public virtual int TemplateId { get; set; }
        public virtual Sign Template { get; set; }
        /// <summary>
        /// Признак, использовать ли определение RuleDefinition как дополнение к шаблону Template
        /// </summary>
        public virtual bool IsAddition { get; set; }

        /// <summary>
        /// Возвращает Правило, либо свое, либо шаблонное
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

        /// <summary>
        /// Возвращает Правило, либо свое, либо шаблонное
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializerRoot"></param>
        /// <returns></returns>
        public override T GetRuleToModify<T>(IRuleSerializerRoot serializerRoot)
        {
            T baseRule = base.GetRuleToModify<T>(serializerRoot);

            if ((baseRule == null) && string.IsNullOrEmpty(RuleToModifyDefinition))
            {
                return (Template != null) ? Template.GetRuleToModify<T>(serializerRoot) : default(T);
            }

            return baseRule;
        }

        public abstract string GetNameByLanguage(string language);

        
    }
}
