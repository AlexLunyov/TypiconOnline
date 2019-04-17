using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Models.CustomSequenceModels;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Web.Extensions;

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class CustomSequenceController : Controller
    {
        private readonly IDataQueryProcessor _queryProcessor;
        private readonly IOutputFormFactory _outputFormFactory;
        private readonly CustomScheduleDataCalculator _dataCalculator;

        public CustomSequenceController(IDataQueryProcessor queryProcessor, IOutputFormFactory outputFormFactory
            , CustomScheduleDataCalculator dataCalculator)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _outputFormFactory = outputFormFactory ?? throw new ArgumentNullException(nameof(outputFormFactory));
            _dataCalculator = dataCalculator ?? throw new ArgumentNullException(nameof(dataCalculator));
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(CompleteSequenceViewModel model)
        {
            ViewBag.Typicons = _queryProcessor.GetTypicons();

            if (model == null)
            {
                model = new CompleteSequenceViewModel();
            }

            return View(model);
        }

        //[Route("{id:int}/{date}/{language?}")]
        [HttpPost]
        public IActionResult Index(GetCustomSequenceViewModel model)
        {
            ViewBag.Typicons = _queryProcessor.GetTypicons();

            var outputModel = new CompleteSequenceViewModel(model);

            if (ModelState.IsValid)
            {
                var typicon = _queryProcessor.Process(new TypiconPublishedVersionQuery(model.Id));

                typicon.OnSuccess(() =>
                {
                    _dataCalculator.CustomRule = model.CustomSequence;

                    //try
                    //{
                    var output = _outputFormFactory.Create(_dataCalculator, new CreateOutputFormRequest()
                    {
                        TypiconId = typicon.Value.TypiconId,
                        TypiconVersionId = typicon.Value.Id,
                        Date = model.Date,
                        HandlingMode = HandlingMode.All
                    });

                    outputModel.Day = output.Day.Localize(model.Language);

                    outputModel.StatusMessage = GetMessage(output.BrokenConstraints);
                    //}
                    //catch (Exception ex)
                    //{
                    //    outputModel.StatusMessage = ex.Message;
                    //}

                })
                .OnFailure(err =>
                {
                    outputModel.StatusMessage = err;
                });
            }
                

            return View(outputModel);
        }

        private string GetMessage(IEnumerable<BusinessConstraint> constraints)
        {
            return string.Join(Environment.NewLine, constraints.Select(c => c.ConstraintFullDescription));
        }
    }
}
