using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.AppServices.Migration.Psalter
{
    public class PsalmsMigrationManager
    {
        public IPsalterService Service { get; private set; }

        public PsalmsMigrationManager(IPsalterService service)
        {
            Service = service ?? throw new ArgumentNullException("IPsalterService in PsalmsMigrationManager");
        }

        public void MigratePsalms(IPsalterReader reader)
        {
            if (reader == null) throw new ArgumentNullException("IPsalterReader in MigratePsalms");

            Psalm currentPsalm = null;

            //IPsalterElement currentElement = null;

            while (reader.Read())
            {
                switch (reader.ElementType)
                {
                    case PsalterElementKind.Kathisma:
                        //не нужна- ничего не делаем
                        break;
                    case PsalterElementKind.Psalm:
                        //если до этого читали Псалом, то добавляем его в базу
                        if (currentPsalm != null)
                        {
                            InsertOrUpdatePsalm(currentPsalm);
                        }
                        currentPsalm = GetPsalm(reader.Element as Psalm);
                        break;
                    case PsalterElementKind.PsalmAnnotation:
                        currentPsalm.GetElement().Annotation.Merge(reader.Element as BookStihos);
                        break;
                    case PsalterElementKind.PsalmText:
                        currentPsalm.GetElement().Text.Merge(reader.Element as BookStihos);
                        break;
                    case PsalterElementKind.Slava:
                        //не нужна- ничего не делаем
                        if (currentPsalm != null)
                        {
                            InsertOrUpdatePsalm(currentPsalm);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// В случае отсутствия Псалма с таким номером в БД добавляет его
        /// </summary>
        /// <param name="currentPsalm"></param>
        private void InsertOrUpdatePsalm(Psalm currentPsalm)
        {
            var response = Service.Get(new GetPsalmRequest() { Number = currentPsalm.Number });

            currentPsalm.Definition = GetDefinition(currentPsalm);

            if (response.Psalm == null)
            {
                Service.Add(new AddPsalmRequest() { Psalm = currentPsalm });
            }
            else
            {
                Service.Update(new UpdatePsalmRequest() { Psalm = currentPsalm });
            }
        }

        private string GetDefinition(Psalm psalm)
        {
            return new TypiconSerializer().Serialize(psalm.GetElement() ?? new BookReading()); 
        }

        /// <summary>
        /// Возвращает либо существующий Псалом из БД либо новый из Reader-a
        /// </summary>
        /// <param name="psalm"></param>
        /// <returns></returns>
        private Psalm GetPsalm(Psalm psalm)
        {
            var response = Service.Get(new GetPsalmRequest() { Number = psalm.Number });
            Psalm result = response.Psalm ?? psalm;

            result.Definition = GetDefinition(result);

            return result;
        }
    }
}
