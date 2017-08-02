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

        public Switch(XmlNode node) : base(node)
        {
            //ищем expression
            XmlNode expressionNode = node.SelectSingleNode(RuleConstants.ExpressionNodeName);

            if (expressionNode != null)
            {
                XmlNode valNode = expressionNode.FirstChild;
                _expression = Factories.RuleFactory.CreateExpression(valNode);
            }
            
            //ищем элементы case
            XmlNodeList casesList = node.SelectNodes(RuleConstants.CaseNodeName);

            if (casesList != null)
            {
                _caseElements = new List<Case>();

                foreach (XmlNode caseNode in casesList)
                {
                    Case caseElement = new Case(caseNode);
                    _caseElements.Add(caseElement);
                }
            }

            //ищем default
            XmlNode defaultNode = node.SelectSingleNode(RuleConstants.DefaultNodeName);
            if (defaultNode != null)
            {
                _default = new ExecContainer(defaultNode);

                
            }
        }

        #region Properties

        private RuleExpression _expression;
        public RuleExpression Expression
        {
            get
            {
                return _expression;
            }
        }

        private List<Case> _caseElements;


        //public List<Case> CaseElements
        //{
        //    get
        //    {
        //        return _caseElements;
        //    }
        //}

        private ExecContainer _default;

        //public RuleExecutable Default
        //{
        //    get
        //    {
        //        return _default;
        //    }
        //}

        #endregion

        #region Methods

        protected override void InnerInterpret(DateTime date, IRuleHandler settings)
        {
            if (IsValid)
            {
                Expression.Interpret(date, settings);

                foreach (Case caseElement in _caseElements)
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
                if (_default != null)
                {
                    _default.Interpret(date, settings);
                }
            }
        }

        protected override void Validate()
        {
            if (_expression == null)
            {
                AddBrokenConstraint(SwitchBusinessBusinessConstraint.ConditionRequired, ElementName);
                //throw new DefinitionsParsingException("Ошибка: в элементе switch должен быть определен элемент expression.");
            }
            else
            {
                //добавляем ломаные правила к родителю
                if (!_expression.IsValid)
                {
                    foreach (BusinessConstraint brokenRule in _expression.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenRule, ElementName + "." + brokenRule.ConstraintPath);
                    }
                }
            }

            if (_caseElements == null)
            {
                AddBrokenConstraint(SwitchBusinessBusinessConstraint.CaseRequired, ElementName);
                //throw new DefinitionsParsingException("Ошибка: в элементе switch должен быть определен хотя бы один элемент case.");
            }
            else
            {
                //добавляем ломаные правила к родителю
                foreach (Case caseElement in _caseElements)
                {
                    if (!caseElement.IsValid)
                    {
                        foreach (BusinessConstraint brokenRule in caseElement.GetBrokenConstraints())
                        {
                            AddBrokenConstraint(brokenRule, ElementName + "." + RuleConstants.CaseNodeName + "." + brokenRule.ConstraintPath);
                        }
                    }

                    if ((_expression != null) && (_expression.ExpressionType != caseElement.ExpressionType))
                    {
                        AddBrokenConstraint(SwitchBusinessBusinessConstraint.ConditionsTypeMismatch, ElementName);
                        //throw new DefinitionsParsingException("Ошибка: значения элемента " + caseNode.Name + " должны быть одного типа");
                    }
                }
            }

            //добавляем ломаные правила к родителю
            if ((_default != null) && !_default.IsValid)
            {
                foreach (BusinessConstraint brokenRule in _default.GetBrokenConstraints())
                {
                    AddBrokenConstraint(brokenRule, ElementName + "." + RuleConstants.DefaultNodeName + "." + brokenRule.ConstraintPath);
                }
            }
        }

        #endregion
    }
}

