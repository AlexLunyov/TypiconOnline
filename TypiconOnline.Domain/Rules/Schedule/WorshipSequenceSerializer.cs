﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class WorshipSequenceSerializer : ExecContainerSerializer
    {
        public WorshipSequenceSerializer(IRuleSerializerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.MikrosEsperinosNode,
                RuleConstants.EsperinosNode,
                RuleConstants.OrthrosNode,
                RuleConstants.LeitourgiaNode };
        }

        protected override ExecContainer CreateObject(XmlDescriptor d)
        {
            return new WorshipSequence(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, ExecContainer container)
        {
            base.FillObject(d, container);

            if (Enum.TryParse(d.GetElementName(), true, out WorshipSequenceKind kind))
            {
                (container as WorshipSequence).Kind = kind;
            }
        }
    }
}