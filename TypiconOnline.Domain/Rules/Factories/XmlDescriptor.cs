using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Factories
{
    /// <summary>
    /// Читает строку как xml и возвращает имя главного узла
    /// </summary>
    public class XmlDescriptor : IDescriptor<XmlNode>
    {
        string description;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                node = null;
            }
        }

        private XmlNode node;

        public XmlNode Element
        {
            get
            {
                if (node == null)
                {
                    node = new XmlNodeCreator().Create(description);
                }
                return node;
            }
            set
            {
                node = value;
                description = (node != null) ? node.OuterXml : string.Empty;
            }
        }

        public string GetElementName()
        {
            return (Element != null) ? Element.Name : string.Empty;
        }
    }
}
