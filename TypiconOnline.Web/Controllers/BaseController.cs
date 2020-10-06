using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Web.Controllers
{
    public abstract class BaseController: Controller
    {
        public BaseController(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor)
        {
            QueryProcessor = queryProcessor;
            CommandProcessor = commandProcessor;
        }

        protected IQueryProcessor QueryProcessor { get; }
        protected ICommandProcessor CommandProcessor { get; }

        protected IActionResult Perform(Func<Result> action, Func<IActionResult> result)
        {
            var r = action();

            if (r.Success)
            {
                return result();
            }
            else
            {
                return Fail(r.ErrorCode);
            }
        }

        protected IActionResult Perform<T>(Func<Result<T>> action, Func<Result<T>, IActionResult> result)
        {
            var r = action();

            if (r.Success)
            {
                return result(r);
            }
            else
            {
                return Fail(r.ErrorCode);
            }
        }

        protected IActionResult Perform(Func<Result> action
            , Func<IActionResult> success
            , Func<IActionResult> fail)
        {
            var r = action();

            if (r.Success)
            {
                return success();
            }
            else
            {
                return fail();
            }
        }

        protected IActionResult Fail(int errCode)
        {
            switch (errCode)
            {
                case 403:
                    return Unauthorized();
                case 404:
                    return NotFound();
                default:
                    return BadRequest();
            }
        }
    }
}
