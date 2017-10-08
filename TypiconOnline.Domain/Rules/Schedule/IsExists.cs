using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент, возвращающий булевское значение.
    /// Возвращает true, если дочерний элемент ymnos указывает на существующие богослужебные тексты.
    /// </summary>
    public class IsExists : BooleanExpression, ICustomInterpreted
    {
        private YmnosRule _ymnos;

        public IsExists(XmlNode node) : base(node)
        {
            if (node.HasChildNodes && node.FirstChild.Name == RuleConstants.YmnosRuleNode)
            {
                _ymnos = RuleFactory.CreateYmnosRule(node.FirstChild);
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            _valueCalculated = _ymnos.CalculateYmnosStructure(date, handler) != null;
        }

        protected override void Validate()
        {
            if (_ymnos == null)
            {
                AddBrokenConstraint(IsExistsBusinessConstraint.YmnosRuleReqiured);
            }
            else if (!_ymnos.IsValid)
            {
                AppendAllBrokenConstraints(_ymnos);
            }
        }
    }
}
