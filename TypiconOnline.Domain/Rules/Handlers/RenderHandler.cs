using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public abstract class RenderHandler : IRuleHandler
    {
        public RuleHandlerSettings Settings
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Execute(ICustomInterpreted element)
        {
            throw new NotImplementedException();
        }

        public virtual ContainerViewModel GetResult()
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorized<T>() where T : ICustomInterpreted
        {
            throw new NotImplementedException();
        }
    }
}
