using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Песнопение
    /// </summary>
    public class Ymnos : RuleElement
    {
        public Ymnos(XmlNode node) : base(node)
        {
            //ymnos
            XmlNode elemNode = node.SelectSingleNode(RuleConstants.YmnosTextNode);
            if (elemNode != null)
            {
                Text = new ItemText(elemNode.OuterXml);
            }

            elemNode = node.SelectSingleNode(RuleConstants.YmnosStihosNode);
            if (elemNode != null)
            {
                Stihos = new ItemText(elemNode.OuterXml);
            }
        }

        /// <summary>
        /// Стих, предваряющий песнопение
        /// </summary>
        public ItemText Stihos { get; set; }
        /// <summary>
        /// Текст песнопения
        /// </summary>
        public ItemText Text { get; set; }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            //ничего
        }

        protected override void Validate()
        {
            if (Text == null || Text.IsEmpty == true)
            {
                AddBrokenConstraint(YmnosBusinessConstraint.TextRequired, ElementName);
            }

            if (Text?.IsValid == false)
            {
                AppendAllBrokenConstraints(Text, ElementName + "." + RuleConstants.YmnosTextNode);
            }

            if (Stihos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Stihos, ElementName + "." + RuleConstants.YmnosStihosNode);
            }
        }
    }
}
