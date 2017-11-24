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
        public IntExpression(string name) : base(name) { }
        public IntExpression(XmlNode node) : base(node) { }

        public override Type ExpressionType => typeof(int);
    }
}
