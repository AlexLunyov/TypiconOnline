using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Rules.Days;
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
        public static Kathisma AppendKathisma(this TypiconEntity typiconEntity, Kathisma kathisma)
        {
            typiconEntity.Kathismas.Add(kathisma);
            return kathisma;
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

        public static void AppendPsalmLink(this Kathisma kathisma, PsalmLink psalmLink)
        {
            var lastSlava = kathisma.SlavaElements.LastOrDefault();
            if (lastSlava == null)
            {
                lastSlava = new SlavaElement();
                kathisma.SlavaElements.Add(lastSlava);
            }
            lastSlava.PsalmLinks.Add(psalmLink);
        }

        public static void AppendNewSlava(this Kathisma kathisma)
        {
            if (kathisma.SlavaElements.Count < 3)
            {
                var slava = new SlavaElement();
                kathisma.SlavaElements.Add(slava);
            }
            else
            {
                //пошла 4-ая Слава - выдавать исключение?
            }
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
                //добавляем или обновляем локализованные значения
                foreach (var lang in stihos.Languages)
                {
                    if (foundStihos.ContainsLanguage(lang))
                    {
                        foundStihos[lang] = stihos[lang];
                    }
                    else
                    {
                        foundStihos.AddElement(lang, stihos[lang]);
                    }
                }
            }
        }
    }
}
