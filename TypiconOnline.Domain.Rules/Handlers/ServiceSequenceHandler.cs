using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.ViewModels;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Обработчик правил, который выводит последовательность богослужений
    /// </summary>
    public class ServiceSequenceHandler : ScheduleHandler
    {
        public ServiceSequenceHandler()
        {
            //AuthorizedTypes = new List<Type>()
            //{
            //    typeof(WorshipRule),
            //    //typeof(Notice),
            //    typeof(TextHolder),
            //    typeof(YmnosStructureRule),
            //    typeof(WorshipSequence),
            //    typeof(EktenisRule),
            //    typeof(CommonRuleElement),
            //    typeof(KekragariaRule),
            //    typeof(ApostichaRule),
            //    typeof(AinoiRule),
            //    typeof(TroparionRule),
            //    typeof(KanonasRule)
            //};
            AuthorizedTypes = null;

            ResctrictedTypes = new List<Type>()
            {
                typeof(ModifyDay),
                typeof(ModifyReplacedDay)
            };
        }

        public override bool Execute(ICustomInterpreted element)
        {
            bool result = base.Execute(element);

            if (!result)
            {
                //добавляем элементы, реализующие интерфейс IViewModelElement к последней WorshipRuleViewModel
                if (element is IViewModelElement viewElement)
                {
                    viewElement.CreateViewModel(this, model => GetResult().LastOrDefault()?.ChildElements.AddRange(model));
                }
            }

            return result;
        }

        /// <summary>
        /// Возвращает результат обработки правила
        /// </summary>
        /// <returns></returns>
        public override ICollection<WorshipRuleViewModel> GetResult()
        {
            //TODO: переопределение добавлено только для тестовых целей. Потом удалить
            var model = base.GetResult();

            if (model.Count == 0)
            {
                model.Add(new WorshipRuleViewModel());
            }

            return model;
        }
    }
}
