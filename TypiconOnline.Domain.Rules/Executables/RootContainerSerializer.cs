using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class RootContainerSerializer: ExecContainerSerializer, IRuleSerializer<RootContainer>
    {
        public RootContainerSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.ExecContainerNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new RootContainer(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            //IAsAdditionElement пропускаем
            //(req.Element as RootContainer).FillElement(req.Descriptor.Element);
        }
    }
}
