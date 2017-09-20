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

            //if (element is TextHolder)
            //{
            //    _executingResult.ChildElements.Add(new RenderTextHolder((element as TextHolder), this));
            //}
            //else if (element is YmnosStructureRule)
            //{
            //    _executingResult.ChildElements.Add(new RenderYmnosStructure(element as YmnosStructureRule, this));
            //}
            //else if (element is ServiceSequence)
            //{
            //    _executingResult.ChildElements.Add(new RenderServiceSequence(element as ServiceSequence, this));
            //}
        }

    }
}
