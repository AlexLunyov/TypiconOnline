using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Caching
{
    public class CachingScheduleService : CachingServiceBase, IScheduleService
    {
        const string KEY_DAY = "ScheduleServiceDay";
        const string KEY_WEEK = "ScheduleServiceDay";

        readonly IScheduleService service;

        public CachingScheduleService(IScheduleService service, ICacheStorage cacheStorage, 
            IConfigurationRepository configurationRepository) : base(cacheStorage, configurationRepository)
        {
            this.service = service ?? throw new ArgumentNullException("IScheduleService");
        }

        public GetScheduleDayResponse GetScheduleDay(GetScheduleDayRequest request)
        {
            string key = KEY_DAY + request.Date;
            var value = cacheStorage.Retrieve<ScheduleDay>(key);
            if (value == null)
            {
                var response = service.GetScheduleDay(request);
                value = response.Day;
                Store(key, value);

                return response;
            }
            return new GetScheduleDayResponse() { Day = value };
        }

        public GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request)
        {
            string key = KEY_DAY + request.Date;
            var value = cacheStorage.Retrieve<ScheduleWeek>(key);
            if (value == null)
            {
                var response = service.GetScheduleWeek(request);
                value = response.Week;
                Store(key, value);

                return response;
            }
            return new GetScheduleWeekResponse() { Week = value };
        }
    }
}
