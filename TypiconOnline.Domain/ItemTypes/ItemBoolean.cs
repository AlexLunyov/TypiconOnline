using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemBoolean : ItemType
    {
        private bool _value;
        private string _stringValue = "";

        private bool _isValid = false;
        
        public ItemBoolean() { }

        public ItemBoolean(string exp)
        {
            _stringValue = exp;

            _isValid = bool.TryParse(_stringValue, out _value);
        }

        public ItemBoolean(bool val)
        {
            _value = val;
            _isValid = true;
            _stringValue = _value.ToString();
        }

        public bool Value
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
            if (!IsEmpty && !_isValid)
            {
                AddBrokenConstraint(ItemBooleanBusinessConstraint.BooleanTypeMismatch, "ItemBoolean");
                //throw new DefinitionsParsingException("Ошибка: неверно введена дата в элементе " + node.Name);
            }
        }

        public bool Equals(ItemBoolean item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("ItemBoolean.Equals");
            }

            return (_stringValue == item._stringValue);
        }
    }
}
