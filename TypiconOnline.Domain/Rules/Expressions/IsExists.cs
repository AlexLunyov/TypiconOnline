using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Элемент, возвращающий булевское значение.
    /// Возвращает true, если дочерний элемент ymnos указывает на существующие богослужебные тексты.
    /// </summary>
    public class IsExists : BooleanExpression
    {
        public IsExists(string name) : base(name) { }

        public ICalcStructureElement ChildElement { get; set; }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            ValueCalculated = ChildElement.Calculate(date, handler?.Settings) != null;
        }

        protected override void Validate()
        {
            if (ChildElement == null)
            {
                AddBrokenConstraint(IsExistsBusinessConstraint.YmnosRuleReqiured);
            }
            else if (ChildElement is RuleElement r && !r.IsValid)
            {
                AppendAllBrokenConstraints(r);
            }
        }
    }
}
