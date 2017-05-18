using System;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Элемент содержит описание дня недели
    /// Возвращает день недели у абстрактной даты
    /// 
    /// Примеры
    /// <dayofweek>воскресенье</dayofweek>
    /// 
    /// <getdayofweek><date>--01-04</date></getdayofweek>
    /// <getdayofweek><getclosestday dayofweek="saturday" weekcount="-2"><date>--11-08</date></getclosestday></getdayofweek>
    /// </summary>
    public class GetDayOfWeek : RuleExpression
    {
        private DateExpression _childDateExp;

        public GetDayOfWeek(XmlNode node) : base(node)
        {
            if (node.HasChildNodes && (node.FirstChild.NodeType != XmlNodeType.Text))
            {
                _childDateExp = Factories.RuleFactory.CreateDateExpression(node.FirstChild);
            }
            else
            {
                _valueExpression = new ItemDayOfWeek(node.InnerText);

                _valueCalculated = (_valueExpression as ItemDayOfWeek).Value;
            }
        }

        #region Properties

        public override Type ExpressionType
        {
            get
            {
                return typeof(DayOfWeek);
            }
        }

        #endregion

        protected override void Validate()
        {
            if (_childDateExp == null)
            {
                if ((_valueExpression == null) || (!(_valueExpression as ItemDayOfWeek).IsValid))
                {
                    AddBrokenConstraint(GetClosestDayBusinessConstraint.DateRequired, ElementName);
                }
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
                if (_childDateExp != null)
                {
                    _childDateExp.Interpret(date, handler);

                    _valueCalculated = ((DateTime)_childDateExp.ValueCalculated).DayOfWeek;
                }
            }
        }
    }
}
