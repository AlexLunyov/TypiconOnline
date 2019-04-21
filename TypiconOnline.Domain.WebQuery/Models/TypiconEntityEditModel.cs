using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TypiconEntityEditModel
    {
        public int Id { get; set; }
        public ItemText Name { get; set; } = new ItemText();
        public string DefaultLanguage { get; set; }
    }
}
