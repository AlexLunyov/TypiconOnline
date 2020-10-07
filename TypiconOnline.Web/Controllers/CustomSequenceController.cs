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
using TypiconOnline.Domain.Identity;
using TypiconOnline.AppServices.OutputFiltering;

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public class CustomSequenceController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IOutputDayFactory _outputFormFactory;
        private readonly CustomScheduleDataCalculator _dataCalculator;

        public CustomSequenceController(IQueryProcessor queryProcessor, OutputDayFactory outputFormFactory
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
            ViewBag.Typicons = _queryProcessor.GetPublicTypicons();

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
            ViewBag.Typicons = _queryProcessor.GetPublicTypicons();

            var outputModel = new CompleteSequenceViewModel(model);

            if (ModelState.IsValid)
            {
                var typicon = _queryProcessor.Process(new TypiconPublishedVersionQuery(model.Id));

                typicon.OnSuccess(() =>
                {
                    _dataCalculator.CustomRule = model.CustomSequence;

                    //try
                    //{
                    var output = _outputFormFactory.Create(_dataCalculator, new CreateOutputDayRequest()
                    {
                        TypiconId = typicon.Value.TypiconId,
                        TypiconVersionId = typicon.Value.Id,
                        Date = model.Date,
                        HandlingMode = HandlingMode.All
                    });

                    outputModel.Day = output.Day.FilterOut(new OutputFilter() { Language = model.Language });

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
