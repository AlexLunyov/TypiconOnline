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
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Infrastructure.Common.Query;
using JetBrains.Annotations;
using TypiconOnline.Domain.Query.Books;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class DaysFromEaster : Int
    {
        IDataQueryProcessor queryProcessor;

        public DaysFromEaster(string name, [NotNull] IDataQueryProcessor queryProcessor) : base(name)
        {
            this.queryProcessor = queryProcessor;
        }

        public DateExpression ChildExpression { get; set; }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            DateTime easterDate = queryProcessor.Process(new CurrentEasterQuery(handler.Settings.Date.Year));

            ChildExpression.Interpret(handler);

            //DateTime easterDate = handler.GetCurrentEaster(date.Year);

            ValueExpression = ChildExpression.ValueCalculated.Subtract(easterDate).Days;
        }

        protected override void Validate()
        {
            if (ChildExpression == null)
            {
                AddBrokenConstraint(DaysFromEasterBusinessConstraint.DateAbsense, ElementName);
            }
            else
            {
                if (ChildExpression.IsValid)
                {
                    foreach (BusinessConstraint brokenRule in ChildExpression.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenRule, ElementName + "." + RuleConstants.DateNodeName + "." + brokenRule.ConstraintPath);
                    }
                }
            }
        }
    }
}

