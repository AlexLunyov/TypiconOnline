using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Правило, возвращающее конкретную дату по числу дней от Пасхи
    /// пример
    /// <datebydaysfromeaster><int>-17</int></datebydaysfromeaster>
    /// </summary>
    public class DateByDaysFromEaster : DateExpression
    {
        private IntExpression _daysFromEaster;

        public DateByDaysFromEaster(XmlNode node) : base(node)
        {
            if (node.HasChildNodes)
            {
                _daysFromEaster = Factories.RuleFactory.CreateIntExpression(node.FirstChild);
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid)
            {
                _daysFromEaster.Interpret(date, handler);

                DateTime easterDate = EasterStorage.Instance.GetCurrentEaster(date.Year);

                _valueCalculated = easterDate.AddDays((int)_daysFromEaster.ValueCalculated);
            }
        }

        protected override void Validate()
        {
            if (_daysFromEaster == null)
            {
                AddBrokenConstraint(DateByDaysFromEasterBusinessConstraint.IntAbsense, ElementName);
            }
            else
            {
                if (!_daysFromEaster.IsValid)
                {
                    foreach (BusinessConstraint brokenRule in _daysFromEaster.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenRule, ElementName + "." + RuleConstants.DateByDaysFromEasterNodeName + "." + brokenRule.ConstraintPath);
                    }
                }
            }
        }
    }
}
