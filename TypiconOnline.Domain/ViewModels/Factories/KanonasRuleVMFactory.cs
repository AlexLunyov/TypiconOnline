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
    public class KanonasRuleVMFactory : ViewModelFactoryBase<KanonasRule>
    {
        List<TextHolder> headers;
        Dictionary<int, ViewModelItem> kanonasHeaders;
        ViewModelItem katavasiaHeader;
        OdiViewModelFactory odiView;

        public KanonasRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer)
        {
            
        }

        public override void Create(CreateViewModelRequest<KanonasRule> req)
        {
            Clear(req);
            //Канон:
            AppendHeader(req);

            for (int i = 1; i <= 9; i++)
            {
                //Песнь
                AppendOdi(req, i);
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

        private void AppendOdi(CreateViewModelRequest<KanonasRule> req, int odiNumber)
        {
            //ничего не делаем, если нет канонов с таким номером песни
            if (req.Element.Kanones.FirstOrDefault(c => c.Odes.Exists(k => k.Number == odiNumber)) == null)
            {
                return;
            }

            AppenOdiHeader(req, odiNumber);

            ///Проходим по всем канонам и добавляем песню, согласно индекса, если она имеется
            for (int i = 0; i < req.Element.Kanones.Count; i++)
            {
                var kanonas = req.Element.Kanones[i];
                ///Признак того, последний ли канон (катавасию не считаем)
                bool isLastKanonas = (i == req.Element.Kanones.Count - 2);
                bool isOdi8 = odiNumber == 8;

                if (kanonas.Odes.FirstOrDefault(c => c.Number == odiNumber && c.Troparia.Count > 0) is Odi odi)
                {
                    ///Проверяем, данная песнь не Катавасия ли - часть канона, который есть одна из катавасий по вся дни лета
                    //bool isKatavasiaKanonas = odi.Troparia.TrueForAll(c => c.Kind == YmnosKind.Katavasia);
                    bool isKatavasiaKanonas = (i == req.Element.Kanones.Count - 1);

                    ///Добавляем шапку канона
                    if (isKatavasiaKanonas)
                    {
                        AppendKatavasiaHeader(req, kanonas.Ihos);

                        if (odiNumber == 8)
                        {
                            //TODO: по умолчанию добавляет после 8-й песни "Хвалим, благословим", но так не должно быть всегда.
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
                        DefaultChorus = kanonas.Stihos[req.Handler.Settings.Language]
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
                    name = kanonas.Name[req.Handler.Settings.Language];
                }
                else
                {
                    holder = GetHeaders(req)[3];
                }

                string kanonasString = holder.Paragraphs[0][req.Handler.Settings.Language];
                kanonasString = kanonasString.Replace("[kanonas]", name).
                                              Replace("[ihos]", kanonas.Ihos.ToString());


                view = ViewModelItemFactory.Create(TextHolderKind.Text, new List<string> { kanonasString });

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
                string str = GetHeaders(req)[4].Paragraphs[0][req.Handler.Settings.Language];
                str = str.Replace("[ihos]", ihos.ToString());

                katavasiaHeader = ViewModelItemFactory.Create(TextHolderKind.Text, new List<string> { str });
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
            viewModel.Paragraphs[0] = viewModel.Paragraphs[0].Replace("[odinumber]", i.ToString());

            req.AppendModelAction(new ElementViewModel() { viewModel });
        }

        private void AppendAfterRule(CreateViewModelRequest<KanonasRule> req, int i)
        {
            //throw new NotImplementedException();
        }

        

        private List<TextHolder> GetHeaders(CreateViewModelRequest<KanonasRule> req)
        {
            if (headers == null)
            {
                headers = req.Handler.Settings.Rule.Owner.GetCommonRuleChildren(
                    new CommonRuleServiceRequest() { Key = CommonRuleConstants.KanonasRule, RuleSerializer = Serializer }).Cast<TextHolder>().ToList();
            }
            return headers;
        }
    }
}
