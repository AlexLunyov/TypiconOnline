using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.WebQuery.Attributes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class PrintWeekTemplateModelBase
    {
        public int Id { get; set; }

        public int TypiconVersionId { get; set; }

        public int DaysPerPage { get; set; }

        public IFormFile File { get; set; }
    }
}
