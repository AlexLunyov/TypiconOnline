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
    /// Базовый класс для логических выражений сравнения.
    /// Ограничения на дочерние элементы: должны быть целочисленными
    /// </summary>
    public abstract class ComparisonExpression : LogicalExpression
    {
        public ComparisonExpression(string name) : base(name) { }
        public ComparisonExpression(XmlNode valNode) : base(valNode) { }

        protected override void Validate()
        {
            base.Validate();

            foreach (RuleExpression exp in ChildElements)
            {
                //Проверяем, элемент с каким выходным типом значения
                //Должен быть только целочисленным
                if ((exp != null) && !(exp is IntExpression))
                {
                    AddBrokenConstraint(ComparisonExpressionBusinessConstraint.TypeMismatch, ElementName);
                }
            }
        }
    }
}

