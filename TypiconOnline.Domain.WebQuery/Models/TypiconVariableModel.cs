using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TypiconVariableModel : IGridModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VariableType Type { get; set; }
        public int Count { get; set; }
        public bool HasValue { get; set; }
    }
}
