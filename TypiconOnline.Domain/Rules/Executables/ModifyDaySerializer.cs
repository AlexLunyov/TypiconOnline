using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override RuleElement CreateObject(CreateObjectRequest req) 
            => new ModifyDay(req.Descriptor.GetElementName(), req.Parent);

        protected override void FillObject(FillObjectRequest req)
        {
            (req.Element as ModifyDay).ShortName = req.Descriptor.Element.GetItemTextStyled(RuleConstants.ShortNameNodeName);

            var attr = req.Descriptor.Element.Attributes[RuleConstants.IsLastNameAttrName];
            (req.Element as ModifyDay).IsLastName = bool.TryParse(attr?.Value, out bool value) ? value : false;

            attr = req.Descriptor.Element.Attributes[RuleConstants.AsAdditionAttrName];
            (req.Element as ModifyDay).AsAddition = bool.TryParse(attr?.Value, out value) ? value : false;

            attr = req.Descriptor.Element.Attributes[RuleConstants.UseFullNameAttrName];
            (req.Element as ModifyDay).UseFullName = bool.TryParse(attr?.Value, out value) ? value : true;

            attr = req.Descriptor.Element.Attributes[RuleConstants.DayMoveAttrName];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                (req.Element as ModifyDay).DayMoveCount = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.PriorityAttrName];
            if (int.TryParse(attr?.Value, out intValue))
            {
                (req.Element as ModifyDay).Priority = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.SignNumberAttrName];
            if (int.TryParse(attr?.Value, out intValue))
            {
                (req.Element as ModifyDay).SignNumber = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.ModifyDayIdAttrName];
            (req.Element as ModifyDay).Id = attr?.Value;

            //filter
            DeserializeFilter(req.Descriptor.Element, req.Element as ModifyDay);

            //IAsAdditionElement
            attr = req.Descriptor.Element.Attributes[RuleConstants.ModifyDayAsadditionModeAttrName];
            (req.Element as ModifyDay).AsAdditionMode = (Enum.TryParse(attr?.Value, true, out AsAdditionMode mode)) ? mode : AsAdditionMode.None;

            foreach (XmlNode childNode in req.Descriptor.Element.ChildNodes)
            {
                if (childNode.Name == RuleConstants.ModifyReplacedDayNodeName)
                {
                    (req.Element as ModifyDay).ModifyReplacedDay = SerializerRoot.Container<ModifyReplacedDay>()
                        .Deserialize(new XmlDescriptor() { Element = childNode }, req.Parent);
                }
                else
                {
                    (req.Element as ModifyDay).ChildDateExp = SerializerRoot.Container<DateExpression>()
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

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
