using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class IsExistsBusinessConstraint
    {
        public static readonly BusinessConstraint YmnosRuleReqiured 
            = new BusinessConstraint($"Отсутствуют дочерний элемент {RuleConstants.YmnosRuleNode}.");
    }
}
