using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    [Serializable]
    public class ItemInt : ItemType, IXmlSerializable
    {
        private int _value = 0;
        private string _stringValue = "";

        public ItemInt() {}

        public ItemInt(int val)
        {
            Value = val;
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
            set
            {
                _value = value;
                _stringValue = value.ToString();
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

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            _stringValue = reader.ReadElementContentAsString();

            int.TryParse(_stringValue, out _value);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(_stringValue);
        }

        #endregion
    }
}
