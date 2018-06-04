using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;
using System.Text.RegularExpressions;

namespace TypiconOnline.Domain.Core.ItemTypes
{
    public class ItemDayOfWeek : ItemEnumType<DayOfWeek>
    {
        //private DayOfWeek _dayOfWeek;

        private string _stringValue = "";

        //private bool _isValid = true;

        private ItemDayOfWeek() { }

        public ItemDayOfWeek(DayOfWeek val)
        {
            _value = val;

            _stringValue = val.ToString();

            _isValid = true;
        }

        public ItemDayOfWeek(string exp) : base(exp)
        {
            if (!_isValid)
            {
                _isValid = true;

                if (exp == DefinitionsDayOfWeek.понедельник.ToString())
                {
                    _value = DayOfWeek.Monday;
                }
                else if (exp == DefinitionsDayOfWeek.вторник.ToString())
                {
                    _value = DayOfWeek.Tuesday;
                }
                else if (exp == DefinitionsDayOfWeek.среда.ToString())
                {
                    _value = DayOfWeek.Wednesday;
                }
                else if (exp == DefinitionsDayOfWeek.четверг.ToString())
                {
                    _value = DayOfWeek.Thursday;
                }
                else if (exp == DefinitionsDayOfWeek.пятница.ToString())
                {
                    _value = DayOfWeek.Friday;
                }
                else if (exp == DefinitionsDayOfWeek.суббота.ToString())
                {
                    _value = DayOfWeek.Saturday;
                }
                else if (exp == DefinitionsDayOfWeek.воскресенье.ToString())
                {
                    _value = DayOfWeek.Sunday;
                }
                else
                {
                    _isValid = false;
                }
            }
        }
    }
}
