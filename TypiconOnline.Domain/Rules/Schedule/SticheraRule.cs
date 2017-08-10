using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Описание стихир Господи воззвах в последовательности богослужений
    /// </summary>
    public class SticheraRule : ExecContainer, ICustomInterpreted
    {
        private Stichera _stichera;
        private ItemBoolean _showPsalm;

        public SticheraRule(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.ShowPsalmAttribute];

            _showPsalm = new ItemBoolean((attr != null) ? attr.Value : "false");
        }

        #region Properties

        /// <summary>
        /// Вычисленная последовательность стихир на Господи воззвах
        /// </summary>
        public Stichera CalculatedStichera
        {
            get
            {
                return _stichera;
            }
        }

        public ItemBoolean ShowPsalm
        {
            get
            {
                return _showPsalm;
            }
        }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid && handler.IsAuthorized<SticheraRule>())
            {
                //используем специальный обработчик для SticheraRule,
                //чтобы создать список источников стихир на обработку
                SticheraRuleHandler kekragariaHandler = new SticheraRuleHandler();

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, kekragariaHandler);
                }

                RuleContainer container = kekragariaHandler.GetResult();

                if (container != null)
                {
                    CalculateStichera(date, handler, container);
                }
            }
        }

        private void CalculateStichera(DateTime date, IRuleHandler handler, RuleContainer container)
        {
            _stichera = new Stichera();
            foreach (YmnosRule ymnosRule in container.ChildElements)
            {
                if (ymnosRule.Source.Value == YmnosSource.Irmologion)
                {
                    //добавляем Богородичны из приложений Ирмология
                }
                else
                {
                    //разбираемся с source
                    DayService dayService = null;
                    switch (ymnosRule.Source.Value)
                    {
                        case YmnosSource.Item1:
                            dayService = (handler.Settings.DayServices.Count > 0) ? handler.Settings.DayServices[0] : null;
                            break;
                        case YmnosSource.Item2:
                            dayService = (handler.Settings.DayServices.Count > 1) ? handler.Settings.DayServices[1] : null;
                            break;
                        case YmnosSource.Item3:
                            dayService = (handler.Settings.DayServices.Count > 2) ? handler.Settings.DayServices[2] : null;
                            break;
                        case YmnosSource.Oktoikh:
                            dayService = BookStorage.Oktoikh.GetOktoikhDay(date);
                            break;
                    }

                    if (dayService == null)
                    {
                        throw new KeyNotFoundException("SticheraRule source not found: " + ymnosRule.Source.Value.ToString());
                    }

                    //теперь разбираемся с place
                     //ymnosRule.Place.Value
                }

                //handler.Settings.DayServices
            }
        }

        protected override void Validate()
        {
            base.Validate();
            //TODO: добавить проверку на наличие элементов stichira в дочерних элементах
        }
    }
}
