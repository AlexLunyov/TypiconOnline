using System.Xml;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// Читает строку как xml и возвращает имя главного узла
    /// </summary>
    public class XmlDescriptor : IDescriptor<XmlNode>
    {
        string description;

        XmlNodeCreator creator = new XmlNodeCreator();

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                node = creator.Create(description);
            }
        }

        private XmlNode node;

        public XmlNode Element
        {
            get
            {
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

        public IDescriptor CreateInstance(string description) => new XmlDescriptor() { Description = description };
    }
}
