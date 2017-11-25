using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;

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

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
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
