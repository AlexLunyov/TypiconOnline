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
    * <equals>
    *   <int>1</int>
    *   <daysfromeaster>
    *       <date>--24-04</date>
    *   </daysfromeaster>
    * </equals>
    */

    /// <summary>
    /// Реализует логическое выражение "РАВНО"
    /// </summary>
    public class Equals : LogicalExpression
    {
        public Equals(XmlNode valNode) : base(valNode)
        {
        }

        protected override bool Operate(RuleExpression exp1, RuleExpression exp2, bool? previousValue)
        {
            bool result = exp1.ValueCalculated.Equals(exp2.ValueCalculated);

            return (previousValue != null) ? (result && (bool)previousValue) : result;
        }
    }
}

