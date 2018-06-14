using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.AppServices.Migration.Psalter
{
    public static class PsalterExtensions
    {
        /// <summary>
        /// Добавляет Кафизму или обновляет существующую, возвращая ее
        /// </summary>
        /// <param name="typiconEntity"></param>
        /// <param name="kathisma"></param>
        /// <returns></returns>
        public static Kathisma AppendOrUpdateKathisma(this TypiconEntity typiconEntity, Kathisma kathisma)
        {
            var found = typiconEntity.Kathismas.FirstOrDefault(c => c.Number == kathisma.Number);

            if (found != null)
            {
                found.NumberName.Merge(kathisma.NumberName);

                return found;
            }
            else
            {
                typiconEntity.Kathismas.Add(kathisma);

                return kathisma;
            }
        }

        public static void AppendStihos(this PsalmLink psalmLink, BookStihos stihos)
        {
            if (psalmLink.StartStihos == null)
            {
                psalmLink.StartStihos = stihos.StihosNumber;
                psalmLink.EndStihos = stihos.StihosNumber;
            }
            else
            {
                psalmLink.EndStihos = stihos.StihosNumber;
            }
        }

        public static void AppendOrUpdatePsalmLink(this Kathisma kathisma, PsalmLink psalmLink)
        {
            var lastSlava = kathisma.SlavaElements.LastOrDefault();
            if (lastSlava == null)
            {
                lastSlava = kathisma.AppendNewSlava();
            }

            //ищем существующую PsalmLink
            var found = lastSlava.PsalmLinks.FirstOrDefault(c => c.Psalm.Number == psalmLink.Psalm.Number);
            if (found != null)
            {
                //обновляем
                found.StartStihos = psalmLink.StartStihos;
                found.EndStihos = psalmLink.EndStihos;
            }
            else
            {
                //добавялем
                lastSlava.PsalmLinks.Add(psalmLink);
            }
            
        }

        /// <summary>
        /// Добавляет новый элемент Славы к Кафизме
        /// </summary>
        /// <param name="kathisma"></param>
        /// <returns>Добавленную Славу</returns>
        public static SlavaElement AppendNewSlava(this Kathisma kathisma)
        {
            SlavaElement result = null;
            if (kathisma.SlavaElements.Count < 3)
            {
                result = new SlavaElement();
                kathisma.SlavaElements.Add(result);
            }
            else
            {
                //пошла 4-ая Слава - выдавать исключение?
            }
            return result;
        }

        public static void Merge(this List<BookStihos> list, BookStihos stihos)
        {
            var foundStihos = list.FirstOrDefault(c => c.StihosNumber == stihos.StihosNumber);

            if (foundStihos == null)
            {
                //просто добавляем стих в коллекцию
                list.Add(stihos);
            }
            else
            {
                foundStihos.Merge(stihos);
            }
        }

        public static void Merge(this ItemText oldItem, ItemText newItem)
        {
            //добавляем или обновляем локализованные значения
            foreach (var item in newItem.Items)
            {
                oldItem.AddOrUpdate(item);
            }
        }
    }
}
