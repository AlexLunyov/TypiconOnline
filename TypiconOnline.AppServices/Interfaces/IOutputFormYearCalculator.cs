using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOutputFormYearCalculator
    {
        /// <summary>
        /// Совершает вычисление выходных форм для опубликованной версии Устава для указанного года
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Сообщение о выполнении задачи</returns>
        Task<Result> HandleAsync(OutputFormYearCalculatorRequest request);

        //Result Handle(OutputFormYearCalculatorRequest request);
    }
}
