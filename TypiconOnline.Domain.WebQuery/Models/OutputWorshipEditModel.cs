using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class OutputWorshipEditModel
    {
        public int Id { get; set; }

        public string Time { get; set; }
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        public virtual string Name { get; set; }
        public virtual TextStyle NameStyle { get; set; } = new TextStyle();
        public virtual string AdditionalName { get; set; }
        public virtual TextStyle AdditionalNameStyle { get; set; } = new TextStyle();
    }
}
