using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypiconOnline.Web.Models.TypiconViewModels
{
    public class CreateTypiconModel
    {
        public string Name { get; set; }

        public string DefaultLanguage { get; set; }

        public int TemplateId { get; set; }
    }
}
