using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Базовый класс для всех главных элементов системы: правил компоновки богослужебных текстов
    /// </summary>
    public abstract class RuleEntity : EntityBase<int>
    {
        IRuleElement _rule;

        /// <summary>
        /// Id Устава (TypiconVersion)
        /// </summary>
        public virtual int TypiconVersionId { get; set; }

        public virtual TypiconVersion TypiconVersion { get; set; }

        protected string _ruleDefinition;
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
            }
        }

        /// <summary>
        /// Возвращает объектную версию описания Правила
        /// </summary>
        /// <param name="serializerRoot">Сериализатор</param>
        /// <returns></returns>
        public IRuleElement GetRule(IRuleSerializerRoot serializerRoot)
        {
            return GetRule<IRuleElement>(serializerRoot);
        }

        /// <summary>
        /// Возвращает объектную версию описания Правила
        /// </summary>
        /// <typeparam name="T">Коренной элемент</typeparam>
        /// <param name="serializerRoot">Сериализатор</param>
        /// <returns></returns>
        public virtual T GetRule<T>(IRuleSerializerRoot serializerRoot) where T: IRuleElement
        {
            if (serializerRoot == null) throw new ArgumentNullException("IRuleSerializerRoot");

            if (_rule == null)
            {
                _rule = serializerRoot.Container<T>().Deserialize(RuleDefinition);
            }

            return (T) _rule;
        }

        protected override void Validate()
        {
            /*if (_rule == null)
            {
                AddBrokenConstraint(RuleEntityBusinessConstraint.RuleRequired);
            }
            else */if (_rule?.IsValid == false)
            {
                AppendAllBrokenConstraints(_rule.GetBrokenConstraints());
            }
        }
    }

    public class RuleEntityBusinessConstraint
    {
        public static readonly BusinessConstraint RuleRequired = new BusinessConstraint("Правило должно быть определено.");
    }
}
