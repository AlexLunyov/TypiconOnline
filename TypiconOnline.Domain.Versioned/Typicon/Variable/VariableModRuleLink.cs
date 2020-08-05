using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Versioned.Typicon.Variable
{
    public class VariableModRuleLink<T> : VariableRuleLink<T> where T : VersionBase, new()
    {
        public DefinitionType DefinitionType { get; set; }
    }

    public enum DefinitionType
    {
        Rule = 0,
        ModRule = 1
    }
}
