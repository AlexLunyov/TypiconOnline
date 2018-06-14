﻿using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Элемент, возвращающий булевское значение.
    /// Возвращает true, если в списке текстов 
    /// богослужений присутствуют две службы, не отмеченные признаком Праздника, принадлежащие к Минее.
    /// </summary>
    public class IsTwoSaints : BooleanExpression
    {
        public IsTwoSaints(string name) : base(name) { }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            //if (handler.isauthorized<iscelebrating>())
            //{
                int i = 0;

                foreach (DayWorship day in handler.Settings.DayWorships)
                {
                    if (day.Parent is MenologyDay && !day.IsCelebrating)
                    {
                       i++;
                    }
                }
                ValueCalculated = (i > 1);
            //}
        }

        protected override void Validate()
        {
        }
    }
}
