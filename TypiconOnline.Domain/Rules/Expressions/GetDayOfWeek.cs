using System;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Возвращает день недели у абстрактной даты
    /// 
    /// Примеры
    /// <getdayofweek><date>--01-04</date></getdayofweek>
    /// <getdayofweek><getclosestday dayofweek="saturday" weekcount="-2"><date>--11-08</date></getclosestday></getdayofweek>
    /// </summary>
    public class GetDayOfWeek : RuleExpression
    {
        private DateExpression _childDateExp;

        public GetDayOfWeek(XmlNode node) : base(node)
        {
            if (node.HasChildNodes)
            {
                _childDateExp = Factories.RuleFactory.CreateDateExpression(node.FirstChild);
            }
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

        protected override void Validate()
        {
            if (_childDateExp == null)
            {
                AddBrokenConstraint(GetClosestDayBusinessConstraint.DateRequired, ElementName);
            }
            else
            {
                //добавляем ломаные правила к родителю
                if (!_childDateExp.IsValid)
                {
                    foreach (BusinessConstraint brokenConstraint in _childDateExp.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenConstraint, ElementName + "." + brokenConstraint.ConstraintPath);
                    }
                }
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid)
            {
                _childDateExp.Interpret(date, handler);

                _valueCalculated = new ItemDayOfWeek(((DateTime)_childDateExp.ValueCalculated).DayOfWeek);
            }
        }
    }
}
