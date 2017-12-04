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
        private RuleHandlerSettings settings = new RuleHandlerSettings() { Date = DateTime.Today };

        public BypassHandler() { }

        public BypassHandler(DateTime date)
        {
            settings = new RuleHandlerSettings() { Date = date };
        }

        public bool Execute(ICustomInterpreted element)
        {
            return true;
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

        public static BypassHandler GetInstance(DateTime date)
        {
            return new BypassHandler(date);
        }

        

        

        public RuleHandlerSettings Settings
        {
            get
            {
                return settings;
            }

            set { }
        }
    }
}
