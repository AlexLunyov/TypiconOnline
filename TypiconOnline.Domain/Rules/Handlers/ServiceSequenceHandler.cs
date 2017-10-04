using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.Rules.Schedule;

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
                typeof(ServiceSequence)
            };
        }

        public override void Execute(ICustomInterpreted element)
        {
            base.Execute(element);

            if (element is TextHolder)
            {
                ExecutingResult.ChildElements.Add(new TextHolderViewModel((element as TextHolder), this));
            }
            else if (element is YmnosStructureRule)
            {
                ExecutingResult.ChildElements.Add(new YmnosStructureViewModel(element as YmnosStructureRule, this));
            }
            else if (element is ServiceSequence)
            {
                ExecutingResult.ChildElements.Add(new ServiceSequenceViewModel(element as ServiceSequence, this));
            }
        }

    }
}
