using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class SignGridModel : IGridModel
    {
        public int Id { get; set; }
        public int TypiconVersionId { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
        public bool IsAddition { get; set; }
        public int? Number { get; set; }
        public int Priority { get; set; }
    }
}
