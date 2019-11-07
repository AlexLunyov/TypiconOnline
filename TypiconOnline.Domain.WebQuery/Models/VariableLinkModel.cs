using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class VariableLinkModel
    {
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public DefinitionType DefinitionType { get; set; }
    }
}