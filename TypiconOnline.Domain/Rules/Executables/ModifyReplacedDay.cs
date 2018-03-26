using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyReplacedDay : ModifyDay
    {
        public ModifyReplacedDay(string name, IEasterContext context) : base(name)
        {
            EasterContext = context ?? throw new ArgumentNullException("IEasterContext");
        }

        #region Properties

        public KindOfReplacedDay Kind { get; set; }

        public IEasterContext EasterContext { get; }

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
