using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Правило, которое для конкретной даты явно описывает особенности богослужения.
    /// Используется как AsAddition
    /// </summary>
    public class ExplicitAddRule : RuleEntity
    {
        /// <summary>
        /// Конкретная дата
        /// </summary>
        public DateTime Date { get; set; }

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            //
        }
    }
}
