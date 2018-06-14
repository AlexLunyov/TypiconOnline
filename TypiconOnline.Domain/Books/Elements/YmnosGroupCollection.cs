using System;
using System.Collections.Generic;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Коллекция Групп песнопений. Используется в YmnosStructure
    /// </summary>
    public class YmnosGroupCollection : List<YmnosGroup>, ICloneable
    {
        /// <summary>
        /// Возвращает общее количество стихир
        /// </summary>
        /// <returns></returns>
        public int TotalYmnisCount
        {
            get
            {
                int sticheraCount = 0;

                foreach (YmnosGroup group in this)
                {
                    sticheraCount += group.Ymnis.Count;
                }
                //}

                return sticheraCount;
            }
        }

        public object Clone()
        {
            var result = new YmnosGroupCollection();

            ForEach(group => result.Add(new YmnosGroup(group)));

            return result;
        }

        /// <summary>
        /// Возвращает коллекцию богослужебных текстов
        /// </summary>
        /// <param name="count">Количество. Если = 0, то выдаем все без фильтрации</param>
        /// <param name="startFrom">стартовый индекс (1 - ориентированный)</param>
        /// <returns></returns>
        public YmnosGroupCollection GetYmnis(int count, int startFrom)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (startFrom < 1 || startFrom > TotalYmnisCount)
            {
                throw new ArgumentOutOfRangeException("startFrom");
            }

            if (count == 0)
            {
                //выдаем все без фильтрации
                return Clone() as YmnosGroupCollection;
            }

            var result = new YmnosGroupCollection();

            /*если заявленное количество больше того, что есть, выдаем с повторами
            * например: 8 = 3 3 2
            *           10= 4 4 3
            * 
            */
            //if (count > YmnosStructureCount)
            //{
            int appendedCount = 0;

            int i = startFrom - 1;

            YmnosGroup lastGroup = null;

            while (appendedCount < count)
            {
                //округляем в большую сторону результат деления count на YmnosStructureCount
                //в результате получаем, сколько раз необходимо повторять песнопение
                int b = (int)Math.Ceiling((double)(count - appendedCount) / (TotalYmnisCount - i));

                YmnosGroup groupToAdd = GetThroughYmnos(i);

                if (lastGroup == null || !lastGroup.Equals(groupToAdd))
                {
                    result.Add(groupToAdd);
                    lastGroup = groupToAdd;
                    appendedCount++;
                    b--;
                }

                Ymnos ymnosToAdd = groupToAdd.Ymnis[0];

                while (b > 0)
                {
                    lastGroup.Ymnis.Add(new Ymnos(ymnosToAdd));

                    b--;
                    appendedCount++;
                }

                i++;
            }
            //}

            return result;
        }

        /// <summary>
        /// Возвращает один богослужебный текст с описанием гласа и подобна.
        /// </summary>
        /// <param name="index">Сквозной индекс из общего числа текстов данного объекта.</param>
        /// <returns></returns>
        public YmnosGroup GetThroughYmnos(int index)
        {
            if (index >= TotalYmnisCount || index < 0)
            {
                throw new IndexOutOfRangeException("YmnosGroup");
            }

            YmnosGroup result = null;

            int i = index;

            foreach (YmnosGroup group in this)
            {
                if (i < group.Ymnis.Count)
                {
                    //значит ищем в этой коллекции
                    result = group.GetGroupWithSingleYmnos(i);

                    break;
                }
                else
                {
                    i = i - group.Ymnis.Count;
                }
            }

            return result;
        }
    }
}
