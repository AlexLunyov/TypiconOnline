using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetScheduleDayRequest
    {
        public GetScheduleDayRequest()
        {
            ConvertSignToHtmlBinding = false;
            Language = "cs-ru";
        }
        public TypiconEntity TypiconEntity { get; set; }
        public virtual DateTime Date { get; set; }
        
        //TODO: ???? нужен ли? - не нужен при первой возможности УДАЛИТЬ
        public ScheduleHandler Handler { get; set; }
        public HandlingMode Mode { get; set; }
        /// <summary>
        /// Язык службы
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Признак, конвертировать ли номер знака службы в формат отображения в веб-формах
        /// </summary>
        public bool ConvertSignToHtmlBinding { get; set; }

        //TODO: реализовать. Либо можно будет обойтись только RuleHandler ???
        public List<IScheduleCustomParameter> CustomParameters { get; set; }
    }
}
