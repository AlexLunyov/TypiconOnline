using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.WebQuery.Attributes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintWeekTemplateEditModel
    {
        public int Id { get; set; }

        public int TypiconVersionId { get; set; }

        [Range(1, 7, ErrorMessage = "Количество дней должно быть определено в интервале от 1 до 7")]
        public int DaysPerPage { get; set; }

        public string OldFileName { get; set; }

        [MaxFileSize(1 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".docx" })]
        public IFormFile File { get; set; }
    }
}
