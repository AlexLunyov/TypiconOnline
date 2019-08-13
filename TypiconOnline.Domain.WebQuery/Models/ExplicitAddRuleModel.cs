using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class ExplicitAddRuleModel : IGridModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
    }
}
