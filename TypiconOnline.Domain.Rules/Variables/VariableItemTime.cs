using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Variables
{
    public class VariableItemTime : Variable<ItemTime>
    {
        public VariableItemTime(string value) : base(value) { }

        protected override ItemTime Initiate(string value) => new ItemTime(value ?? string.Empty);
    }
}
