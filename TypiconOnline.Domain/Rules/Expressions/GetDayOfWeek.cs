using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
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
    /// <getdayofweek name="суббота"/>
    /// </summary>
    public class GetDayOfWeek : RuleExpression
    {
        private DateExpression _childDateExp;

        private ItemDayOfWeek _name;

        public GetDayOfWeek(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.GetDayOfWeekAttrName];
            if (attr != null)
            {
                _name = new ItemDayOfWeek(attr.Value);
            }

            if (node.HasChildNodes)
            {
                _childDateExp = Factories.RuleFactory.CreateDateExpression(node.FirstChild);
            }
            //else
            //{
            //    _valueExpression = new ItemDayOfWeek(node.InnerText);

            //    _valueCalculated = (_valueExpression as ItemDayOfWeek).Value;
            //}
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
            if ((_name == null) && (_childDateExp == null))
            {
                AddBrokenConstraint(GetDayOfWeekBusinessConstraint.TermsRequired, ElementName);
            }

            if ((_name != null) && (_childDateExp != null))
            {
                AddBrokenConstraint(GetDayOfWeekBusinessConstraint.TermsTooMuch, ElementName);
            }

            if ((_name != null) && (!_name.IsValid))
            {
                AddBrokenConstraint(GetDayOfWeekBusinessConstraint.DayOfWeekWrongDefinition, ElementName);
            }

            //добавляем ломаные правила к родителю
            if ((_childDateExp != null) && !_childDateExp.IsValid)
            {
                foreach (BusinessConstraint brokenConstraint in _childDateExp.GetBrokenConstraints())
                {
                    AddBrokenConstraint(brokenConstraint, ElementName + "." + brokenConstraint.ConstraintPath);
                }
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (_childDateExp != null)
            {
                _childDateExp.Interpret(date, handler);

                _valueCalculated = ((DateTime)_childDateExp.ValueCalculated).DayOfWeek;
            }
            else
            {
                _valueCalculated = _name.Value;
            }
        }
    }
}
