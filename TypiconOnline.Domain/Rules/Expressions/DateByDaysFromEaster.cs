using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Interfaces;
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
        public DateByDaysFromEaster(string name) : base(name) { }

        public DateByDaysFromEaster(XmlNode node) : base(node)
        {
            if (node.HasChildNodes)
            {
                ChildExpression = Factories.RuleFactory.CreateIntExpression(node.FirstChild);
            }
        }

        public IntExpression ChildExpression { get; set; }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            ChildExpression.Interpret(date, handler);

            DateTime easterDate = BookStorage.Instance.Easters.GetCurrentEaster(date.Year);

            ValueCalculated = easterDate.AddDays((int)ChildExpression.ValueCalculated);
        }

        protected override void Validate()
        {
            if (ChildExpression == null)
            {
                AddBrokenConstraint(DateByDaysFromEasterBusinessConstraint.IntAbsense, ElementName);
            }
            else
            {
                if (!ChildExpression.IsValid)
                {
                    AppendAllBrokenConstraints(ChildExpression, ElementName);
                }
            }
        }
    }
}
