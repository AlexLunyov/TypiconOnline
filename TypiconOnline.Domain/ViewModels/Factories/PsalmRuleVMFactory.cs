﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels.Messaging;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class PsalmRuleVMFactory : ViewModelFactoryBase<PsalmRule>
    {
        private const string NUMBER = "[number]";

        public PsalmRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer)
        {
        }

        public override void Create(CreateViewModelRequest<PsalmRule> req)
        {
            if (req.Element?.Calculate(req.Handler.Settings) is BookReading psalmReading)
            {
                AppendHeader(req);
                AppendText(req, psalmReading);
            }
        }

        private void AppendHeader(CreateViewModelRequest<PsalmRule> req)
        {
            TextHolder header = req.Handler.Settings.Rule.Owner.GetCommonRuleChildren(
                new CommonRuleServiceRequest() { Key = CommonRuleConstants.Psalm, RuleSerializer = Serializer })
                .FirstOrDefault() as TextHolder;

            var viewHeader = ViewModelItemFactory.Create(header, req.Handler, Serializer);

            //вставляем номер Псалма
            viewHeader.Paragraphs[0] = viewHeader.Paragraphs[0]
                .Replace(NUMBER, req.Handler.Settings.LanguageSettings.IntConverter.ToString(req.Element.Number));

            req.AppendModelAction(new ElementViewModel() { viewHeader });
        }

        private void AppendText(CreateViewModelRequest<PsalmRule> req, BookReading psalmReading)
        {
            List<string> paragraphs = psalmReading.Text.Select(c => c[req.Handler.Settings.LanguageSettings.Name]).ToList();

            req.AppendModelAction(new ElementViewModel() { ViewModelItemFactory.Create(TextHolderKind.Lector, paragraphs) });
        }
    }
}