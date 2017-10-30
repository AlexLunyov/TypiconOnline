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
    public class ItemTextNoted : ItemTextStyled
    {
        public ItemTextNoted() : base() { }

        public ItemTextNoted(string expression) : base(expression) { }

        /// <summary>
        /// Примечание. Например, "трижды" к тропарю или другому тексту
        /// </summary>
        public ItemTextNoted Note { get; set; }

        public override bool IsEmpty
        {
            get
            {
                return base.IsEmpty && Note.IsEmpty;
            }
        }

        protected override bool ReadNode(XmlReader reader)
        {
            bool isRead = base.ReadNode(reader);

            if (!isRead && RuleConstants.ItemTextNoteNode == reader.Name)
            {
                XmlSerializer _serializer = new XmlSerializer(typeof(ItemTextNoted), new XmlRootAttribute(RuleConstants.ItemTextNoteNode));
                Note = _serializer.Deserialize(reader) as ItemTextNoted;

                isRead = true;
            }

            return isRead;
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


        protected override void Validate()
        {
            base.Validate();

            if (Note?.IsValid == false)
            {
                AppendAllBrokenConstraints(Note);
            }
        }

        public override string ToString()
        {
            return (Note != null) ? string.Format($"{base.ToString()} {Note}") : base.ToString();
        }
    }
}
