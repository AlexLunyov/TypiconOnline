using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Песнопение
    /// </summary>
    [Serializable]
    public class Ymnos : ValueObjectBase
    {
        public Ymnos() { }

        public Ymnos(Ymnos source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Ymnos");
            }

            //ElementName = string.Copy(source.ElementName);
            source.Stihoi.ForEach(c => Stihoi.Add(new ItemText(c)));
            Text = new ItemText(source.Text.StringExpression);
        }

        public Ymnos(XmlNode node) //: base(node)
        {
            //ymnos
            XmlNode elemNode = node.SelectSingleNode(RuleConstants.YmnosTextNode);
            Text =  new ItemText((elemNode != null) ? elemNode.OuterXml : string.Empty);

            XmlAttribute kindAttr = node.Attributes[RuleConstants.OdiTroparionKindAttr];
            if (kindAttr != null)
            {
                YmnosKind kind;

                if (Enum.TryParse(kindAttr.Value, out kind))
                {
                    Kind = kind;
                }
            }

            XmlNodeList stihoiList = node.SelectNodes(RuleConstants.YmnosStihosNode);
            if (stihoiList != null)
            {
                foreach (XmlNode stihosItemNode in stihoiList)
                {
                    Stihoi.Add(new ItemText(stihosItemNode));
                }
            }
        }

        /// <summary>
        /// Разновидность песнопения (троичен, богородиен, мученичен...)
        /// </summary>
        [XmlAttribute(RuleConstants.OdiTroparionKindAttr)]
        public YmnosKind Kind { get; set; }
        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        [XmlElement(RuleConstants.AnnotationNode)]
        public ItemText Annotation { get; set; }

        private List<ItemText> _stihoi = new List<ItemText>();
        /// <summary>
        /// Стих, предваряющий песнопение
        /// </summary>
        [XmlElement(RuleConstants.YmnosStihosNode)]
        public List<ItemText> Stihoi
        {
            get
            {
                return _stihoi;
            }
            set
            {
                _stihoi = value;
            }
        }
        /// <summary>
        /// Текст песнопения
        /// </summary>
        [XmlElement(RuleConstants.YmnosTextNode)]
        public ItemText Text { get; set; }

        protected override void Validate()
        {
            if (Text == null || Text.IsEmpty == true)
            {
                AddBrokenConstraint(YmnosBusinessConstraint.TextRequired);
            }
            else if (!Text.IsValid)
            {
                AppendAllBrokenConstraints(Text, RuleConstants.YmnosTextNode);
            }

            if (Stihoi != null)
            {
                Stihoi.ForEach(c =>
                {
                    if (!c.IsValid)
                    {
                        AppendAllBrokenConstraints(c, RuleConstants.YmnosStihosNode);
                    }
                });
            }
        }
    }
}
