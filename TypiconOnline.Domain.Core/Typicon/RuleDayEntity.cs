using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Класс описывает особенности соединения служб (Минея, Триодь, Октоих)
    /// </summary>
    /// <typeparam name="DayType">Тип дня (Минея, Триодь, Октоих)</typeparam>
    public class RuleDayEntity<DayType> : RuleEntity/*<ExecContainer>*/ where DayType : Day
    {
        public DayType Day { get; set; }

        /// <summary>
        /// Назначенный знак для этого описания
        /// </summary>
        public Sign Sign { get; set; }

        public new RuleFolderEntity<DayType> Folder { get; set; }

        //отсутвует хранение xml-формы правила

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
