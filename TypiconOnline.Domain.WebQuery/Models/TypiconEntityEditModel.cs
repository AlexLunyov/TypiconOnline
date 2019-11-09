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
        public bool IsModified { get; set; }
        public bool IsTemplate { get; set; }
        public bool HasVariables { get; set; }
        /// <summary>
        /// Список Id И Имен редакторов Устава
        /// </summary>
        public IEnumerable<(int, string)> Editors { get; set; }
    }
}
