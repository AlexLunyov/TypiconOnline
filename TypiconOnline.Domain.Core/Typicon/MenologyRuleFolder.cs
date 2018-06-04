using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    public class MenologyRuleFolder : FolderEntity //RuleFolderEntity<MenologyDay>
    {
        public MenologyRuleFolder()
        {
            Name = "";
            Folders = new List<MenologyRuleFolder>();
            Rules = new List<RuleEntity>();
        }

        //public IOwner Owner { get; set; }
        public new string Name { get; set; }

        public new MenologyRuleFolder Parent { get; set; }

        public new List<MenologyRuleFolder> Folders { get; set; }

        public new List<RuleEntity> Rules { get; set; }
    }
}
