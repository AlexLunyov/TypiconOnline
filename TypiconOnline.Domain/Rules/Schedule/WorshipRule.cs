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
    public class WorshipRule : ExecContainer, ICustomInterpreted
    {
        public WorshipRule(string name) : base(name) { }

        #region Properties

        public ItemTime Time { get; set; }
        public string Name { get; set; }
        public bool IsDayBefore { get; set; } = false;
        public string AdditionalName { get; set; }
        /// <summary>
        /// Значение заполняется при обработке Правила с помощью CustomParmeter
        /// </summary>
        public HandlingMode ModeFromHandler { get; set; } = HandlingMode.All;

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<WorshipRule>())
            {
                base.InnerInterpret(date, handler);

                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (!Time.IsValid)
            {
                AddBrokenConstraint(ServiceBusinessConstraint.TimeTypeMismatch, ElementName);
            }

            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenConstraint(ServiceBusinessConstraint.NameReqiured, ElementName);
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
}

