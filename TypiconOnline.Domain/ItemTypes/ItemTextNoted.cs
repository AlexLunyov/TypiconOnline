using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemTextNoted : ItemText
    {
        public ItemTextNoted() : base() { }

        public ItemTextNoted(string expression) : base(expression) { }

        /// <summary>
        /// Примечание. Например, "трижды" к тропарю или другому тексту
        /// </summary>
        public ItemTextNoted Note { get; set; }

        protected override XmlDocument ComposeXml()
        {
            XmlDocument doc = base.ComposeXml();

            if (Note != null)
            {
                XmlDocument noteDoc = Note.ComposeXml();

                XmlNode node = doc.CreateElement(RuleConstants.ItemTextNoteNode);

                node.InnerText = noteDoc.FirstChild.InnerXml;

                doc.FirstChild.AppendChild(node);
            }

            return doc;
        }

        protected override void BuildFromXml(XmlNode node)
        {
            base.BuildFromXml(node);

            XmlNode noteNode = node.SelectSingleNode(RuleConstants.ItemTextNoteNode);

            if (noteNode != null)
            {
                Note = new ItemTextNoted(noteNode.OuterXml);
            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (Note?.IsValid == false)
            {
                AppendAllBrokenConstraints(Note);
            }
        }

        public override void ReadXml(XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;

            reader.MoveToElement();
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.MoveToContent();

                string name = reader.Name;

                switch (name)
                {
                    case RuleConstants.ItemTextItemNode:
                        string language = reader.GetAttribute(RuleConstants.ItemTextLanguageAttr);
                        if (IsKeyValid(language))
                        {
                            string value = reader.ReadElementContentAsString();
                            AddElement(language, value);
                        }
                        //reader.MoveToElement();
                        break;
                    case RuleConstants.StyleNodeName:
                        XmlSerializer _serializer = new XmlSerializer(typeof(TextStyle), new XmlRootAttribute(RuleConstants.StyleNodeName));
                        Style = _serializer.Deserialize(reader) as TextStyle;
                        break;
                    case RuleConstants.ItemTextNoteNode:
                        _serializer = new XmlSerializer(typeof(ItemTextNoted), new XmlRootAttribute(RuleConstants.ItemTextNoteNode));
                        Note = _serializer.Deserialize(reader) as ItemTextNoted;
                        break;
                        //default:
                        //    reader.Read();
                        //    break;
                }
            }

            reader.Read();
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);

            if (Note != null && !Note.IsEmpty)
            {
                XmlSerializer _serializer = new XmlSerializer(typeof(ItemTextNoted), new XmlRootAttribute(RuleConstants.ItemTextNoteNode));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                _serializer.Serialize(writer, Note, ns);
            }
        }

        public override string ToString()
        {
            string result = base.ToString();

            if (Note != null)
            {
                result = string.Format("{0} {1}", result, Note);
            }

            return result;
        }
    }
}
