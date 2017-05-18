
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class Case : RuleExpression
    {
        private List<RuleExpression> _valuesElements;
        private ExecContainer _actionElement;


        public Case(XmlNode caseNode) : base(caseNode)
        {
            XmlNode valuesNode = caseNode.SelectSingleNode(RuleConstants.ValuesNodeName);

            _valuesElements = new List<RuleExpression>();

            if ((valuesNode != null) && (valuesNode.ChildNodes != null))
            {
                foreach (XmlNode valueNode in valuesNode.ChildNodes)
                {
                    RuleExpression valueElement = Factories.RuleFactory.CreateExpression(valueNode);
                    _valuesElements.Add(valueElement);
                }
            }

            XmlNode actionNode = caseNode.SelectSingleNode(RuleConstants.ActionNodeName);

            if (actionNode != null)
            {
                _actionElement = Factories.RuleFactory.CreateExecContainer(actionNode);
            }
        }

        #region Properties

        public List<RuleExpression> ValuesElements
        {
            get
            {
                return _valuesElements;
            }
        }

        public ExecContainer ActionElement
        {
            get
            {
                return _actionElement;
            }
        }

        public override Type ExpressionType
        {
            get
            {
                if (ValuesElements.Count > 0)
                {
                    return ValuesElements[0].ExpressionType;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Methods

        public override bool ValueExpressionEquals(RuleExpression entity)
        {
            if (entity != null)
            {
                foreach (RuleExpression valueElement in ValuesElements)
                {
                    if (valueElement.ValueExpressionEquals(entity))
                        return true;
                }
            }

            return false;
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler settings)
        {
            if (IsValid)
            {
                foreach (RuleExpression valueElement in ValuesElements)
                {
                    valueElement.Interpret(date, settings);
                }
            }
        }

        protected override void Validate()
        {
            if (_valuesElements.Count == 0)
            {
                AddBrokenConstraint(CaseBusinessConstraint.ValuesRequired, ElementName);
            }

            RuleExpression prevValueElement = null;
            foreach (RuleExpression valueElement in _valuesElements)
            {
                //проверяем, одного ли типа элементы
                if (prevValueElement != null)
                {
                    if (prevValueElement.ExpressionType != valueElement.ExpressionType)
                    {
                        AddBrokenConstraint(CaseBusinessConstraint.ValuesTypeMismatch, ElementName);
                        //throw new DefinitionsParsingException("Ошибка: значения элемента " + valuesNode.Name + " должны быть одного типа");
                    }
                }
                prevValueElement = valueElement;
            }

            if (_actionElement == null)
            {
                AddBrokenConstraint(CaseBusinessConstraint.ActionsRequired, ElementName);
                //throw new DefinitionsParsingException("Ошибка: отсутствует элемент " + DefinitionsConstants.ActionNodeName + " в элементе " + DefinitionsConstants.CaseNodeName);
            }
        }

        #endregion
    }
}

