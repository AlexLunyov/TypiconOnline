using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Элемент, возвращающий булевское значение.
    /// Возвращает true, если в списке текстов 
    /// богослужений присутствует служба Господского или Богородиченого праздника, его предпразднства или попразднства.
    /// </summary>
    public class IsCelebrating : BooleanExpression
    {
        public IsCelebrating(string name) : base(name) { }

        protected override void InnerInterpret(IRuleHandler handler)
        {
           ValueCalculated = false;

            foreach (DayWorship day in handler?.Settings?.DayWorships)
            {
                if (day.IsCelebrating)
                {
                    ValueCalculated = true;
                    break;
                }
            }
        }

        protected override void Validate()
        {
        }
    }
}
