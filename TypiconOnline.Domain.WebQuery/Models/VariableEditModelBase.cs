using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class VariableEditModelBase
    {
        public int Id { get; set; }
        public int TypiconId { get; set; }
        public bool IsTemplate { get; set; }
        public string Name { get; set; }
        public VariableType Type { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        
        public IEnumerable<VariableLinkModel> Links { get; set; } = new List<VariableLinkModel>();
    }
}
