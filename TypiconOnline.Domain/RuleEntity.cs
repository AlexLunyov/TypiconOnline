﻿using System;
using System.Collections.Generic;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Базовый класс для всех главных элементов системы: богослужебных текстов и правил их компоновки
    /// </summary>
    public abstract class RuleEntity/*<T> */ : EntityBase<int>, IAggregateRoot /*where T : RuleContainer*/
    {
        public virtual string Name { get; set; }
        public string PathName
        {
            get
            {
                return (Folder != null) ? (Folder.PathName + "/" + Name) : Name;
            }
        }
        public virtual FolderEntity Folder { get; set; }

        protected RuleContainer _rule = null;
        public virtual RuleContainer Rule
        {
            get
            {
                if ((_rule == null) && !string.IsNullOrEmpty(_ruleDefinition))
                {
                    _rule = RuleContainerFactory.CreateRuleContainer(_ruleDefinition);
                }

                return _rule;
            }
        }

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

                _rule = RuleContainerFactory.CreateRuleContainer(_ruleDefinition);
            }
        }

        protected override void Validate()
        {
            if (_rule == null)
            {
                AddBrokenConstraint(RuleEntityBusinessConstraint.RuleRequired);
            }
            else
            {
                List<BusinessConstraint> constraints = _rule.GetBrokenConstraints();
                if (constraints.Count > 0)
                {
                    foreach(BusinessConstraint constraint in constraints)
                    {
                        AddBrokenConstraint(constraint);
                    }
                }
            }
        }
    }
}
