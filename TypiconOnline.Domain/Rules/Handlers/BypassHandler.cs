using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ViewModels;

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

        public bool IsTypeAuthorized(ICustomInterpreted t)
        {
            return true;
        }

        public static BypassHandler Instance
        {
            get
            {
                return new BypassHandler();
            }
        }

        public RuleHandlerSettings Settings
        {
            get
            {
                return null;
            }

            set { }
        }
    }
}
