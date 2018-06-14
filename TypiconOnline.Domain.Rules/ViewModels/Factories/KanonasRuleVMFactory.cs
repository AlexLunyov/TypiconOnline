using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Schedule.Extensions;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class KanonasRuleVMFactory : ViewModelFactoryBase<KanonasRule>
    {
        IReadOnlyList<TextHolder> headers;
        Dictionary<int, ViewModelItem> kanonasHeaders;
        ViewModelItem katavasiaHeader;
        OdiViewModelFactory odiView;

        public KanonasRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        public override void Create(CreateViewModelRequest<KanonasRule> req)
        {
            if (req.Element == null || !req.Element.IsValid)
            {
                //TODO: просто ничего не делаем, хотя надо бы это обрабатывать
                return;
            }

            Clear(req);
            //Канон:
            AppendHeader(req);

            var defaultOdi = req.Element.Odes.FirstOrDefault(c => c.Number == null);

            for (int i = 1; i <= 9; i++)
            {
                //Песнь
                AppendOdi(req, defaultOdi, i);
                //Правило после песни
                AppendAfterRule(req, i);
            }
        }

        private void Clear(CreateViewModelRequest<KanonasRule> req)
        {
            headers = null;
            kanonasHeaders = new Dictionary<int, ViewModelItem>();
            odiView = new OdiViewModelFactory(req.Handler, Serializer, req.AppendModelAction);
        }

        private void AppendHeader(CreateViewModelRequest<KanonasRule> req)
        {
            TextHolder header = GetHeaders(req)[0];

            req.AppendModelAction(new ElementViewModel() { ViewModelItemFactory.Create(header, req.Handler, Serializer) });
        }

        private void AppendOdi(CreateViewModelRequest<KanonasRule> req, KOdiRule defaultOdiRule, int odiNumber)
        {
            /*
             * Ищем KOdiRule для обработки:
             * Если есть с заданным номером, используем ее. 
             * Иначе если есть defaultOdi, используем ее.
             * Иначе ничего не делаем. Также ничего не делаем, если нет канонов с таким номером у найденного KOdiRule.
            */

            var odiRuleToHandle = req.Element.Odes.FirstOrDefault(c => c.Number == odiNumber) ?? defaultOdiRule;

            if (odiRuleToHandle == null 
                || odiRuleToHandle.Kanones.FirstOrDefault(c => c.Odes.Exists(k => k.Number == odiNumber)) == null)
            {
                return;
            }

            AppenOdiHeader(req, odiNumber);

            //Проходим по всем канонам и добавляем песню, согласно индекса, если она имеется
            for (int i = 0; i < odiRuleToHandle.Kanones.Count; i++)
            {
                var kanonas = odiRuleToHandle.Kanones[i];
                //Признак того, последний ли канон (катавасию не считаем)
                bool isLastKanonas = odiRuleToHandle.Kanones.IsLastKanonasBeforeKatavasia(i);
                bool isOdi8 = req.Element.IsOrthros && odiNumber == 8;

                if (kanonas.Odes.FirstOrDefault(c => c.Number == odiNumber && c.Troparia.Count > 0) is Odi odi)
                {
                    //Проверяем, данная песнь не Катавасия ли - часть канона, который есть одна из катавасий по вся дни лета
                    bool isKatavasiaKanonas = odi.Troparia.TrueForAll(c => c.Kind == YmnosKind.Katavasia);
                    //bool isKatavasiaKanonas = (i == odiRuleToHandle.Kanones.Count - 1);

                    //Добавляем шапку канона
                    if (isKatavasiaKanonas)
                    {
                        AppendKatavasiaHeader(req, kanonas.Ihos);

                        if (isOdi8)
                        {
                            //добавляет после 8-й песни "Хвалим, благословим"
                            AppendStihosOdi8(req);
                        }
                    }
                    else
                    {
                        AppendKanonasHeader(req, kanonas);
                    }

                    odiView.AppendViewModel(new AppendViewModelOdiRequest()
                    {
                        Odi = odi,
                        IsLastKanonas = isLastKanonas,
                        IsOdi8 = isOdi8,
                        DefaultChorus = kanonas.Stihos
                    });
                }
            }
        }

        private void AppendKanonasHeader(CreateViewModelRequest<KanonasRule> req, Kanonas kanonas)
        {
            int hash = kanonas.GetHashCode();

            ViewModelItem view = null;
            
            if (!kanonasHeaders.ContainsKey(hash))
            {
                TextHolder holder = null;
                string name = "";
                if (kanonas.Name != null)
                {
                    holder = GetHeaders(req)[2];
                    name = kanonas.Name.FirstOrDefault(req.Handler.Settings.Language.Name).Text;
                }
                else
                {
                    holder = GetHeaders(req)[3];
                }

                var kanonasP = ParagraphVMFactory.Create(holder.Paragraphs[0], req.Handler.Settings.Language.Name);
                kanonasP.Replace("[kanonas]", name);
                kanonasP.Replace("[ihos]", req.Handler.Settings.Language.IntConverter.ToString(kanonas.Ihos));

                view = ViewModelItemFactory.Create(TextHolderKind.Text, new List<ParagraphViewModel>() { kanonasP });


                //string kanonasString = holder.Paragraphs[0][req.Handler.Settings.Language.Name];
                //kanonasString = kanonasString.Replace("[kanonas]", name).
                //                              Replace("[ihos]", req.Handler.Settings.Language.IntConverter.ToString(kanonas.Ihos));
                //view = ViewModelItemFactory.Create(TextHolderKind.Text, new List<string> { kanonasString });

                kanonasHeaders.Add(hash, view);
            }
            else
            {
                view = kanonasHeaders[hash];
            }

            req.AppendModelAction(new ElementViewModel() { view });
        }

        private void AppendKatavasiaHeader(CreateViewModelRequest<KanonasRule> req, int ihos)
        {
            if (katavasiaHeader == null)
            {
                var katavasiaP = ParagraphVMFactory.Create(GetHeaders(req)[4].Paragraphs[0], req.Handler.Settings.Language.Name);
                katavasiaP.Replace("[ihos]", req.Handler.Settings.Language.IntConverter.ToString(ihos));

                katavasiaHeader = ViewModelItemFactory.Create(TextHolderKind.Text, new List<ParagraphViewModel>() { katavasiaP });

                //string str = GetHeaders(req)[4].Paragraphs[0][req.Handler.Settings.Language.Name];
                //str = str.Replace("[ihos]", req.Handler.Settings.Language.IntConverter.ToString(ihos));

                //katavasiaHeader = ViewModelItemFactory.Create(TextHolderKind.Text, new List<string> { str });
            }

            req.AppendModelAction(new ElementViewModel() { katavasiaHeader });
        }

        /// <summary>
        /// Добавляет "Хвалим, благословим..."
        /// </summary>
        private void AppendStihosOdi8(CreateViewModelRequest<KanonasRule> req)
        {
            TextHolder odi8TextHolder = GetHeaders(req)[5];
            var viewModel = ViewModelItemFactory.Create(odi8TextHolder, req.Handler, Serializer);

            req.AppendModelAction(new ElementViewModel() { viewModel });
        }

        private void AppenOdiHeader(CreateViewModelRequest<KanonasRule> req, int i)
        {
            TextHolder odiTextHolder = GetHeaders(req)[1];

            var viewModel = ViewModelItemFactory.Create(odiTextHolder, req.Handler, Serializer);
            viewModel.Paragraphs[0].
                Replace("[odinumber]", req.Handler.Settings.Language.IntConverter.ToString(i));

            req.AppendModelAction(new ElementViewModel() { viewModel });
        }

        private void AppendAfterRule(CreateViewModelRequest<KanonasRule> req, int odiNumber)
        {
            if (req.Element.AfterRules?.FirstOrDefault(c => c.OdiNumber == odiNumber) is KAfterRule rule)
            {
                rule.Interpret(req.Handler);
            }
        }

        private IReadOnlyList<TextHolder> GetHeaders(CreateViewModelRequest<KanonasRule> req)
        {
            if (headers == null)
            {
                headers = Serializer.GetCommonRuleChildren<TextHolder>(req.Handler.Settings.TypiconId, CommonRuleConstants.KanonasRule);
            }
            return headers;
        }
    }
}
