using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintDayTemplateGridModel : IGridModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool HasFile { get; set; }
        public string Name { get; set; }
    }
}
