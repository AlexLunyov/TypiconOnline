using TypiconOnline.Domain.Rules.Interfaces;

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

        protected override void InnerInterpret(IRuleHandler handler)
        {
            ValueCalculated = ChildElement.Calculate(handler?.Settings) != null;
        }

        protected override void Validate()
        {
            if (ChildElement == null)
            {
                AddBrokenConstraint(IsExistsBusinessConstraint.YmnosRuleReqiured);
            }
            else if (ChildElement is RuleElementBase r && !r.IsValid)
            {
                AppendAllBrokenConstraints(r);
            }
        }
    }
}
