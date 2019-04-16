using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Базовый класс для всех главных элементов системы: правил компоновки богослужебных текстов
    /// </summary>
    public abstract class RuleEntity : ValueObjectBase<IRuleSerializerRoot>, IHasId<int>
    {
        IRuleElement _rule;

        public int Id { get; set; }

        /// <summary>
        /// Id Устава (TypiconVersion)
        /// </summary>
        public virtual int TypiconVersionId { get; set; }

        public virtual TypiconVersion TypiconVersion { get; set; }

        private string _ruleDefinition;
        public virtual string RuleDefinition
        {
            get
            {
                return _ruleDefinition;
            }
            set
            {
                _ruleDefinition = value;
                _rule = null;
                //IsValidated = false;
            }
        }

        /// <summary>
        /// Возвращает объектную версию описания Правила
        /// </summary>
        /// <typeparam name="T">Коренной элемент</typeparam>
        /// <param name="serializerRoot">Сериализатор</param>
        /// <returns></returns>
        public virtual T GetRule<T>(IRuleSerializerRoot serializerRoot) where T: IRuleElement
        {
            return InnerGetRule<T>(ref _rule, serializerRoot, RuleDefinition);
        }

        /// <summary>
        /// Данный функционал вынесен отдельно для того, чтобы можно было повторно использовать в ModRuleEntity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rule"></param>
        /// <param name="serializerRoot"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        protected virtual T InnerGetRule<T>(ref IRuleElement rule, IRuleSerializerRoot serializerRoot, string definition) where T : IRuleElement
        {
            if (serializerRoot == null) throw new ArgumentNullException(nameof(serializerRoot));

            if (rule == null)
            {
                rule = serializerRoot.Container<T>().Deserialize(definition);
            }

            return (T) rule;
        }

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            if (!string.IsNullOrEmpty(RuleDefinition))
            {
                var element = GetRule<IRuleElement>(serializerRoot);
                if (element == null)
                {
                    AddBrokenConstraint(new BusinessConstraint("Правило заполнено с неопределяемыми системой ошибками.", "RuleDefinition"));
                }
                else if (!element.IsValid)
                {
                    AppendAllBrokenConstraints(element.GetBrokenConstraints(), "RuleDefinition");
                }
            }
        }
    }
}
