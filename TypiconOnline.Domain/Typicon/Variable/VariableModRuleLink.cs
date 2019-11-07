using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Typicon.Variable
{
    public class VariableModRuleLink<T> : VariableRuleLink<T> where T : ModRuleEntity, new()
    {
        public DefinitionType DefinitionType { get; set; }
    }

    public enum DefinitionType
    {
        Rule = 0,
        ModRule = 1
    }
}
