
using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class Case : RuleExpression
    {
        public Case(string name) : base(name) { }

        #region Properties

        public List<RuleExpression> ValuesElements { get; set; } = new List<RuleExpression>();

        public ExecContainer ActionElement { get; set; }

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

        protected override void InnerInterpret(IRuleHandler settings)
        {
            foreach (RuleExpression valueElement in ValuesElements)
            {
                valueElement.Interpret(settings);
            }
        }

        protected override void Validate()
        {
            if (ValuesElements.Count == 0)
            {
                AddBrokenConstraint(CaseBusinessConstraint.ValuesRequired, ElementName);
            }

            RuleExpression prevValueElement = null;
            foreach (RuleExpression valueElement in ValuesElements)
            {
                //проверяем, одного ли типа элементы
                if (prevValueElement != null)
                {
                    if (prevValueElement.ExpressionType != valueElement.ExpressionType)
                    {
                        AddBrokenConstraint(CaseBusinessConstraint.ValuesTypeMismatch, ElementName);
                    }
                }
                prevValueElement = valueElement;
            }

            if (ActionElement == null)
            {
                AddBrokenConstraint(CaseBusinessConstraint.ActionsRequired, ElementName);
            }
        }

        #endregion
    }
}

