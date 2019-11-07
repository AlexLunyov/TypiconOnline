using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DefineTypiconVariableCommand : DeleteRuleCommandBase<TypiconVariable>
    {
        public DefineTypiconVariableCommand(int id
            , string value) : base(id)
        {
            Value = value;
        }
        public string Value { get; }
    }
}
