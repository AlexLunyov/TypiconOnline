using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TypiconEntityEditModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Description { get; set; }
        public string SystemName { get; set; }
        public string DefaultLanguage { get; set; } = CommonConstants.DefaultLanguage;
        public bool IsModified { get; set; }
        public bool IsTemplate { get; set; }
        public bool HasVariables { get; set; }
        public bool HasEmptyPrintTemplates { get; set; }
        public bool DeleteModifiedOutputDays { get; set; } = false;
        /// <summary>
        /// Описание причин, почему Устав не может быть опубликован
        /// </summary>
        public IEnumerable<string> PublishErrors { get; set; }
        /// <summary>
        /// Список Id И Имен редакторов Устава
        /// </summary>
        public IEnumerable<(int, string)> Editors { get; set; }
    }
}
