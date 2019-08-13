using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class ExplicitAddRuleEditModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string RuleDefinition { get; set; }
    }
}
