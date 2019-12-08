using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconEntityByPrintWeekTemplateQuery : TypiconEntityByChildQuery<PrintWeekTemplate>
    {
        public TypiconEntityByPrintWeekTemplateQuery(int id) : base(id)
        {
        }
    }
}
