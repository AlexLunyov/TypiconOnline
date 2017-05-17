using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemInt : ItemType
    {
        private int _value = 0;
        private string _stringValue = "";

        private ItemInt() {}

        public ItemInt(int val)
        {
            _value = val;
        }

        public ItemInt(string exp)
        {
            _stringValue = exp;

            int.TryParse(_stringValue, out _value);
        }

        public int Value
        {
            get
            {
                return _value;
            }
        }


        protected override void Validate()
        {
            if (!int.TryParse(_stringValue, out _value))
            {
                AddBrokenConstraint(ItemIntBusinessConstraint.IntTypeMismatch, "ItemInt");
                //throw new DefinitionsParsingException("Ошибка: неверно введена дата в элементе " + node.Name);
            }
        }
    }
}
