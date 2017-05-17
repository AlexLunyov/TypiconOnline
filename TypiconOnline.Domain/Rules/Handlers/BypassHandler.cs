using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class BypassHandler : IRuleHandler
    {
        public void Execute(ICustomInterpreted element)
        {
            //throw new NotImplementedException();
        }

        public bool IsAuthorized<T>() where T : ICustomInterpreted
        {
            return true;
        }

        public DateTime GetCurrentEaster(int year)
        {
            return new DateTime(year, 4, 25);
        }

        public void Initialize(RuleHandlerRequest request)
        {
            //throw new NotImplementedException();
        }

        public RuleContainer GetResult()
        {
            return null;
            //throw new NotImplementedException();
        }

        public static BypassHandler Instance
        {
            get
            {
                return new BypassHandler();
            }
        }
    }
}
