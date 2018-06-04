using TypiconOnline.Domain.Rules.Calendar;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Days
{
    public abstract class CalendarElement : EntityBase<int>, IAggregateRoot
    {
        public virtual string Name { get; set; }


        private CalendarContainer _definition;
        public CalendarContainer Definition
        {
            get
            {
                return _definition;
            }
            set
            {
                _definition = value;

                //добавить изменение RuleDefinition
            }
        }


        private string _definitionText;
        public string DefinitionText
        {
            get
            {
                return _definitionText;
            }

            set
            {
                _definitionText = value;

                //добавить изменение Rule
            }
        }
    }
}
