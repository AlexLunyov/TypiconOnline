using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class WorshipSequenceSerializer : ExecContainerSerializer, IRuleSerializer<WorshipSequence>
    {
        public WorshipSequenceSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.MikrosEsperinosNode,
                RuleConstants.MegalisEsperinosNode,
                RuleConstants.EsperinosNode,
                RuleConstants.OrthrosNode,
                RuleConstants.LeitourgiaNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req) => new WorshipSequence(SerializerRoot, req.Descriptor.GetElementName());

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            if (Enum.TryParse(req.Descriptor.GetElementName(), true, out WorshipSequenceKind kind))
            {
                (req.Element as WorshipSequence).Kind = kind;
            }
        }
    }
}
