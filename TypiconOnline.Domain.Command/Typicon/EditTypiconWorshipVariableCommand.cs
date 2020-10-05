using System;
using System.Collections.Generic;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Models;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditTypiconWorshipVariableCommand : EditRuleCommandBase<TypiconVariable>
    {
        public EditTypiconWorshipVariableCommand(int id
            , string description, string value) : base(id)
        {
            Description = description;
            Value = value;
        }
        
        public string Description { get; }

        /// <summary>
        /// Значение переменной
        /// </summary>
        public string Value { get; }
    }
}
