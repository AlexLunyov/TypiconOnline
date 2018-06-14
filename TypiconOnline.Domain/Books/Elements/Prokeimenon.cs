using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание прокимна
    /// </summary>
    [Serializable]
    public class Prokeimenon : DayElementBase
    {
        public Prokeimenon() { }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(ElementConstants.YmnosIhosAttrName)]
        public int Ihos { get; set; }

        /// <summary>
        /// Разновидность песнопения (троичен, богородиен, мученичен...)
        /// </summary>
        [XmlAttribute(ElementConstants.ProkeimenonKindAttr)]
        public YmnosKind Kind { get; set; } = YmnosKind.Ymnos;

        private List<ItemText> _stihoi = new List<ItemText>();
        /// <summary>
        /// Коллекция стихов прокимна
        /// </summary>
        [XmlElement(ElementConstants.YmnosStihosNode)]
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

        #endregion

        protected override void Validate()
        {
            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, ElementConstants.ProkeimenonNode);
            }

            if (Stihoi == null || Stihoi.Count == 0)
            {
                AddBrokenConstraint(ProkeimenonBusinessConstraint.StihoiRequired);
            }
        }
    }
}
