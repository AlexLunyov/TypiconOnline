using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.WebQuery.Attributes;
using TypiconOnline.Domain.WebQuery.Models;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintDayTemplateEditModel : PrintDayTemplateModelBase
    {
        public string OldFileName { get; set; }
    }
}
