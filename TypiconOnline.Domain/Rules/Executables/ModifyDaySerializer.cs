using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyDaySerializer : RuleXmlSerializerBase, IRuleSerializer<ModifyDay>
    {
        public ModifyDaySerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.ModifyDayNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new ModifyDay(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            XmlAttribute attr = d.Element.Attributes[RuleConstants.ShortNameAttrName];
            (element as ModifyDay).ShortName = attr?.Value;

            attr = d.Element.Attributes[RuleConstants.IsLastNameAttrName];
            (element as ModifyDay).IsLastName = bool.TryParse(attr?.Value, out bool value) ? value : false;

            attr = d.Element.Attributes[RuleConstants.AsAdditionAttrName];
            (element as ModifyDay).AsAddition = bool.TryParse(attr?.Value, out value) ? value : false;

            attr = d.Element.Attributes[RuleConstants.UseFullNameAttrName];
            (element as ModifyDay).UseFullName = bool.TryParse(attr?.Value, out value) ? value : true;

            attr = d.Element.Attributes[RuleConstants.DayMoveAttrName];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                (element as ModifyDay).DayMoveCount = intValue;
            }

            attr = d.Element.Attributes[RuleConstants.PriorityAttrName];
            (element as ModifyDay).Priority = int.TryParse(attr?.Value, out intValue) ? intValue : 0;

            attr = d.Element.Attributes[RuleConstants.SignNumberAttrName];
            if (int.TryParse(attr?.Value, out intValue))
            {
                (element as ModifyDay).SignNumber = intValue;
            }

            //filter
            DeserializeFilter(d.Element, element as ModifyDay);

            foreach (XmlNode childNode in d.Element.ChildNodes)
            {
                if (childNode.Name == RuleConstants.ModifyReplacedDayNodeName)
                {
                    (element as ModifyDay).ModifyReplacedDay = SerializerRoot.Container<ModifyReplacedDay>()
                        .Deserialize(new XmlDescriptor() { Element = childNode });
                }
                else
                {
                    (element as ModifyDay).ChildDateExp = SerializerRoot.Container<DateExpression>()
                        .Deserialize(new XmlDescriptor() { Element = childNode });
                }
            }
        }

        private void DeserializeFilter(XmlNode element, ModifyDay modifyDay)
        {
            XmlAttribute attr = element.Attributes[RuleConstants.FilterExcludeItemAttr];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                modifyDay.Filter.ExcludedItem = intValue;
            }

            attr = element.Attributes[RuleConstants.FilterIncludeItemAttr];
            if (int.TryParse(attr?.Value, out intValue))
            {
                modifyDay.Filter.IncludedItem = intValue;
            }

            attr = element.Attributes[RuleConstants.FilterIsCelebratingAttr];
            if (bool.TryParse(attr?.Value, out bool boolValue))
            {
                modifyDay.Filter.IsCelebrating = boolValue;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
