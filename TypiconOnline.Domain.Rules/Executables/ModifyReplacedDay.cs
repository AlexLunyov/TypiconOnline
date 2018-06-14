using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyReplacedDay : ModifyDay
    {
        public ModifyReplacedDay(string name, IAsAdditionElement parent) : base(name, parent) { }

        #region Properties

        public KindOfReplacedDay Kind { get; set; }

        /// <summary>
        /// Дата, по которой будет совершаться поиск правила для модификации
        /// </summary>
        public DateTime DateToReplaceCalculated
        {
            get; private set; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            InterpretChildDateExp(handler);

            DateToReplaceCalculated = handler.Settings.Date;

            handler.Execute(this);

            DateTime date = handler.Settings.Date;
            handler.Settings.Date = MoveDateCalculated;

            ModifyReplacedDay?.Interpret(handler);
            //возвращаем на всякий случай обратно дату
            handler.Settings.Date = date;
        }

    }
}
