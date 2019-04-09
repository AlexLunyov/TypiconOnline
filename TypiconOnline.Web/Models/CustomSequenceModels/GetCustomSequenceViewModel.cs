using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TypiconOnline.Web.Models.CustomSequenceModels
{
    public class GetCustomSequenceViewModel
    {
        [Required]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public string CustomSequence { get; set; }
        [Required]
        public string Language { get; set; }
    }
}
