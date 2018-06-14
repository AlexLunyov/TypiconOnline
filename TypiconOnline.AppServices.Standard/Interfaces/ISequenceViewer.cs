using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;

namespace TypiconOnline.AppServices.Interfaces
{
    /// <summary>
    /// Просмотрщик последовательностей - фасад, скрывающий весь алгоритм получения 
    /// выходной формы последовательности богослужения
    /// </summary>
    public interface ISequenceViewer
    {
        GetSequenceResponse GetSequence(GetSequenceRequest request);
    }
}
