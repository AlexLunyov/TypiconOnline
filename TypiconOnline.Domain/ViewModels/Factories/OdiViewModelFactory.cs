using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class OdiViewModelFactory
    {
        enum ChorusKind { Slava, Blagoslovim, Inyne, Common }

        IRuleHandler handler;
        Action<ElementViewModel> appendModelAction;
        List<TextHolder> choruses;
        IRuleSerializerRoot serializer;

        public OdiViewModelFactory(IRuleHandler handler, IRuleSerializerRoot serializer, Action<ElementViewModel> appendModelAction)
        {
            this.handler = handler ?? throw new ArgumentNullException("IRuleHandler in OdiViewModelHandler");
            this.appendModelAction = appendModelAction ?? throw new ArgumentNullException("Action<ElementViewModel> in OdiViewModelHandler");
            this.serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot in OdiViewModelHandler");

            choruses = handler.Settings.Rule.Owner.GetCommonRuleChildren(
                    new CommonRuleServiceRequest() { Key = CommonRuleConstants.KanonasChorusRule, RuleSerializer = serializer }).Cast<TextHolder>().ToList();
        }

        /// <summary>
        /// Используется для добавления выходной модели песни в каноне Утрени. 
        /// </summary>
        /// <param name="odi"></param>
        /// <param name="katavasiaHeader">Если не null, то считаем, что это последний канон, в который необходимо вставить заголовок Катавасия</param>
        /// <param name="isOdi8">Если true, обавляет "Благословим" вместо "Славы", в 8-ую песнь вставляет стих "Хвалим, благословим..."</param>
        public void AppendViewModel(AppendViewModelOdiRequest req)
        {
            if (req.Odi == null)
            {
                //TODO: просто ничего не делаем, хотя надо бы это обрабатывать
                return;
            }

            for (int i = 0; i < req.Odi.Troparia.Count; i++)
            {
                Ymnos troparion = req.Odi.Troparia[i];
                //добавляем припев, только если это не катавасия
                if (troparion.Kind != YmnosKind.Katavasia)
                {
                    AppendChorus(req.Odi, req.IsLastKanonas, req.IsOdi8, i, req.DefaultChorus);
                }
                
                AppendTroparion(troparion);
            }
        }

        private void AppendChorus(Odi odi, bool isLastKanonas, bool isOdi8, int i, string defaultChorus)
        {
            var troparion = odi.Troparia[i];

            var kind = ChorusKind.Common;

            if (isLastKanonas)
            {
                //проверяем, если последний канон,
                if (i == odi.Troparia.Count - 2)
                {
                    if (!isOdi8)
                    {
                        //если предпоследний тропарь - Слава
                        kind = ChorusKind.Slava;
                    }
                    else
                    {
                        //если 8-ая песня - Благословим
                        kind = ChorusKind.Blagoslovim;
                    }
                }
                else if (i == odi.Troparia.Count - 1)
                {
                    //если последний - И ныне
                    kind = ChorusKind.Inyne;
                }
            }

            string text = GetChorus(kind, troparion, defaultChorus);

            if (!string.IsNullOrEmpty(text)
                //добавляем пустой припев к Ирмосу в любом случае
                || troparion.Kind == YmnosKind.Irmos)
            {
                //добавляем припев
                var view = ViewModelItemFactory.Create((kind == ChorusKind.Common) ? ViewModelItemKind.Chorus : ViewModelItemKind.Text,
                    new List<string>() { text }, handler, serializer);

                appendModelAction(new ElementViewModel() { view });
            }
        }

        private string GetChorus(ChorusKind kind, Ymnos troparion, string defaultChorus)
        {
            string result = "";

            //выбираем какой предустановленный тип припева отображен в тропаре
            if (troparion.Kind == YmnosKind.Ymnos && kind == ChorusKind.Common)
            {
                return defaultChorus;
            }

            int index = -1;
            
            switch (kind)
            {
                case ChorusKind.Slava:
                    index = 0;
                    break;
                case ChorusKind.Blagoslovim:
                    index = 1;
                    break;
                case ChorusKind.Inyne:
                    index = 2;
                    break;
                default:
                    switch (troparion.Kind)
                    {
                        case YmnosKind.Ierarhon:
                            index = 3;
                            break;
                        case YmnosKind.Martyrion:
                            index = 4;
                            break;
                        case YmnosKind.Nekrosimo:
                            index = 5;
                            break;
                        case YmnosKind.Osion:
                            index = 6;
                            break;
                        case YmnosKind.Theotokion:
                            index = 7;
                            break;
                        case YmnosKind.Triadiko:
                            index = 8;
                            break;
                        case YmnosKind.Irmos:
                            //оставляем пустым значение
                            break;
                        case YmnosKind.Katavasia:
                            //оставляем пустым значение
                            break;
                            //case YmnosKind.Ymnos:
                            //    break;
                    }
                    break;
            }

            if (index >= 0)
            {
                result = choruses[index]?.Paragraphs[0]?[handler.Settings.Language.Name] ?? "";
            }

            return result;
        }

        private void AppendTroparion(Ymnos troparion)
        {
            var view = ViewModelItemFactory.Create(troparion, handler, serializer);

            appendModelAction(new ElementViewModel() { view });
        }
    }
}
