using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditRuleCommandBase<T> : ICommand where T : RuleEntity, new()
    {
        public EditRuleCommandBase(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}