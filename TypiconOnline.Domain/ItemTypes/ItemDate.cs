using System;
using System.Globalization;
using TypiconOnline.Domain.Books.Elements;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemDate : ItemType
    {
        private int _month = 0;
        private int _day = 0;

        private string _expression = string.Empty;

        public ItemDate() { }
        public ItemDate(int monthes, int days)
        {
            _month = monthes;
            _day = days;

            _expression = GetExpression(_month, _day);
        }

        public ItemDate(ItemDate date)
        {
            _month = date.Month;
            _day = date.Day;
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
            set
            {
                string exp = GetExpression(value, _day);

                Expression = exp;
            }
        }

        public int Day
        {
            get
            {
                return _day;
            }
            set
            {
                string exp = GetExpression(_month, value);

                Expression = exp;
            }
        }

        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;

                var date = new DateTime();
                if (IsExpressionValid(_expression, out date))
                {
                    _month = date.Month;
                    _day = date.Day;
                }
                //else
                //{
                //    //в случае неверного заполнения, задаем отрицательные значения месяцу и дню
                //    _month = 99;
                //    _day = 99;
                //}
            }
        }

        public override string ToString() => GetExpression(_month, _day);

        /// <summary>
        /// Возврашает строку в формате "--MM-dd"
        /// </summary>
        /// <returns></returns>
        private string GetExpression(int month, int day)
        {
            return $"--{month.ToString("00")}-{day.ToString("00")}";
        }

        /// <summary>
        /// Считаем пустой и валидной дату с нулевыми значениями
        /// </summary>
        public bool IsEmpty => Month == 0 && Day == 0 && string.IsNullOrEmpty(Expression);

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
            string format = ElementConstants.ItemDateParsing + "yyyy";

            //добавляем фейковый год, который гарантированно високосный
            expression = expression + "2016";

            return DateTime.TryParseExact(expression, format, new CultureInfo("ru-RU"), DateTimeStyles.None, out date);
        }
    }
}
