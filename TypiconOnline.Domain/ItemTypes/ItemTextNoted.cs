using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemTextNoted : ItemTextHeader
    {
        public ItemTextNoted() : base() { }

        public ItemTextNoted(ITypiconSerializer serializer) : base(serializer) { }

        public ItemTextNoted(string exp) : base(exp) { }

        public ItemTextNoted(string exp, string rootName) : base(exp, rootName) { }

        public ItemTextNoted(ItemText source) : base(source) { }

        [XmlElement("note")]
        public ItemTextStyled Note { get; set; }

        protected override ItemText Deserialize(string exp) => Serializer.Deserialize<ItemTextNoted>(exp, RootName);

        protected override string Serialize() => Serializer.Serialize(this, RootName);

        protected override void Build(ItemText source)
        {
            base.Build(source);

            if (source is ItemTextNoted s)
            {
                Note = s.Note;
            }
        }
    }
}
