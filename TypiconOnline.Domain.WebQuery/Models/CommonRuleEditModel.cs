using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class CommonRuleEditModel : RuleModelBase
    {
        //[Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string Name { get; set; }
        public ModelMode Mode { get; set; }
    }
}
