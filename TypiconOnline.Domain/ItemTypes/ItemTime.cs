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
    public class ItemTime : ItemType
    {
        private int _hour = 0;
        private int _minute = 0;

        private string _stringExpression = "";

        public ItemTime() { }
        public ItemTime(int hour, int minute)
        {
            _hour = hour;
            _minute = minute;

            _stringExpression = hour.ToString("00") + "." + minute.ToString("00");
        }

        public ItemTime(string exp)
        {
            Expression = exp;
        }

        public int Hour
        {
            get
            {
                return _hour;
            }
        }

        public int Minute
        {
            get
            {
                return _minute;
            }
        }

        public string Expression
        {
            get
            {
                return _stringExpression;
            }
            set
            {
                _stringExpression = value;

                DateTime date = new DateTime();
                if (DateTime.TryParseExact(_stringExpression, RuleConstants.ItemTimeParsing, new CultureInfo("ru-RU"), DateTimeStyles.None, out date))
                {
                    _hour = date.Hour;
                    _minute = date.Minute;
                }
            }
        }

        /// <summary>
        /// Возврашает строку в формате "--MM-dd"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = _hour.ToString("00") + "."+_minute.ToString("00");
            return result;
        }

        public bool IsEmpty
        {
            get
            {
                return (_stringExpression == "");
            }
        }

        protected override void Validate()
        {
            if (!IsEmpty)
            {
                DateTime date = new DateTime();
                if (!DateTime.TryParseExact(this.ToString(), RuleConstants.ItemTimeParsing, new CultureInfo("ru-RU"), DateTimeStyles.None, out date))
                {
                    AddBrokenConstraint(ItemTimeBusinessConstraint.TimeTypeMismatch, "ItemTime");
                    //throw new DefinitionsParsingException("Ошибка: неверно введена дата в элементе " + node.Name);
                }
            }
        }


    }
}
