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
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class Int : IntExpression
    {
        public Int(string name) : base(name) { }

        public Int(XmlNode valNode) : base(valNode)
        {
            //_outputaValue = 0;

            //_stringValue = valNode.InnerText;

            //int.TryParse(valNode.InnerText, out _outputaValue);

            ValueExpression = new ItemInt(valNode.InnerText);
        }


        public override object ValueExpression
        {
            get
            {
                return base.ValueExpression;
            }
            set
            {
                base.ValueExpression = value;

                ValueCalculated = (value as ItemInt)?.Value;
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            //ничего
        }


        //public override Type OutputType
        //{
        //    get
        //    {
        //        return typeof(int);
        //    }
        //}

        //public override int OutputValue
        //{
        //    get
        //    {
        //        return _value;
        //    }
        //}

        protected override void Validate()
        {
            if ((ValueExpression as ItemInt)?.IsValid == false)
            {
                foreach (BusinessConstraint brokenRule in ((ItemInt)ValueExpression).GetBrokenConstraints())
                {
                    AddBrokenConstraint(brokenRule, ElementName);
                }
            }
        }

        //public virtual bool Equals(RuleExpression IRuleOutputElement)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}

