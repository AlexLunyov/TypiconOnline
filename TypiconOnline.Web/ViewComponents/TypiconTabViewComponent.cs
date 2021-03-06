﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Models.TypiconViewModels;
using TypiconOnline.WebServices.Authorization;

namespace TypiconOnline.Web.ViewComponents
{
    public class TypiconTabViewComponent : ViewComponent
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAuthorizationService _authorizationService;

        public TypiconTabViewComponent(IQueryProcessor queryProcessor
            , IAuthorizationService authorizationService)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<IViewComponentResult> InvokeAsync(string typiconId, TypiconTab tab)
        {
            if (int.TryParse(typiconId, out int id))
            {
                var draft = _queryProcessor.Process(new TypiconDraftVersionQuery(id));

                if (draft.Success)
                {
                    return View(new TypiconTabModel()
                    {
                        TypiconId = id,
                        Tab = tab,
                        Name = draft.Value.Name.FirstOrDefault(CommonConstants.DefaultLanguage).Text,
                        UserIsAuthor = await IsTypiconsAuthor(draft.Value.Typicon),
                        IsTemplate = draft.Value.IsTemplate,
                        VariablesCount = draft.Value.TypiconVariables.Count(c => string.IsNullOrEmpty(c.Value)),
                        EmptyPrintTemplatesCount = draft.Value
                            .PrintDayTemplates
                            .Count(c => c.PrintFile == null || c.PrintFile.Length == 0),
                        ScheduleSettingsExist = draft.Value.ScheduleSettings != null,
                        IsModified = draft.Value.IsModified
                    });
                }
            }

            return View(new TypiconTabModel());
        }

        protected async Task<bool> IsTypiconsAuthor(TypiconEntity typiconEntity)
        {
            var result = await _authorizationService.AuthorizeAsync(
                                                       User as ClaimsPrincipal,
                                                       typiconEntity,
                                                       TypiconOperations.Delete);
            return result.Succeeded;
        }
    }
}
