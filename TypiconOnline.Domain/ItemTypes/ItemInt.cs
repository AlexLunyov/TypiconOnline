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

        public ItemInt() {}

        public ItemInt(int val)
        {
            _value = val;
            _stringValue = val.ToString();
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

        public bool IsEmpty
        {
            get
            {
                return (_stringValue == "");
            }
        }


        protected override void Validate()
        {
            if (!IsEmpty && !int.TryParse(_stringValue, out _value))
            {
                AddBrokenConstraint(ItemIntBusinessConstraint.IntTypeMismatch, "ItemInt");
                //throw new DefinitionsParsingException("Ошибка: неверно введена дата в элементе " + node.Name);
            }
        }

        public bool Equals(ItemInt itemInt)
        {
            if (itemInt == null)
            {
                throw new ArgumentNullException("ItemInt.Equals");
            }

            return (_stringValue == itemInt._stringValue);
        }
    }
}
