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
using System.Globalization;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /*
     * EXAMPLE
    *
    * <moreequals>
    *   <int>1</int>
    *   <daysfromeaster>
    *       <date>--24-04</date>
    *   </daysfromeaster>
    * </moreequals>
    */

    /// <summary>
    /// Реализует логическое выражение "БОЛЬШЕ ИЛИ РАВНО".
    /// </summary>
    public class MoreEquals : ComparisonExpression
    {
        public MoreEquals(string name) : base(name) { }

        protected override bool Operate(RuleExpression exp1, RuleExpression exp2, bool? previousValue)
        {
            int compared = ((RuleComparableExpression)exp1).CompareTo((RuleComparableExpression)exp2);

            bool result = (compared <= 0);

            return (previousValue != null) ? (result && (bool)previousValue) : result;
        }
    }
}

