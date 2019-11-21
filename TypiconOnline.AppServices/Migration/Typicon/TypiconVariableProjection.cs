using System;
using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class TypiconVariableProjection
    {
        public TypiconVariableProjection() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public VariableType Type { get; set; }
    }
}