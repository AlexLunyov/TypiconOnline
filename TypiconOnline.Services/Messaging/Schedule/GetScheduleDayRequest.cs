using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetScheduleDayRequest
    {
        public GetScheduleDayRequest()
        {
            ConvertSignToHtmlBinding = false;
        }
        public TypiconEntity TypiconEntity { get; set; }
        public virtual DateTime Date { get; set; }
        
        //TODO: ???? нужен ли?
        public IRuleHandler RuleHandler { get; set; }
        public HandlingMode Mode { get; set; }
        /// <summary>
        /// Признак, конвертировать ли номер знака службы в формат отображения в веб-формах
        /// </summary>
        public bool ConvertSignToHtmlBinding { get; set; }

        //TODO: реализовать. Либо можно будет обойтись только RuleHandler ???
        public List<GetScheduleRequestParameter> CustomParameters { get; set; }
    }
}
