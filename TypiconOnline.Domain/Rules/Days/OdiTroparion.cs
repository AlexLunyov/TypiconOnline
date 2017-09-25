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
    /// Тропарь канона
    /// </summary>
    public class OdiTroparion: ItemText
    {
        public OdiTroparion() { }

        public OdiTroparion(XmlNode node) : base(node)
        {
            //kind
            XmlAttribute kindAttr = node.Attributes[RuleConstants.OdiTroparionKindAttr];

            if (kindAttr != null)
            {
                switch (kindAttr.Value)
                {
                    case RuleConstants.TheotokionKindAttrValue:
                        Kind = OdiTroparionKind.Theotokion;
                        break;
                    case RuleConstants.TriadikoKindAttrValue:
                        Kind = OdiTroparionKind.Triadiko;
                        break;
                }
            }
            
        }

        public OdiTroparionKind Kind { get; set; }
    }

    /// <summary>
    /// Тип тропаря: не определен, Богородичен, Троичен
    /// </summary>
    public enum OdiTroparionKind { Undefined = 0, Theotokion = 1, Triadiko = 2 }
}
