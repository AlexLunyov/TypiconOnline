using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class TypiconRule : ModRuleEntity, ITemplateHavingEntity
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
        public override T GetModRule<T>(IRuleSerializerRoot serializerRoot)
        {
            T baseRule = base.GetModRule<T>(serializerRoot);

            if ((baseRule == null) && string.IsNullOrEmpty(ModRuleDefinition))
            {
                return (Template != null) ? Template.GetModRule<T>(serializerRoot) : default(T);
            }

            return baseRule;
        }

        public abstract string GetNameByLanguage(string language);

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            base.Validate(serializerRoot);

            if (Template == null || TemplateId < 1)
            {
                AddBrokenConstraint(new BusinessConstraint("Шаблон для Правила должен быть определен", "TypiconRule"));
            }
        }
    }
}
