using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class OutputDayEditModel
    {
        public int Id { get; set; }
        public int TypiconId { get; set; }
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        public virtual string Name { get; set; }
        public virtual TextStyle NameStyle { get; set; } = new TextStyle();

        public int PrintTemplateId { get; set; }
    }
}
