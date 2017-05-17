using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public abstract class IntExpression : RuleExpression
    {
        public IntExpression(XmlNode node) : base(node)
        {
        }

        //protected int _outputValue;

        #region Properties

        public override Type ExpressionType
        {
            get
            {
                return typeof(ItemInt);
            }
        }

        //public override int OutputValue
        //{
        //    get { return _outputValue; }
        //}

        #endregion
    }
}
