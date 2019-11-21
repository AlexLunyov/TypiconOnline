using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypiconOnline.Web.Models.TypiconViewModels
{
    public class TypiconOperationsModel
    {
        public bool IsModified { get; set; }
        public bool IsTemplate { get; set; }
        public bool HasVariables { get; set; }
    }
}
