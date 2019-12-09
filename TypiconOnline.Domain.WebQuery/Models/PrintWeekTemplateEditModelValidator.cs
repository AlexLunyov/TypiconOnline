using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.AppServices.Extensions;
using DocumentFormat.OpenXml;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintWeekTemplateEditModelValidator : PrintWeekTemplateModelValidatorBase<PrintWeekTemplateEditModel>
    {
        public PrintWeekTemplateEditModelValidator(IRuleSerializerRoot ruleSerializer) : base(ruleSerializer) { }

        public override IEnumerable<ValidationResult> Validate(PrintWeekTemplateEditModel model)
            => ValidatePrintWeekTemplate(model);
    }
}
