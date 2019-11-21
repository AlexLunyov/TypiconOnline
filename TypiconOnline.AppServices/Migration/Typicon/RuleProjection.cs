using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public abstract class RuleProjection
    {
        public List<(int VariableId, DefinitionType Type)> VariableLinks { get; set; }

        [XmlIgnore]
        public string RuleDefinition { get; set; }

        [XmlElement("RuleDefinition")]
        public XmlElement RuleDefinitionNode
        {
            get
            {
                return GetElement(RuleDefinition);
            }
            set
            {
                if (value != null)
                {
                    RuleDefinition = value.OuterXml;
                }
            }
        }

        protected XmlElement GetElement(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(val);

                return doc.DocumentElement;
            }

            return default;
        }
    }
}