using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Обработчик правил, который выводит последовательность богослужений
    /// </summary>
    public class ServiceSequenceHandler : ScheduleHandler
    {
        public ServiceSequenceHandler()
        {
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
        public override ICollection<OutputWorship> GetResult()
        {
            //TODO: переопределение добавлено только для тестовых целей. Потом удалить
            var model = base.GetResult();

            if (model.Count == 0)
            {
                model.Add(new OutputWorship());
            }

            return model;
        }
    }
}
