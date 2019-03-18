using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    public class ScheduleOutputFormService : IScheduleService
    {
        private readonly TypiconDBContext _dbContext;
        private readonly IScheduleDayNameComposer _nameComposer;
        private readonly ITypiconSerializer _serializer;

        public ScheduleOutputFormService(TypiconDBContext dbContext
            , IScheduleDayNameComposer nameComposer
            , ITypiconSerializer serializer)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _nameComposer = nameComposer ?? throw new ArgumentNullException(nameof(nameComposer));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public GetScheduleDayResponse GetScheduleDay(GetScheduleDayRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var outputForm = _dbContext.Set<OutputForm>().FirstOrDefault(c => c.TypiconId == request.TypiconId && c.Date == request.Date);

            GetScheduleDayResponse response = new GetScheduleDayResponse();

            if (outputForm != null)
            {
                response.Day = _serializer.Deserialize<ScheduleDay>(outputForm.Definition);
            }
            else
            {
                //надо запускать формирование выходных форм
                //пока так
                response.Exception = new NullReferenceException("Выходная форма не найдена.");
            }

            return response;
        }

        public GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ScheduleWeek week = new ScheduleWeek()
            {
                Name = _nameComposer.GetWeekName(request.Date, request.Language)
            };

            GetScheduleDayRequest dayRequest = new GetScheduleDayRequest()
            {
                Date = request.Date,
                TypiconId = request.TypiconId
            };

            int i = 0;

            while (i < 7)
            {
                GetScheduleDayResponse dayResponse = GetScheduleDay(dayRequest);
                week.Days.Add(dayResponse.Day);
                dayRequest.Date = dayRequest.Date.AddDays(1);
                i++;
            }

            return new GetScheduleWeekResponse() { Week = week };
        }
    }
}
