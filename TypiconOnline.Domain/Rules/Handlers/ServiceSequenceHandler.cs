using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Обработчик правил, который выводит последовательность богослужений
    /// </summary>
    public class ServiceSequenceHandler : ScheduleHandler
    {
        public ServiceSequenceHandler()
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(Service),
                //typeof(Notice),
                typeof(TextHolder),
                typeof(YmnosStructureRule),
                typeof(ServiceSequence),
                typeof(Ektenis),
                typeof(CommonRuleElement),
            };
        }

        public override void Execute(ICustomInterpreted element)
        {
            base.Execute(element);
        }

    }
}
