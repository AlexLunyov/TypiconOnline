using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ExecContainerSerializer : RuleXmlSerializerBase, IRuleSerializer<ExecContainer>
    {
        public ExecContainerSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new [] {
                //RuleConstants.ExecContainerNodeName,
                RuleConstants.ActionNodeName,
                RuleConstants.DefaultNodeName,
                RuleConstants.ThenNodeName,
                RuleConstants.ElseNodeName,
                RuleConstants.WorshipRuleSequenceNode
            };
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new ExecContainer(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            var node = req.Descriptor.Element;
            if (node.HasChildNodes)
            {
                var children = node.ChildNodes.DeserializeChildren(SerializerRoot, req.Parent);

                (req.Element as ExecContainer).ChildElements.AddRange(children);
            }
        }
    }
}
