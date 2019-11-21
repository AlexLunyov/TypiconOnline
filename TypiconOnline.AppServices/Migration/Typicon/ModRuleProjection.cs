using System.Xml;
using System.Xml.Serialization;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    public abstract class ModRuleProjection: RuleProjection
    {
        [XmlIgnore]
        public string ModRuleDefinition { get; set; }

        [XmlElement("ModRuleDefinition")]
        public XmlElement ModRuleDefinitionNode
        {
            get
            {
                return GetElement(ModRuleDefinition);
            }
            set
            {
                if (value != null)
                {
                    ModRuleDefinition = value.OuterXml;
                }
            }
        }
    }
}