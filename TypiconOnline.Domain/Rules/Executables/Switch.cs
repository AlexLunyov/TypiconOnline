﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class Switch : RuleExecutable
    {
        //public RuleElement ParentElement { get; set; }

        public Switch(string name) : base(name) { }

        public Switch(XmlNode node) : base(node)
        {
            //ищем expression
            XmlNode expressionNode = node.SelectSingleNode(RuleConstants.ExpressionNodeName);

            if (expressionNode != null)
            {
                XmlNode valNode = expressionNode.FirstChild;
                Expression = Factories.RuleFactory.CreateExpression(valNode);
            }
            
            //ищем элементы case
            XmlNodeList casesList = node.SelectNodes(RuleConstants.CaseNodeName);

            if (casesList != null)
            {
                CaseElements = new List<Case>();

                foreach (XmlNode caseNode in casesList)
                {
                    Case caseElement = new Case(caseNode);
                    CaseElements.Add(caseElement);
                }
            }

            //ищем default
            XmlNode defaultNode = node.SelectSingleNode(RuleConstants.DefaultNodeName);
            if (defaultNode != null)
            {
                Default = new ExecContainer(defaultNode);
            }
        }

        #region Properties

        public RuleExpression Expression { get; set; }

        public List<Case> CaseElements { get; set; }

        public ExecContainer Default { get; set; }

        #endregion

        #region Methods

        protected override void InnerInterpret(DateTime date, IRuleHandler settings)
        {
            Expression.Interpret(date, settings);

            foreach (Case caseElement in CaseElements)
            {
                caseElement.Interpret(date, settings);

                foreach (RuleExpression caseValue in caseElement.ValuesElements)
                {
                    caseValue.Interpret(date, settings);

                    if (Expression.ValueCalculated.Equals(caseValue.ValueCalculated))
                    {
                        //и значения совпадают
                        caseElement.ActionElement.Interpret(date, settings);
                        return;
                    }
                }
            }

            //если мы здесь, значит совпадений не было
            Default?.Interpret(date, settings);
        }

        protected override void Validate()
        {
            if (Expression == null)
            {
                AddBrokenConstraint(SwitchBusinessBusinessConstraint.ConditionRequired, ElementName);
            }
            else
            {
                //добавляем ломаные правила к родителю
                if (!Expression.IsValid)
                {
                    AppendAllBrokenConstraints(Expression, ElementName);
                }
            }

            if (CaseElements == null)
            {
                AddBrokenConstraint(SwitchBusinessBusinessConstraint.CaseRequired, ElementName);
            }
            else
            {
                //добавляем ломаные правила к родителю
                foreach (Case caseElement in CaseElements)
                {
                    if (!caseElement.IsValid)
                    {
                        AppendAllBrokenConstraints(caseElement, string.Format("{0}.{1}", ElementName, RuleConstants.CaseNodeName));
                    }

                    if (Expression?.ExpressionType != caseElement.ExpressionType)
                    {
                        AddBrokenConstraint(SwitchBusinessBusinessConstraint.ConditionsTypeMismatch, ElementName);
                    }
                }
            }

            //добавляем ломаные правила к родителю
            if (Default?.IsValid == false)
            {
                AppendAllBrokenConstraints(Default, string.Format("{0}.{1}", ElementName, RuleConstants.DefaultNodeName));
            }
        }

        #endregion
    }
}

