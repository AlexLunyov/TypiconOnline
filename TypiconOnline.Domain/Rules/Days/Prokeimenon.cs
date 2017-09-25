using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Описание прокимна
    /// </summary>
    [Serializable]
    public class Prokeimenon : ValueObjectBase
    {
        public Prokeimenon() { }

        public Prokeimenon(XmlNode node)// : base(node)
        {
            //глас
            XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            if (ihosAttr != null)
            {
                int result = default(int);
                int.TryParse(ihosAttr.Value, out result);
                Ihos = result;
            }

            XmlAttribute kindAttr = node.Attributes[RuleConstants.ProkeimenonKindAttr];
            if (kindAttr != null)
            {
                ProkiemenonKind kind;

                if (Enum.TryParse(kindAttr.Value, out kind))
                {
                    Kind = kind;
                }
            }

            //стихи
            XmlNodeList stihoiList = node.SelectNodes(RuleConstants.YmnosStihosNode);
            if (stihoiList != null)
            {
                foreach (XmlNode stihosItemNode in stihoiList)
                {
                    Stihoi.Add(new ItemText(stihosItemNode));
                }
            }
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(RuleConstants.YmnosIhosAttrName)]
        public int Ihos { get; set; }

        [XmlAttribute(RuleConstants.ProkeimenonKindAttr)]//(AttributeName = RuleConstants.ProkeimenonKindAttr, Type = typeof(ProkiemenonKind))]
        [DefaultValue(ProkiemenonKind.Prokiemenon)]
        public ProkiemenonKind Kind { get; set; }

        private List<ItemText> _stihoi = new List<ItemText>();
        /// <summary>
        /// Коллекция стихов прокимна
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

        #endregion

        protected override void Validate()
        {
            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, RuleConstants.ProkeimenonNode);
            }

            if (Stihoi == null || Stihoi.Count == 0)
            {
                AddBrokenConstraint(ProkeimenonBusinessConstraint.StihoiRequired);
            }
        }
    }
}
