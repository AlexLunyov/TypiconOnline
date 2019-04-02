using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TypiconOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class OutputFormsController : Controller
    {
        private readonly IOutputForms _outputForms;
        private readonly IScheduleDayViewer<string> _dayViewer;
        private readonly IScheduleWeekViewer<string> _weekViewer;

        public OutputFormsController(IOutputForms outputForms, IScheduleDayViewer<string> dayViewer, IScheduleWeekViewer<string> weekViewer)
        {
            _outputForms = outputForms ?? throw new ArgumentNullException(nameof(outputForms));
            _dayViewer = dayViewer ?? throw new ArgumentNullException(nameof(dayViewer));
            _weekViewer = weekViewer ?? throw new ArgumentNullException(nameof(weekViewer));
        }

        // GET api/<controller>/
        [Route("")]
        [Route("Index")]
        public string Index()
        {
            return Get(1, new DateTime(2019, 1, 1));
        }

        // GET api/<controller>/get/1/01-01-2019
        [Route("Get/{typiconId}/{date?}/{language?}")]
        public string Get(int typiconId, DateTime date, string language = "cs-ru")
        {
            var result = _outputForms.Get(typiconId, date, language);

            if (result.Success)
            {
                return _dayViewer.Execute(result.Value);
            }
            else
            {
                return result.Error;
            }
        }

        // GET api/<controller>/getweek
        [Route("GetWeek")]
        public string GetWeek()
        {
            return GetWeek(1, DateTime.Now);
        }

        // GET api/<controller>/getweek/1/01-01-2019
        [Route("GetWeek/{typiconId}/{date}/{language?}")]
        public string GetWeek(int typiconId, DateTime date, string language = "cs-ru")
        {
            var result = _outputForms.GetWeek(typiconId, date, language);

            if (result.Success)
            {
                return _weekViewer.Execute(result.Value);
            }
            else
            {
                return result.Error;
            }
        }
    }
}
