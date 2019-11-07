using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditTypiconVariableCommand : EditRuleCommandBase<TypiconVariable>
    {
        public EditTypiconVariableCommand(int id
            , string description) : base(id)
        {
            Description = description;
        }
        public string Description { get; }
    }
}
