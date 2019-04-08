using System;
using TypiconOnline.AppServices.Exceptions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Elements;
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
        /// <param name="migrateOnlyKathismaName">Только лишь обновляет/добавляет имена у кафизм для заданного языка</param>
        /// <exception cref="ResourceNotFoundException">В случае отсутствия Псалмов в БД</exception>
        public void MigrateKathismas(IPsalterReader reader, TypiconVersion typiconEntity, bool migrateOnlyKathismaName = false)
        {
            if (reader == null) throw new ArgumentNullException("IPsalterReader in MigrateKathismas");
            if (typiconEntity == null) throw new ArgumentNullException("TypiconVersion in MigrateKathismas");

            //Удаляем все кафизмы, если установлен флаг
            //if (clearBeforeMigration)
            //{
            //    DeleteAllKathismas(typiconEntity);
            //}

            Kathisma kathisma = null;
            PsalmLink psalmLink = null;

            IPsalterElement currentElement = null;

            while (reader.Read())
            {
                switch (reader.ElementType)
                {
                    case PsalterElementKind.Kathisma:
                        {
                            currentElement = reader.Element;
                            kathisma = typiconEntity.AppendOrUpdateKathisma(reader.Element as Kathisma);
                            psalmLink = null;
                        }
                        break;
                    case PsalterElementKind.Psalm:
                        {
                            if (!migrateOnlyKathismaName)
                            {
                                //если до этого читали Псалом, то добавляем его, очищая ссылки на стихи
                                if (psalmLink != null)
                                {
                                    AppendAndClearPsalmLink(kathisma, psalmLink);
                                }
                                currentElement = reader.Element;
                                psalmLink = CreatePsalmLink((reader.Element as Psalm).Number);
                            }
                        }
                        break;
                    case PsalterElementKind.PsalmAnnotation:
                        //не нужна- ничего не делаем
                        break;
                    case PsalterElementKind.PsalmText:
                        {
                            if (!migrateOnlyKathismaName)
                            {
                                if (currentElement == null)
                                {
                                    //значит пришли сюда после чтения "Славы"
                                    //добавляем сформированную Ссылку
                                    //и создаем новую ссылку на Псалом (17 кафизма)
                                    if (psalmLink != null)
                                    {
                                        kathisma.AppendOrUpdatePsalmLink(psalmLink);
                                    }
                                    psalmLink = CreatePsalmLink(psalmLink);
                                }
                                psalmLink.AppendStihos(reader.Element as BookStihos);
                                currentElement = psalmLink;
                            }
                        }
                        break;
                    case PsalterElementKind.Slava:
                        {
                            if (!migrateOnlyKathismaName)
                            {
                                //Добавляем новую Славу
                                kathisma.AppendNewSlava();

                                //добавляем Ссылку
                                if (currentElement is PsalmLink p)
                                {
                                    if (kathisma.SlavaElements.Count >= 3)
                                    {
                                        kathisma.AppendOrUpdatePsalmLink(p);
                                    }
                                }

                                currentElement = null;
                            }
                        }
                        break;
                }
            }

            void AppendAndClearPsalmLink(Kathisma k, PsalmLink p)
            {
                //Пока убираем, потому как в текст Псалма будут входить и аннотации
                //ClearStihosIndexes(p);
                k.AppendOrUpdatePsalmLink(p);
            }
        }

        private PsalmLink CreatePsalmLink(int number)
        {
            var response = Context.Get(new GetPsalmRequest() { Number = number });

            if (response.Psalm == null)
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

        private void DeleteAllKathismas(TypiconVersion typiconEntity)
        {
            typiconEntity.Kathismas.ForEach(c => c.TypiconVersion = null);
            typiconEntity.Kathismas?.Clear();
        }
    }
}
