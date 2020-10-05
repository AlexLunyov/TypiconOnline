using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class VariableDefineModel: VariableEditModelBase
    {
        public VariableDefineModel() { }

        public VariableDefineModel(VariableEditModelBase model)
        {
            Id = model.Id;
            Name = model.Name;
            Type = model.Type;
            Description = model.Description;
            Count = model.Count;
            Links = model.Links;
        }

        public string Value { get; set; }
    }
}
