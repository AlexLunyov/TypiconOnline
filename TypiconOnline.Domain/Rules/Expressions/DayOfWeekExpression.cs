using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Элемент содержит описание дня недели
    /// 
    /// Пример
    /// <dayofweek>воскресенье</dayofweek>
    /// </summary>
    public class DayOfWeekExpression : RuleExpression
    {
        public DayOfWeekExpression(XmlNode node) : base(node)
        {
            _valueExpression = new ItemDayOfWeek(node.InnerText);

            _valueCalculated = (_valueExpression as ItemDayOfWeek).Value;
        }

        #region Properties

        public override Type ExpressionType
        {
            get
            {
                return typeof(ItemDayOfWeek);
            }
        }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            //пусто
        }

        

        protected override void Validate()
        {
            if (!(_valueExpression as ItemDayOfWeek).IsValid)
            {
                foreach (BusinessConstraint brokenConstraint in ((ItemDayOfWeek)_valueExpression).GetBrokenConstraints())
                {
                    AddBrokenConstraint(brokenConstraint, ElementName);
                }
            }
        }
    }
}
