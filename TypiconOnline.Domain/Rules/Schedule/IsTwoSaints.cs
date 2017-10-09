﻿using System;
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
    /// богослужений присутствуют две службы, не отмеченные признаком Праздника, принадлежащие к Минее.
    /// </summary>
    public class IsTwoSaints : BooleanExpression, ICustomInterpreted
    {
        public IsTwoSaints(XmlNode node) : base(node)
        {
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            //if (handler.isauthorized<iscelebrating>())
            //{
                int i = 0;

                foreach (DayService day in handler.Settings.DayServices)
                {
                    if (day.Parent is MenologyDay && !day.IsCelebrating)
                    {
                       i++;
                    }
                }
                _valueCalculated = (i > 1);
            //}
        }

        protected override void Validate()
        {
        }
    }
}