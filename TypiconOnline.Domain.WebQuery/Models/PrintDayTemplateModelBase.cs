using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class PrintDayTemplateModelBase
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
