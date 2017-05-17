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
    public class ItemDate : ItemType
    {
        private int _month = 0;
        private int _day = 0;

        private string _stringExpression = "";

        public ItemDate() { }
        public ItemDate(int monthes, int days)
        {
            _month = monthes;
            _day = days;

            _stringExpression = "--" + monthes.ToString("00") + "-" + _day.ToString("00");
        }

        public ItemDate(string exp)
        {
            Expression = exp;
        }

        public int Month
        {
            get
            {
                return _month;
            }
        }

        public int Day
        {
            get
            {
                return _day;
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
                if (IsExpressionValid(_stringExpression, out date))
                {
                    _month = date.Month;
                    _day = date.Day;
                }
            }
        }

        /// <summary>
        /// Возврашает строку в формате "--MM-dd"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "--" + _month.ToString("00") + "-"+_day.ToString("00");
            return result;
        }

        public bool IsEmpty
        {
            get
            {
                return (_stringExpression == "");
            }
            //set
            //{
            //    _stringExpression = "";
            //    _month = 0;
            //    _day = 0;
            //}
        }

        protected override void Validate()
        {
            if (!IsEmpty)
            {
                DateTime date = new DateTime();
                if (!IsExpressionValid(this.ToString(), out date))
                {
                    AddBrokenConstraint(ItemDateBusinessConstraint.DateTypeMismatch, "ItemDate");
                    //throw new DefinitionsParsingException("Ошибка: неверно введена дата в элементе " + node.Name);
                }
            }
        }

        /// <summary>
        /// Проверка формата выражения
        /// --02-29 будет возвращать true
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private bool IsExpressionValid(string expression, out DateTime date)
        {
            string format = RuleConstants.ItemDateParsing + "yyyy";

            //добавляем фейковый год, который гарантированно високосный
            expression = expression + "2016";

            return DateTime.TryParseExact(expression, format, new CultureInfo("ru-RU"), DateTimeStyles.None, out date);
        }
    }
}
