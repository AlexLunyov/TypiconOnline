using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyDaySerializer : RuleXmlSerializerBase, IRuleSerializer<ModifyDay>
    {
        public ModifyDaySerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.ModifyDayNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req) 
            => new ModifyDay(req.Descriptor.GetElementName(), req.Parent);

        protected override void FillObject(FillObjectRequest req)
        {
            var obj = req.Element as ModifyDay;

            obj.ShortName = req.Descriptor.Element.GetItemTextStyled(SerializerRoot.TypiconSerializer, RuleConstants.ShortNameNodeName);

            var attr = req.Descriptor.Element.Attributes[RuleConstants.IsLastNameAttrName];
            obj.IsLastName = bool.TryParse(attr?.Value, out bool value) ? value : false;

            attr = req.Descriptor.Element.Attributes[RuleConstants.AsAdditionAttrName];
            obj.AsAddition = bool.TryParse(attr?.Value, out value) ? value : false;

            attr = req.Descriptor.Element.Attributes[RuleConstants.UseFullNameAttrName];
            obj.UseFullName = bool.TryParse(attr?.Value, out value) ? value : true;

            attr = req.Descriptor.Element.Attributes[RuleConstants.DayMoveAttrName];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                obj.DayMoveCount = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.PriorityAttrName];
            if (int.TryParse(attr?.Value, out intValue))
            {
                obj.Priority = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.SignNumberAttrName];
            if (int.TryParse(attr?.Value, out intValue))
            {
                obj.SignNumber = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.ModifyDayIdAttrName];
            obj.Id = attr?.Value;

            //filter
            DeserializeFilter(req.Descriptor.Element, req.Element as ModifyDay);

            //IAsAdditionElement
            attr = req.Descriptor.Element.Attributes[RuleConstants.ModifyDayAsadditionModeAttrName];
            obj.AsAdditionMode = (Enum.TryParse(attr?.Value, true, out AsAdditionMode mode)) ? mode : AsAdditionMode.None;

            foreach (XmlNode childNode in req.Descriptor.Element.ChildNodes)
            {
                if (childNode.Name == RuleConstants.ModifyReplacedDayNodeName)
                {
                    obj.ModifyReplacedDay = SerializerRoot.Container<ModifyReplacedDay>()
                        .Deserialize(new XmlDescriptor() { Element = childNode }, req.Parent);
                }
                else
                {
                    obj.ChildDateExp = SerializerRoot.Container<DateExpression>()
                        .Deserialize(new XmlDescriptor() { Element = childNode }, req.Parent);
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

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
