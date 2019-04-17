using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreateTypiconCommand : ICommand
    {
        public CreateTypiconCommand(string name, string defaultLanguage, int templateId, int ownerId)
        {
            Name = name;
            DefaultLanguage = defaultLanguage;
            TemplateId = templateId;
            OwnerId = ownerId;
        }

        public string Name { get; }
        public string DefaultLanguage { get; }
        public int TemplateId { get; }
        public int OwnerId { get; }
    }
}
