using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class NoticeSerializer : WorshipRuleSerializer, IRuleSerializer<Notice>
    {
        public NoticeSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.NoticeNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new Notice(req.Descriptor.GetElementName(), req.Parent);
        }
    }
}
