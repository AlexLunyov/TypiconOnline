using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Exceptions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.AppServices.Migration.Psalter
{
    public class KathismasMigrationManager
    {
        public IPsalterContext Context { get; private set; }

        public KathismasMigrationManager(IPsalterContext context)
        {
            Context = context ?? throw new ArgumentNullException("IPsalterContext in KathismasMigrationManager");
        }

        /// <summary>
        /// Осуществляет миграцию Кафизм на основании текстовых документов, а также существующих в БД псалмов
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typiconEntity"></param>
        /// <exception cref="ResourceNotFoundException">В случае отсутствия Псалмов в БД</exception>
        public void MigrateKathismas(IPsalterReader reader, TypiconEntity typiconEntity)
        {
            if (reader == null) throw new ArgumentNullException("IPsalterReader in MigrateKathismas");
            if (typiconEntity == null) throw new ArgumentNullException("TypiconEntity in MigrateKathismas");

            DeleteAllKathismas(typiconEntity);

            Kathisma kathisma = null;
            PsalmLink psalmLink = null;

            IPsalterElement currentElement = null;

            while (reader.Read())
            {
                switch (reader.ElementType)
                {
                    case PsalterElementKind.Kathisma:
                        currentElement = reader.Element;
                        kathisma = typiconEntity.AppendKathisma(reader.Element as Kathisma);
                        break;
                    case PsalterElementKind.Psalm:
                        //если до этого читали Псалом, то добавляем его, очищая ссылки на стихи
                        if (currentElement is PsalmLink p)
                        {
                            ClearStihosIndexes(p);
                            kathisma?.AppendPsalmLink(p);
                        }
                        currentElement = reader.Element;
                        psalmLink = CreatePsalmLink((reader.Element as Psalm).Number);
                        break;
                    case PsalterElementKind.PsalmAnnotation:
                        //не нужна- ничего не делаем
                        break;
                    case PsalterElementKind.PsalmText:
                        if (currentElement == null)
                        {
                            //значит пришли сюда после чтения "Славы"
                            //создаем новую ссылку на Псалом (17 кафизма)
                            psalmLink = CreatePsalmLink(psalmLink);
                        }
                        psalmLink?.AppendStihos(reader.Element as PsalmStihos);
                        currentElement = psalmLink;
                        break;
                    case PsalterElementKind.Slava:
                        kathisma?.AppendNewSlava();
                        if (currentElement is PsalmLink pl)
                        {
                            kathisma?.AppendPsalmLink(pl);
                        }
                        currentElement = null;
                        break;
                }
            }
        }

        private PsalmLink CreatePsalmLink(int number)
        {
            var response = Context.Get(new GetPsalmRequest() { Number = number });

            if (response.Exception == null)
            {
                throw new ResourceNotFoundException("Не найден Псалом в выбранном контексте.", response.Exception);
            }

            return new PsalmLink()
            {
                Psalm = response.Psalm
            };
        }

        private PsalmLink CreatePsalmLink(PsalmLink psalmLink)
        {
            return new PsalmLink()
            {
                Psalm = psalmLink.Psalm
            };
        }

        /// <summary>
        /// Очищает ссылки на стихи - значит, что ссылаемся на Псалом полностью
        /// </summary>
        /// <param name="psalmLink"></param>
        private void ClearStihosIndexes(PsalmLink psalmLink)
        {
            psalmLink.StartStihos = null;
            psalmLink.EndStihos = null;
        }

        private void DeleteAllKathismas(TypiconEntity typiconEntity)
        {
            typiconEntity.Kathismas.ForEach(c => c.TypiconEntity = null);
            typiconEntity.Kathismas?.Clear();
        }
    }
}
