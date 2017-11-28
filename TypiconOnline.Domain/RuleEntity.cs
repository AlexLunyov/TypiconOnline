using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Базовый класс для всех главных элементов системы: правил компоновки богослужебных текстов
    /// </summary>
    public abstract class RuleEntity : EntityBase<int>
    {
        RuleElement _rule;

        public virtual string Name { get; set; }

        //public string PathName
        //{
        //    get
        //    {
        //        return (Folder != null) ? (Folder.PathName + "/" + Name) : Name;
        //    }
        //}
        //public virtual FolderEntity Folder { get; set; }

        //
        //public virtual RuleElement Rule
        //{
        //    get
        //    {
        //        if ((_rule == null) && !string.IsNullOrEmpty(_ruleDefinition))
        //        {
        //            //_rule = RuleFactory.CreateElement(_ruleDefinition);//RuleContainerFactory.CreateRuleContainer(_ruleDefinition);
        //        }

        //        return _rule;
        //    }
        //}

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
        public RuleElement GetRule(IRuleSerializerRoot serializerRoot)
        {
            return GetRule<RuleElement>(serializerRoot);
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
                //List<BusinessConstraint> constraints = _rule.GetBrokenConstraints();
                //if (constraints.Count > 0)
                //{
                //    foreach(BusinessConstraint constraint in constraints)
                //    {
                //        AddBrokenConstraint(constraint);
                //    }
                //}
            }
        }
    }
}
