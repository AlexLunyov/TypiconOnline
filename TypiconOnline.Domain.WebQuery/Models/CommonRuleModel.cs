using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class CommonRuleModel : IGridModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
