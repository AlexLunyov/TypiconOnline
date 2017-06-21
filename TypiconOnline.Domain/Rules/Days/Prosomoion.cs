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
        /// <summary>
        /// Если true, то самоподобен
        /// </summary>
        public ItemBoolean Self { get; set; }

        public Prosomoion(XmlNode node)
        {
            //самоподобен?
            XmlAttribute selfAttr = node.Attributes[RuleConstants.ProsomoionSelfAttr];

            Self = (selfAttr != null) ? new ItemBoolean(selfAttr.Value) : new ItemBoolean();

            Build(node.OuterXml);
        }

        protected override void Validate()
        {
            base.Validate();

            if (!Self.IsValid)
            {
                AppendAllBrokenConstraints(Self, RuleConstants.ProsomoionNode + "." + RuleConstants.ProsomoionSelfAttr);
            }
        }
    }
}
