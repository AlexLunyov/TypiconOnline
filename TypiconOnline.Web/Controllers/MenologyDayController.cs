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
    public class MenologyDayController : TextBaseController<MenologyDayGridModel>
    {
        public MenologyDayController(IQueryProcessor queryProcessor
            , ICommandProcessor commandProcessor) : base(queryProcessor, commandProcessor)
        {
        }


        // GET: MenologyDay/Edit/5
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

            var found = QueryProcessor.Process(new MenologyDayEditQuery(id));

            if (found.Success)
            {
                return View(found.Value);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenologyDayEditModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new EditMenologyDayCommand(model.Id,
                    model.LeapDate,
                    model.Name,
                    model.ShortName,
                    model.IsCelebrating,
                    model.UseFullName,
                    model.Definition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new MenologyDayEditModel()
            {
                //Name = new ItemTextStyled(new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]"))),
                //ShortName = new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]"))
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenologyDayEditModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateMenologyDayCommand(model.LeapDate,
                    model.Name,
                    model.ShortName,
                    model.IsCelebrating,
                    model.UseFullName,
                    model.Definition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [Route("[controller]/[action]/{worshipId}")]
        public async Task<IActionResult> Delete(int worshipId)
        {
            try
            {
                //удаление
                var result = await CommandProcessor.ExecuteAsync(new DeleteMenologyDayCommand(worshipId));

                ClearStoredData(GetQuery());

                return Json(data: result.Success);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override IGridQuery<MenologyDayGridModel> GetQuery() => new AllMenologyDaysQuery(DEFAULT_LANGUAGE);

        protected override Func<MenologyDayGridModel, string, bool> BuildExpression 
            => (m, searchValue) 
                    => m.Name == searchValue
                    || m.ShortName == searchValue
                    || m.Date == searchValue
                    || m.LeapDate == searchValue
                    || m.IsCelebrating.ToString() == searchValue;
    }
}