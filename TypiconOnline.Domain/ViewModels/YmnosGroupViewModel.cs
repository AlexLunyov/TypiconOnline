using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels
{
    public class YmnosGroupViewModel : ContainerViewModel
    {
        /// <summary>
        /// Глас
        /// </summary>
        public int Ihos { get; set; }
        /// <summary>
        /// "Глас" текст
        /// </summary>
        public string IhosText { get; set; }
        /// <summary>
        /// Подобен
        /// </summary>
        public string Prosomoion { get; set; }
        /// <summary>
        /// Самоподобен - текстовое значение. Заполняется в случае, если в правиле - true
        /// </summary>
        public string Self { get; set; }

        public YmnosGroupViewModel(YmnosGroup group, IRuleHandler handler) : base()
        {
            if (group == null || group.Ymnis == null) throw new ArgumentNullException("YmnosGroup");
            if (handler == null) throw new ArgumentNullException("handler");

            Ihos = group.Ihos.Value;

            if (!group.Annotation.IsEmpty)
            {
                Text = group.Annotation.Text[handler.Settings.Language];
            }

            //текст "Глас"
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { Handler = handler };
            req.Key = CommonRuleConstants.IhosText;
            IhosText = CommonRuleService.Instance.GetTextValue(req);

            //если подобен
            if (!group.Prosomoion.IsEmpty)
            {
                req.Key = CommonRuleConstants.ProsomoionText;
                Prosomoion = CommonRuleService.Instance.GetTextValue(req);

                Prosomoion = Prosomoion + " " + group.Prosomoion.Text[handler.Settings.Language];
            }
            //самоподобен?
            if (group.Prosomoion.Self.Value)
            {
                req.Key = CommonRuleConstants.SelfText;
                Self = CommonRuleService.Instance.GetTextValue(req);
            }

            foreach (Ymnos ymnos in group.Ymnis)
            {
                //добавляем стих и песнопение как отдельные объекты
                if (!ymnos.Stihos.IsEmpty)
                {
                    ChildElements.Add( new TextHolderViewModel()
                    {
                        Kind = TextHolderKind.Stihos,
                        Paragraphs = new string[] { ymnos.Stihos.Text[handler.Settings.Language] }
                    });
                }

                ChildElements.Add(new TextHolderViewModel()
                {
                    Kind = TextHolderKind.Choir,
                    Paragraphs = new string[] { ymnos.Text.Text[handler.Settings.Language] }
                });
            }
        }
    }
}
