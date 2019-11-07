using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class VariableEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public IEnumerable<VariableLinkModel> Links { get; set; } = new List<VariableLinkModel>();
    }
}
