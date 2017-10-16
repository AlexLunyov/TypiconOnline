using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание блаженн. Используется в структуре Литургии
    /// </summary>
    [Serializable]
    public class Makarismi : DayElementBase
    {
        public Makarismi() { }

        public Makarismi(XmlNode node)
        {
            //TODO: игнорирую реализацию в надежде, что не придется ее использовать
        }

        /// <summary>
        /// Блаженны
        /// </summary>
        [XmlElement(RuleConstants.MakarismiOdiNode)]
        public List<MakariosOdi> Links { get; set; }

        /// <summary>
        /// Непосредственное описание стихов блаженн. Используется в Октоихе
        /// </summary>
        [XmlElement(RuleConstants.MakarismiYmnisNode)]
        public YmnosGroup Ymnis { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}