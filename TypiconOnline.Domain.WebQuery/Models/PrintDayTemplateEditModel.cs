using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.WebQuery.Attributes;
using TypiconOnline.Domain.WebQuery.Models;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintDayTemplateEditModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string OldFileName { get; set; }
        [MaxFileSize(500 * 1024)]
        [AllowedExtensions(new string[] { ".docx" })]
        public IFormFile File { get; set; }
    }
}
