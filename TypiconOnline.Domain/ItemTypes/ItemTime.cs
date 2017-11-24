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
        private string _stringExpression = "";

        public ItemTime() { }
        public ItemTime(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;

            _stringExpression = hour.ToString("00") + "." + minute.ToString("00");
        }

        public ItemTime(string exp)
        {
            Expression = exp;
        }

        public int Hour
        {
            get; private set; } = 0;

        public int Minute
        {
            get; private set; } = 0;

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
                    Hour = date.Hour;
                    Minute = date.Minute;
                }
            }
        }

        /// <summary>
        /// Возврашает строку в формате "HH.mm"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = Hour.ToString("00") + "."+Minute.ToString("00");
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
