using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class NoticeSerializer : WorshipRuleSerializer
    {
        public NoticeSerializer(IRuleSerializerRoot unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.NoticeNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new Notice(d.GetElementName());
        }
    }
}
