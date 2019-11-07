using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class VariableDefineModel: VariableEditModel
    {
        public VariableDefineModel() { }

        public VariableDefineModel(VariableEditModel model)
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
