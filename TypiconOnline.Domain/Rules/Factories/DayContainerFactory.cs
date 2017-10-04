using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Factories
{
    public class DayContainerFactory : IDayContainerFactory
    {
        private string _dayDefinition;

        public DayContainerFactory(string dayDefinition)
        {
            if (string.IsNullOrEmpty(dayDefinition)) throw new ArgumentNullException("DayDefinition");

            _dayDefinition = dayDefinition;
        }

        public virtual string DayDefinition
        {
            get
            {
                return _dayDefinition;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("DayDefinition");

                _dayDefinition = value;
            }
        }

        public DayContainer Create()
        {
            return new TypiconSerializer().Deserialize<DayContainer>(_dayDefinition);
        }
    }
}
