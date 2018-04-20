using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент "Служба". Является строкой в расписании. 
    /// Включает в себя последовательность богослужения, задекларированного в названии
    /// </summary>
    public class WorshipRule : ExecContainer, ICustomInterpreted, IRewritableElement
    {
        public WorshipRule(string name, IRewritableElement parent) : base(name)
        {
            Parent = parent;
        }

        #region Properties
        /// <summary>
        /// Идентификатор для поиска (используется в переопределении правил AsAddition)
        /// </summary>
        public string Id { get; set; }
        public ItemTime Time { get; set; }
        public string Name { get; set; }
        public bool IsDayBefore { get; set; } = false;
        public string AdditionalName { get; set; }
        /// <summary>
        /// Значение заполняется при обработке Правила с помощью CustomParmeter
        /// </summary>
        public HandlingMode ModeFromHandler { get; set; } = HandlingMode.All;

        #region IRewritableElement implementation

        public IRewritableElement Parent { get; }

        public string RewritableName
        {
            get
            {
                string result = ElementName;

                if (Parent != null)
                {
                    result = $"{Parent.RewritableName}/{result}";
                }

                if (!string.IsNullOrEmpty(Id))
                {
                    result += $"?{RuleConstants.WorshipRuleIdAttrName}={Id}";
                }

                return result;
            }
        }

        public bool Rewrite { get; set; }

        #endregion

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<WorshipRule>())
            {
                base.InnerInterpret(handler);

                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (!Time.IsValid)
            {
                AddBrokenConstraint(WorshipRuleBusinessConstraint.TimeTypeMismatch, ElementName);
            }

            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenConstraint(WorshipRuleBusinessConstraint.NameReqiured, ElementName);
            }

            foreach (RuleElement element in ChildElements)
            {
                //добавляем ломаные правила к родителю
                if (!element.IsValid)
                {
                    foreach (BusinessConstraint brokenRule in element.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenRule, ElementName + "." + brokenRule.ConstraintPath);
                    }
                }
            }
        }
    }

    public class WorshipRuleBusinessConstraint
    {
        public static readonly BusinessConstraint TimeTypeMismatch = new BusinessConstraint("Неверный формат времени.");
        public static readonly BusinessConstraint NameReqiured = new BusinessConstraint("Отсутствуют определение имени.");
        public static readonly BusinessConstraint IsDayBeforeTypeMismatch = new BusinessConstraint("Неверный формат логического обозначения.");
    }
}

