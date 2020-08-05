using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Versioned.Typicon.Variable
{
    public class VariableRuleLink<T> where T : VersionBase, new()
    {
        public int VariableId { get; set; }
        public virtual TypiconVariable Variable { get; set; }

        public int EntityId { get; set; }

        public virtual T Entity { get; set; }
    }
}
