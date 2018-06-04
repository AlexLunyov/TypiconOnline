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
    public class Ymnos : DayElementBase
    {
        public Ymnos()
        {
            Text = new ItemText();
        }

        public Ymnos(Ymnos source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Ymnos");
            }

            //ElementName = string.Copy(source.ElementName);
            source.Stihoi.ForEach(c => Stihoi.Add(new ItemText(c)));
            Text = new ItemText(source.Text);
            Kind = source.Kind;
            Annotation = (source.Annotation != null) ? new ItemText(source.Annotation) : null;
        }

        /// <summary>
        /// Разновидность песнопения (троичен, богородиен, мученичен...)
        /// </summary>
        [XmlAttribute(XmlConstants.OdiTroparionKindAttr)]
        public YmnosKind Kind { get; set; }
        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        [XmlElement(XmlConstants.AnnotationNode)]
        public ItemText Annotation { get; set; }
        /// <summary>
        /// Стих, предваряющий песнопение
        /// </summary>
        [XmlElement(XmlConstants.YmnosStihosNode)]
        public List<ItemText> Stihoi { get; set; } = new List<ItemText>();
        /// <summary>
        /// Текст песнопения
        /// </summary>
        [XmlElement(XmlConstants.YmnosTextNode)]
        public ItemText Text { get; set; }

        protected override void Validate()
        {
            if (Text == null || Text.IsEmpty == true)
            {
                AddBrokenConstraint(YmnosBusinessConstraint.TextRequired);
            }
            else if (!Text.IsValid)
            {
                AppendAllBrokenConstraints(Text, XmlConstants.YmnosTextNode);
            }

            if (Stihoi != null)
            {
                Stihoi.ForEach(c =>
                {
                    if (!c.IsValid)
                    {
                        AppendAllBrokenConstraints(c, XmlConstants.YmnosStihosNode);
                    }
                });
            }
        }
    }
}
