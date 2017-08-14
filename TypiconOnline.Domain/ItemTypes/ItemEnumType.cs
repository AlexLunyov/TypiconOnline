using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.ItemTypes
{
    /// <summary>
    /// Базовый класс для перечислений в правилах
    /// </summary>
    /// <typeparam name="T">Тип перечислений</typeparam>
    public class ItemEnumType<T> : ItemType where T : struct, IConvertible
    {
        protected T _value;
        private string _stringValue = "";

        private bool _isValid = false;

        public ItemEnumType() { }

        public ItemEnumType(string exp)
        {
            //if (!typeof(T).IsEnum)
            //{
            //    throw new ArgumentException("T must be an enumerated type");
            //}

            _stringValue = exp.ToLower();

            _isValid = Enum.TryParse(exp, true, out _value);
        }

        public T Value
        {
            get
            {
                return _value;
            }
        }

        protected override void Validate()
        {
            if (!_isValid)
            {
                AddBrokenConstraint(ItemEnumTypeBusinessConstraint.EnumTypeMismatch, "ItemEnumType " + typeof(T).ToString());
            }
        }
    }
}
