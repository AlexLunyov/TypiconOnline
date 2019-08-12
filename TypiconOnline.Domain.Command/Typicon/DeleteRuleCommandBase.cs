using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteRuleCommandBase<T> : ICommand where T : RuleEntity, new()
    {
        public DeleteRuleCommandBase(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}