using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class CommonRuleEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string RuleDefinition { get; set; }
    }
}
