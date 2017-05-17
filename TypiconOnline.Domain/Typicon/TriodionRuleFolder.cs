using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    public class TriodionRuleFolder : FolderEntity
    {
        public new TriodionRuleFolder Parent { get; set; }

        public new List<TriodionRuleFolder> Folders { get; set; }

    }
}
