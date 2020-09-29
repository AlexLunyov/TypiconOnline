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
    public class TriodionDayController : TextBaseController<TriodionDayGridModel>
    {
        public TriodionDayController(IQueryProcessor queryProcessor
            , ICommandProcessor commandProcessor) : base(queryProcessor, commandProcessor)
        {
        }


        // GET: TriodionDay/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">TriodionDayId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var found = QueryProcessor.Process(new TriodionDayEditQuery(id));

            if (found.Success)
            {
                return View(found.Value);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TriodionDayEditModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new EditTriodionDayCommand(model.Id,
                    model.DaysFromEaster,
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
            return View(new TriodionDayEditModel()
            {
                Name = new ItemTextStyled(new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]"))),
                ShortName = new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]"))
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TriodionDayEditModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateTriodionDayCommand(model.DaysFromEaster,
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
                var result = await CommandProcessor.ExecuteAsync(new DeleteTriodionDayCommand(worshipId));

                ClearStoredData(GetQuery());

                return Json(data: result.Success);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override IGridQuery<TriodionDayGridModel> GetQuery() => new AllTriodionDaysQuery(DEFAULT_LANGUAGE);

        protected override Expression<Func<TriodionDayGridModel, bool>> BuildExpression(string searchValue)
        {
            return m => m.Name == searchValue
                    || m.ShortName == searchValue
                    || m.DaysFromEaster.ToString() == searchValue
                    || m.IsCelebrating.ToString() == searchValue;
        }
    }
}