using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Web.Models.TypiconViewModels
{
    public class EditTypiconModel
    {
        public int Id { get; set; }
        public string Name_CsRu { get; set; }
        public string Name_CsCs { get; set; }
        public string Name_RuRu { get; set; }
        public string Name_ElEl { get; set; }
        public string DefaultLanguage { get; set; }
    }
}
