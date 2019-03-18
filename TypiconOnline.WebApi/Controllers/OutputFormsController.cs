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

        public OutputFormsController(IOutputForms outputForms, IScheduleDayViewer<string> dayViewer)
        {
            _outputForms = outputForms ?? throw new ArgumentNullException(nameof(outputForms));
            _dayViewer = dayViewer ?? throw new ArgumentNullException(nameof(dayViewer));
        }

        // GET api/<controller>/
        [HttpGet("")]
        public string Index()
        {
            return Get(1, new DateTime(2019, 1, 1));
        }

        // GET api/<controller>/get?typiconId=1&date=01-01-2019
        [HttpGet("{id}")]
        public string Get(int typiconId, DateTime date)
        {
            var result = _outputForms.Get(typiconId, date);

            if (result.Success)
            {
                string str = _dayViewer.Execute(result.Value);
                return str;
            }
            else
            {
                return result.Error;
            }
        }

    }
}
