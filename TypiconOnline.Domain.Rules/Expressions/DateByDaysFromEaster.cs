using JetBrains.Annotations;
using System;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Правило, возвращающее конкретную дату по числу дней от Пасхи
    /// пример
    /// <datebydaysfromeaster><int>-17</int></datebydaysfromeaster>
    /// </summary>
    public class DateByDaysFromEaster : DateExpression
    {
        IQueryProcessor queryProcessor;

        public DateByDaysFromEaster(string name, [NotNull] IQueryProcessor queryProcessor) : base(name)
        {
            this.queryProcessor = queryProcessor;
        }

        public IntExpression ChildExpression { get; set; }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            ChildExpression.Interpret(handler);

            DateTime easterDate = queryProcessor.Process(new CurrentEasterQuery(handler.Settings.Date.Year));

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
