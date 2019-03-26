
using System;
using System.Collections.Generic;
using System.Linq;
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

        public override bool ExpressionTypeEquals(RuleExpression entity)
        {
            if (IsValid)
            {
                return ValuesElements.First().ExpressionTypeEquals(entity);
            }
            return false;
        }

        public override bool ValueCalculatedEquals(RuleExpression entity)
        {
            foreach (RuleExpression valueElement in ValuesElements)
            {
                if (valueElement.ValueCalculatedEquals(entity))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Methods


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
                    if (!prevValueElement.ExpressionTypeEquals(valueElement))
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

