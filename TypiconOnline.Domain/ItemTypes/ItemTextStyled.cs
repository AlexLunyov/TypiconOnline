﻿using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.ItemTypes
{
    [Serializable]
    public class ItemTextStyled : ItemText
    {
        public ItemTextStyled() : base() { }

        //public ItemTextStyled(ITypiconSerializer serializer) : base(serializer) { }

        //public ItemTextStyled(string exp) : base(exp) { }

        //public ItemTextStyled(string exp, string rootName) : base(exp, rootName) { }
        public ItemTextStyled(params ItemTextUnit[] items) : base(items) { }
        public ItemTextStyled(ItemText source) : base(source) { }

        public ItemTextStyled(ItemTextStyled source) : base(source)
        {
            IsBold = source.IsBold;
            IsItalic = source.IsItalic;
            IsRed = source.IsRed;
        }

        [XmlAttribute("bold")]
        public bool IsBold { get; set; }
        [XmlAttribute("italic")]
        public bool IsItalic { get; set; }
        [XmlAttribute("red")]
        public bool IsRed { get; set; }

        //protected override ItemText Deserialize(string exp) => Serializer.Deserialize<ItemTextStyled>(exp, RootName);

        //protected override string Serialize() => Serializer.Serialize(this);//, RootName);

        protected override void Build(ItemText source)
        {
            base.Build(source);

            if (source is ItemTextStyled s)
            {
                IsBold = s.IsBold;
                IsItalic = s.IsItalic;
                IsRed = s.IsRed;
            }
        }
    }
}
