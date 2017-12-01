using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetScheduleDayRequest: ServiceRequestBase
    {
        public GetScheduleDayRequest()
        {
            ConvertSignToHtmlBinding = false;
            Language = "cs-ru";
            ThrowExceptionIfInvalid = false;
        }
        //public int TypiconId { get; set; }
        public TypiconEntity Typicon { get; set; }
        public virtual DateTime Date { get; set; }
        
        //TODO: ???? нужен ли? - не нужен при первой возможности УДАЛИТЬ
        public ScheduleHandler Handler { get; set; }
        /// <summary>
        /// Язык службы
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Признак, конвертировать ли номер знака службы в формат отображения в веб-формах
        /// </summary>
        public bool ConvertSignToHtmlBinding { get; set; }
        /// <summary>
        /// Признак, генерировать ли исключение в случае неверного составления правила при его обработке
        /// </summary>
        public bool ThrowExceptionIfInvalid { get; set; }

        //TODO: реализовать. Либо можно будет обойтись только RuleHandler ???
        public CustomParamsCollection<IRuleApplyParameter> ApplyParameters { get; set; } = new CustomParamsCollection<IRuleApplyParameter>();
        public CustomParamsCollection<IRuleCheckParameter> CheckParameters { get; set; } = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.All);
    }
}
