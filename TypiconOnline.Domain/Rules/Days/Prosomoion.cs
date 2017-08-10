using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание подобна
    /// </summary>
    public class Prosomoion : ItemText
    {
        public Prosomoion(Prosomoion source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Prosomoion");
            }

            Self = new ItemBoolean(source.Self.Value);
            Build(source.StringExpression);
        }

        public Prosomoion() : base()
        {
            Self = new ItemBoolean();
        }

        public Prosomoion(XmlNode node)
        {
            //самоподобен?
            XmlAttribute selfAttr = node.Attributes[RuleConstants.ProsomoionSelfAttr];

            Self = (selfAttr != null) ? new ItemBoolean(selfAttr.Value) : new ItemBoolean();

            Build(node.OuterXml);
        }

        /// <summary>
        /// Если true, то самоподобен
        /// </summary>
        public ItemBoolean Self { get; set; }

        protected override void Validate()
        {
            base.Validate();

            if (!Self.IsValid)
            {
                AppendAllBrokenConstraints(Self, RuleConstants.ProsomoionNode + "." + RuleConstants.ProsomoionSelfAttr);
            }
        }

        public override bool Equals(ItemStyledType item)
        {
            bool result = base.Equals(item);

            if (item is Prosomoion)
            {
                result = result && Self.Equals((item as Prosomoion).Self);
            }

            return result;
        }
    }
}
