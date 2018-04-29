using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemTextHeader : ItemTextStyled
    {
        public ItemTextHeader() : base() { }

        public ItemTextHeader(ITypiconSerializer serializer) : base(serializer) { }

        public ItemTextHeader(string exp) : base(exp) { }

        public ItemTextHeader(string exp, string rootName) : base(exp, rootName) { }

        public ItemTextHeader(ItemText source) : base(source) { }

        [XmlAttribute("header")]
        public HeaderCaption Header { get; set; } = HeaderCaption.NotDefined;

        protected override ItemText Deserialize(string exp) => Serializer.Deserialize<ItemTextHeader>(exp, RootName);

        protected override string Serialize() => Serializer.Serialize(this, RootName);

        protected override void Build(ItemText source)
        {
            base.Build(source);

            if (source is ItemTextHeader s)
            {
                Header = s.Header;
            }
        }
    }
}
