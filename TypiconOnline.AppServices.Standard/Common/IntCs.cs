using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.AppServices.Common
{
    /// <summary>
    /// Класс дял миграции церковно-славянских чисел в цифровые целочисленные значения и обратно
    /// в диапазоне от 1 до 20000
    /// </summary>
    public class IntCs
    {
        static readonly string[] TITLE = new string[] { @"\", "7" };
        static readonly string THOUSAND = "¤";
        static readonly int MAXVALUE = 20000;

        /// <summary>
        /// Если в строковом массиве несколько значений, первым указывается то, которое отображется без титл
        /// Важно! - потому как по этому признаку определяется, число ли вводимая строка или нет
        /// </summary>
        static readonly Dictionary<int, string[]> intCSDictionary = new Dictionary<int, string[]>()
        {
            { 1,   new string[] {"№", "а"} },
            { 2,   new string[] {"в" } },
            { 3,   new string[] {"G", "г" } },
            { 4,   new string[] {"д" } },
            { 5,   new string[] {"є" } },
            { 6,   new string[] {"ѕ" } },
            { 7,   new string[] {"з" } },
            { 8,   new string[] {"}", "и" } },
            { 9,   new string[] {"f" } },
            { 10,   new string[] {"‹", "i" } },
            { 11,   new string[] {"аi" } },
            { 12,   new string[] {"вi" } },
            { 13,   new string[] {"гi" } },
            { 14,   new string[] {"дi" } },
            { 15,   new string[] {"єi" } },
            { 16,   new string[] {"ѕi" } },
            { 17,   new string[] {"зi" } },
            { 18,   new string[] {"иi" } },
            { 19,   new string[] {"fi" } },
            { 20,   new string[] {"к" } },
            { 30,   new string[] {"л" } },
            { 40,   new string[] {"м" } },
            { 50,   new string[] {"н" } },
            { 60,   new string[] {"x" } },
            { 70,   new string[] {"o" } },
            { 80,   new string[] {"п" } },
            { 90,   new string[] {"ч" } },
            { 100,   new string[] {"р" } },
            { 200,   new string[] {"с" } },
            { 300,   new string[] {"т" } },
            { 400,   new string[] {"µ" } },
            { 500,   new string[] {"ф" } },
            { 600,   new string[] {"х" } },
            { 700,   new string[] {"p" } },
            { 800,   new string[] {"t" } },
            { 900,   new string[] {"ц" } }
        };

        /// <summary>
        /// Распознает строку как целочисленное значение, написанное символами ц-с языка от 1 до 20000
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Parse(string str)
        {
            //нуля нет в ц-с
            int result = 0;

            if (!IsDigit(str))
            {
                return result;
            }

            //удаляем титлы
            foreach (var title in TITLE)
            {
                str = str.Replace(title, string.Empty);
            }

            if (str.StartsWith(THOUSAND))
            {
                //нашли тысячи
                str = str.Replace(THOUSAND, string.Empty);

                Find();
                result *= 1000;
            }

            while (!string.IsNullOrEmpty(str) && Find()) { }

            //обнуляем результат, если строка не пустая - значит возникла ошибка парсинга
            if (!string.IsNullOrEmpty(str))
            {
                result = 0;
            }

            return (result <= MAXVALUE) ? result : MAXVALUE;

            bool Find()
            {
                foreach (var item in intCSDictionary)
                {
                    foreach (var s in item.Value)
                    {
                        //нашли совпадение
                        if (str.StartsWith(s))
                        {
                            result += item.Key;
                            str = str.Remove(0, s.Length);
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        private static bool IsDigit(string str)
        {
            bool isDigit = false;

            //ищем титлы титлы
            foreach (var title in TITLE)
            {
                if (str.Contains(title))
                {
                    isDigit = true;
                }
            }

            //ищем первые элементы из массовов строк справочника, где определны более одного элемента

            foreach (var item in intCSDictionary)
            {
                if (item.Value.Count() > 1)
                {
                    if (str.Contains(item.Value[0]))
                    {
                        isDigit = true;
                    }
                }
            }

            //проверяем, если не было титл - значит это вообще не число
            return isDigit;
        }

        public static bool TryParse(string str, out int value)
        {
            value = Parse(str);

            return value != 0;
        }
    }

    /*
     *  Таблица соответствий
            1	№ а
            2	в
            3	G г
            4	д
            5	є7
            6	ѕ
            7	з7
            8	} и
            9	f
            10	‹ i
            11	аi
            12	вi
            13	гi
            14	дi
            15	єi
            16	ѕi
            17	зi
            18	иi
            19	fi
            20	к
            30	л
            40	м
            50	н
            60	x
            70	o
            80	п
            90	ч
            100	р
            200	с
            300	т
            400	µ
            500	ф
            600	х
            700	p
            800	t
            900	ц
            Тысячная приставка	¤
            Титла	\
     */
}
