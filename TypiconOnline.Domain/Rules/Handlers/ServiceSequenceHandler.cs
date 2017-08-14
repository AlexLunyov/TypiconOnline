using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class ServiceSequenceHandler : RuleHandlerBase
    {
        public ServiceSequenceHandler()
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(Service),
                //typeof(Notice),
                typeof(TextHolder)
            };
        }

        public override RuleHandlerSettings Settings
        {
            set
            {
                base.Settings = value;
            }
        }

        public override void Execute(ICustomInterpreted element)
        {
            if (element is TextHolder)
            {
                
                _executingResult.ChildElements.Add(new TextHolder(element as TextHolder));
            }
        }

        //public override RuleContainer GetResult()
        //{
        //    return _executingResult;
        //}
    }
}
