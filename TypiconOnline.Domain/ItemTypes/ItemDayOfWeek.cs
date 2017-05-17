using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;
using System.Text.RegularExpressions;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemDayOfWeek : ItemType
    {
        private DayOfWeek _dayOfWeek;

        private string _stringValue = "";

        private bool _isValid;

        private ItemDayOfWeek() { }

        public ItemDayOfWeek(DayOfWeek val)
        {
            _dayOfWeek = val;

            _stringValue = val.ToString();
        }

        public ItemDayOfWeek(string exp)
        {
            _stringValue = exp;
            _isValid = true;

            if (exp == RuleConstants.DefinitionsDayOfWeek.понедельник.ToString())
            {
                _dayOfWeek = DayOfWeek.Monday;
            }
            else if (exp == RuleConstants.DefinitionsDayOfWeek.вторник.ToString())
            {
                _dayOfWeek = DayOfWeek.Tuesday;
            }
            else if (exp == RuleConstants.DefinitionsDayOfWeek.среда.ToString())
            {
                _dayOfWeek = DayOfWeek.Wednesday;
            }
            else if (exp == RuleConstants.DefinitionsDayOfWeek.четверг.ToString())
            {
                _dayOfWeek = DayOfWeek.Thursday;
            }
            else if (exp == RuleConstants.DefinitionsDayOfWeek.пятница.ToString())
            {
                _dayOfWeek = DayOfWeek.Friday;
            }
            else if (exp == RuleConstants.DefinitionsDayOfWeek.суббота.ToString())
            {
                _dayOfWeek = DayOfWeek.Saturday;
            }
            else if (exp == RuleConstants.DefinitionsDayOfWeek.воскресенье.ToString())
            {
                _dayOfWeek = DayOfWeek.Sunday;
            }
            else
            {
                _isValid = false;
                
                //throw new DefinitionsParsingException("Ошибка: не верно определен день недели в элементе " + valNode.Name);
            }
        }

        public DayOfWeek Value
        {
            get
            {
                return _dayOfWeek;
            }
        }

        protected override void Validate()
        {
            if (!_isValid)
            {
                AddBrokenConstraint(ItemDayOfWeekBusinessConstraint.DayOfWeekTypeMismatch, "ItemDayOfWeek");
            }
        }


    }
}
