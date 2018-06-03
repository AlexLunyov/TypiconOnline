using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Базовый класс для всех главных элементов системы: правил компоновки богослужебных текстов
    /// </summary>
    public abstract class RuleEntity : EntityBase<int>
    {
        RuleElement _rule;

        /// <summary>
        /// Id Устава (TypiconEntity)
        /// </summary>
        public virtual int OwnerId { get; set; }

        public virtual TypiconEntity Owner { get; set; }

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
        /// <returns>Правило должно иметь корневой элемент RootContainer</returns>
        public RootContainer GetRule(IRuleSerializerRoot serializerRoot)
        {
            return GetRule<RootContainer>(serializerRoot);
        }

        /// <summary>
        /// Возвращает объектную версию описания Правила
        /// </summary>
        /// <typeparam name="T">Коренной элемент</typeparam>
        /// <param name="serializerRoot">Сериализатор</param>
        /// <returns></returns>
        public virtual T GetRule<T>(IRuleSerializerRoot serializerRoot) where T: RuleElement
        {
            if (serializerRoot == null) throw new ArgumentNullException("IRuleSerializerRoot");

            if (_rule == null)
            {
                _rule = serializerRoot.Container<T>().Deserialize(RuleDefinition);
            }

            return _rule as T;
        }

        protected override void Validate()
        {
            /*if (_rule == null)
            {
                AddBrokenConstraint(RuleEntityBusinessConstraint.RuleRequired);
            }
            else */if (_rule?.IsValid == false)
            {
                AppendAllBrokenConstraints(_rule);
            }
        }
    }

    public class RuleEntityBusinessConstraint
    {
        public static readonly BusinessConstraint RuleRequired = new BusinessConstraint("Правило должно быть определено.");
    }
}
