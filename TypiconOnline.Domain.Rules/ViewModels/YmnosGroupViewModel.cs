using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.ViewModels;

namespace TypiconOnline.Domain.ViewModels
{
    public class YmnosGroupViewModel : ContainerViewModel
    {
        private YmnosGroup _group;

        private string _kindChoirValue;
        private string _kindStihosValue;

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
        public string Annotation { get; set; }
        /// <summary>
        /// Самоподобен - текстовое значение. Заполняется в случае, если в правиле - true
        /// </summary>
        public string Self { get; set; }

        public IRuleSerializerRoot Serializer { get; }

        public YmnosGroupViewModel(YmnosGroup group, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            if (group == null || group.Ymnis == null) throw new ArgumentNullException("YmnosGroup");
            _group = group;

            _handler = handler ?? throw new ArgumentNullException("handler");

            Serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot");

            Ihos = group.Ihos;

            if (group.Annotation?.IsEmpty == false)
            {
                Annotation = group.Annotation[handler.Settings.Language];
            }

            //текст "Глас"
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { RuleSerializer = Serializer };
            req.Key = CommonRuleConstants.IhosText;
            IhosText = handler.Settings.Rule.Owner.GetCommonRuleTextValue(req, handler.Settings.Language);

            //если подобен
            if (group.Prosomoion?.IsEmpty == false)
            {
                req.Key = CommonRuleConstants.ProsomoionText;
                Prosomoion = handler.Settings.Rule.Owner.GetCommonRuleTextValue(req, handler.Settings.Language);

                Prosomoion = string.Format(@"{0}: ""{1}""", Prosomoion, group.Prosomoion[handler.Settings.Language]);
            }
            //самоподобен?
            if (group.Prosomoion?.Self == true)
            {
                req.Key = CommonRuleConstants.SelfText;
                Self = handler.Settings.Rule.Owner.GetCommonRuleTextValue(req, handler.Settings.Language);
            }

            //находим Стих и Хор для дальнешей вставки
            req.Key = CommonRuleConstants.StihosRule;
            _kindStihosValue = handler.Settings.Rule.Owner.GetCommonRuleTextValue(req, handler.Settings.Language);

            req.Key = CommonRuleConstants.ChoirRule;
            _kindChoirValue = handler.Settings.Rule.Owner.GetCommonRuleTextValue(req, handler.Settings.Language);
        }

        protected override void FillChildElements()
        {
            foreach (Ymnos ymnos in _group.Ymnis)
            {
                //добавляем стих и песнопение как отдельные объекты
                foreach (ItemText stihos in ymnos.Stihoi)
                {
                    _childElements.Add(new TextHolderViewModel(Serializer)
                    {
                        Kind = TextHolderKind.Stihos,
                        KindStringValue = _kindStihosValue,
                        Paragraphs = new string[] { stihos[_handler.Settings.Language] }
                    });
                }

                _childElements.Add(new TextHolderViewModel(Serializer)
                {
                    Kind = TextHolderKind.Choir,
                    KindStringValue = _kindChoirValue,
                    Paragraphs = new string[] { ymnos.Text[_handler.Settings.Language] }
                });
            }
        }
    }
}
