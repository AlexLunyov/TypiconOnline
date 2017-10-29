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

        protected override XmlDocument ComposeXml()
        {
            XmlDocument doc = base.ComposeXml();

            if (Note != null)
            {
                XmlDocument noteDoc = Note.ComposeXml();

                XmlNode node = doc.ImportNode(noteDoc.DocumentElement, true);
                doc.DocumentElement.AppendChild(node);
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

        public override string ToString()
        {
            return (Note != null) ? string.Format($"{base.ToString()} {Note}") : base.ToString();
        }
    }
}
