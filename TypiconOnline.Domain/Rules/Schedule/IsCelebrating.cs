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

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент, возвращающий булевское значение.
    /// Возвращает true, если в списке текстов 
    /// богослужений присутствует служба Господского или Богородиченого праздника, его предпразднства или попразднства.
    /// </summary>
    public class IsCelebrating : BooleanExpression
    {
        public IsCelebrating(XmlNode node) : base(node)
        {
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
           _valueCalculated = false;

            foreach (DayService day in handler.Settings.DayServices)
            {
                if (day.IsCelebrating)
                {
                    _valueCalculated = true;
                    break;
                }
            }
        }

        protected override void Validate()
        {
        }
    }
}
