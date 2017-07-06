using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public abstract class BooleanExpression : RuleExpression
    {
        public BooleanExpression(XmlNode node) : base(node)
        {
        }

        //protected int _outputValue;

        #region Properties

        public override Type ExpressionType
        {
            get
            {
                return typeof(ItemBoolean);
            }
        }

        //public override int OutputValue
        //{
        //    get { return _outputValue; }
        //}

        #endregion
    }
}
