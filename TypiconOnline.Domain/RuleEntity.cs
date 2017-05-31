using System;
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
    public class RuleEntity/*<T> */ : EntityBase<int>, IAggregateRoot /*where T : RuleContainer*/
    {

        public virtual string Name { get; set; }

        public virtual string CommonName
        {
            get
            {
                return Name;
            }
        }

        //private ItemText _name1 = new ItemText();
        public virtual ItemText Name1
        {
            get;
            //{

            //}
            set;
        }



        public virtual ItemText Name2 { get; set; }

        public virtual FolderEntity Folder { get; set; }

        public virtual TypiconEntity Owner { get; set; }

        public string PathName
        {
            get
            {
                return (Folder != null) ? (Folder.PathName + "/" + Name) : Name;
            }
        }

        private RuleContainer _rule = null;
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


        private string _ruleDefinition;
        public string RuleDefinition
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
