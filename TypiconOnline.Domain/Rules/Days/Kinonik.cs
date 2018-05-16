using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Причастен
    /// </summary>
    [Serializable]
    public class Kinonik : ItemText
    {
        /// <summary>
        /// Разновидность песнопения (троичен, богородиен, мученичен...)
        /// </summary>
        [XmlAttribute(RuleConstants.OdiTroparionKindAttr)]
        public YmnosKind Kind { get; set; } = YmnosKind.Ymnos;
    }
}
