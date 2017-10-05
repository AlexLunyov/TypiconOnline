using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Executables
{
    /// <summary>
    /// Класс реализует обработку xml-конструкции if-then-else
    /// </summary>
    public class If : RuleExecutable
    {
        public If(XmlNode node): base(node)
        {
            //ищем condition
            XmlNode elementNode = node.SelectSingleNode(RuleConstants.ExpressionIfNodeName);

            if (elementNode != null)
            {
                XmlNode valNode = elementNode.FirstChild;
                _expression = Factories.RuleFactory.CreateBooleanExpression(valNode);
            }

            //ищем then
            elementNode = node.SelectSingleNode(RuleConstants.ThenNodeName);
            if (elementNode != null)
            {
                _then = new ExecContainer(elementNode);
            }

            //ищем else
            elementNode = node.SelectSingleNode(RuleConstants.ElseNodeName);
            if (elementNode != null)
            {
                _else = new ExecContainer(elementNode);
            }
        }

        #region Properties

        private BooleanExpression _expression;
        public BooleanExpression Expression
        {
            get
            {
                return _expression;
            }
        }

        private ExecContainer _then;

        private ExecContainer _else;

        #endregion

        #region Methods

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid)
            {
                Expression.Interpret(date, handler);

                if ((bool)Expression.ValueCalculated)
                {
                    _then.Interpret(date, handler);
                }
                else if (_else != null)
                {
                    _else.Interpret(date, handler);
                }
            }
        }

        protected override void Validate()
        {
            if (_expression == null)
            {
                AddBrokenConstraint(IfBusinessBusinessConstraint.ConditionRequired, ElementName);
            }
            else
            {
                //добавляем ломаные правила к родителю
                if (!_expression.IsValid)
                {
                    AppendAllBrokenConstraints(_expression);
                }
            }

            if (_then == null)
            {
                AddBrokenConstraint(IfBusinessBusinessConstraint.ThenRequired, ElementName);
            }
            else
            {
                //добавляем ломаные правила к родителю
                if (!_then.IsValid)
                {
                    AppendAllBrokenConstraints(_then);
                }
            }

            if (_else?.IsValid == false)
            {
                AppendAllBrokenConstraints(_else);
            }
        }

        #endregion
    }
}
