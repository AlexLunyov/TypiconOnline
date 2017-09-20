using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TypiconMigrationTool.Experiments.XmlSerialization
{
    [Serializable]
    public class BoolExpression //: IXmlSerializable
    {
        private bool _value;
        private string _stringValue = "";

        private bool _isValid = false;

        public BoolExpression() { }

        public BoolExpression(string exp)
        {
            _stringValue = exp;

            _isValid = bool.TryParse(_stringValue, out _value);
        }

        public BoolExpression(bool val)
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
        [XmlIgnore]
        public bool IsEmpty
        {
            get
            {
                return (_stringValue == "");
            }
        }

        //protected override void Validate()
        //{
        //    if (!IsEmpty && !_isValid)
        //    {
        //        AddBrokenConstraint(ItemBooleanBusinessConstraint.BooleanTypeMismatch, "ItemBoolean");
        //        //throw new DefinitionsParsingException("Ошибка: неверно введена дата в элементе " + node.Name);
        //    }
        //}

        public bool Equals(BoolExpression item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("ItemBoolean.Equals");
            }

            return (_stringValue == item._stringValue);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            //Name = reader.GetAttribute("Name");
            //Boolean isEmptyElement = reader.IsEmptyElement; // (1)
            //reader.ReadStartElement();
            //if (!isEmptyElement) // (1)
            //{
            //    Birthday = DateTime.ParseExact(reader.
            //        ReadElementString("Birthday"), "yyyy-MM-dd", null);
            //    reader.ReadEndElement();
            //}
        }

        public void WriteXml(XmlWriter writer)
        {
            //nothing yet
        }
    }
}
