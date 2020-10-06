using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypiconOnline.Domain.Command.Books;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.WebQuery.Books;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = RoleConstants.AdminAndTypesetterRoles)]
    public class OktoikhDayController : TextBaseController<OktoikhDayGridModel>
    {
        public OktoikhDayController(IQueryProcessor queryProcessor
            , ICommandProcessor commandProcessor) : base(queryProcessor, commandProcessor)
        {
        }


        // GET: OktoikhDay/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">MenologyRuleId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var found = QueryProcessor.Process(new OktoikhDayEditQuery(id));

            if (found.Success)
            {
                return View(found.Value);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OktoikhDayEditModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new EditOktoikhDayCommand(model.Id,
                    //model.Ihos,
                    //model.DayOfWeek,
                    model.Definition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View(new OktoikhDayEditModel());
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(OktoikhDayEditModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var command = new CreateOktoikhDayCommand(model.Ihos,
        //            model.DayOfWeek,
        //            model.Definition);

        //        await CommandProcessor.ExecuteAsync(command);

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //[Route("[controller]/[action]/{worshipId}")]
        //public async Task<IActionResult> Delete(int worshipId)
        //{
        //    try
        //    {
        //        //удаление
        //        var result = await CommandProcessor.ExecuteAsync(new DeleteOktoikhDayCommand(worshipId));

        //        ClearStoredData(GetQuery());

        //        return Json(data: result.Success);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        protected override IGridQuery<OktoikhDayGridModel> GetQuery() => new AllOktoikhDaysQuery();

        protected override Func<OktoikhDayGridModel, string, bool> BuildExpression
            => (m, searchValue)
                => m.Ihos.ToString() == searchValue
                || m.DayOfWeek == searchValue;
    }
}