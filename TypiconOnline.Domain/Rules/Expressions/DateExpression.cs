using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Expressions
{
    
    public abstract class DateExpression : RuleExpression
    {
        public DateExpression(string name) : base(name) { }

        public DateExpression(XmlNode node) : base(node) { }

        //protected DateTime _outputDate = DateTime.MinValue;

        #region Properties

        public override Type ExpressionType
        {
            get
            {
                return typeof(DateTime);
            }
        }
        

        //public override DateTime ValueCalculated
        //{
        //    get
        //    {
        //        return _outputDate;
        //    }
        //}

        #endregion
    }
}
