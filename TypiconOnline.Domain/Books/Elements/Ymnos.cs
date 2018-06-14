using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
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
        [XmlAttribute(ElementConstants.OdiTroparionKindAttr)]
        public YmnosKind Kind { get; set; }
        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        [XmlElement(ElementConstants.AnnotationNode)]
        public ItemText Annotation { get; set; }
        /// <summary>
        /// Стих, предваряющий песнопение
        /// </summary>
        [XmlElement(ElementConstants.YmnosStihosNode)]
        public List<ItemText> Stihoi { get; set; } = new List<ItemText>();
        /// <summary>
        /// Текст песнопения
        /// </summary>
        [XmlElement(ElementConstants.YmnosTextNode)]
        public ItemText Text { get; set; }

        protected override void Validate()
        {
            if (Text == null || Text.IsEmpty == true)
            {
                AddBrokenConstraint(YmnosBusinessConstraint.TextRequired);
            }
            else if (!Text.IsValid)
            {
                AppendAllBrokenConstraints(Text, ElementConstants.YmnosTextNode);
            }

            if (Stihoi != null)
            {
                Stihoi.ForEach(c =>
                {
                    if (!c.IsValid)
                    {
                        AppendAllBrokenConstraints(c, ElementConstants.YmnosStihosNode);
                    }
                });
            }
        }
    }
}
