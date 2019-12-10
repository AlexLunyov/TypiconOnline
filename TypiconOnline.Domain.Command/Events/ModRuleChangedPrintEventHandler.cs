using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Events;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Events;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Events
{
    /// <summary>
    /// Обработчик синхронизирует ссылки на Печатные шаблоны Устава
    /// </summary>
    public class ModRuleChangedPrintEventHandler : IDomainEventHandler<RuleModDefinitionChangedEvent>
    {
        public ModRuleChangedPrintEventHandler(/*TypiconDBContext dbContext
            , */CollectorSerializerRoot serializerRoot)
        {
            //DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            SerializerRoot = serializerRoot ?? throw new ArgumentNullException(nameof(serializerRoot));
        }
        
        //protected TypiconDBContext DbContext { get; }

        protected CollectorSerializerRoot SerializerRoot { get; }

        public Result Execute(RuleModDefinitionChangedEvent ev)
        {
            //1. получаем все шаблоны
            var printTemplates = ev.Entity.TypiconVersion.PrintDayTemplates;

            //2. Очищаем все ссылки на PrintDayTemplate для данной сущности
            printTemplates.ForEach(c => c.ClearLinks(ev.Entity));

            //3. Находим ModifyDay, где определен Number
            var localTemplates = GetLocalPrintTemplates(SerializerRoot, ev.NewDefinition);

            foreach (var number in localTemplates)
            {
                //4. Ищем PrintDayTemplate с таким номером.
                var found = printTemplates.FirstOrDefault(c => c.Number == number);

                if (found == null)
                {
                    //a. Если не находим, создаем новый PrintDayTemplate с пустым файлом
                    found = new PrintDayTemplate()
                    {
                        TypiconVersion = ev.Entity.TypiconVersion,
                        Number = number
                    };

                    //b. Добавляем его к TypiconVersion, которая указана у сущности
                    ev.Entity.TypiconVersion.PrintDayTemplates.Add(found);
                }

                found.AddLink(ev.Entity);
            }

            //удаляем Печатные шаблоны Устава, у которых нет ссылок и которые не содержат загруженного файла
            ev.Entity.TypiconVersion.PrintDayTemplates
                .RemoveAll(c => c.IsAutoDeletable());

            return Result.Ok();
        }

        private static IEnumerable<int> GetLocalPrintTemplates(CollectorSerializerRoot serializerRoot, string definition)
        {
            serializerRoot.ClearElements();
            serializerRoot.Container<RootContainer>().Deserialize(definition);

            return serializerRoot.GetElements<ModifyDay>()
                .Where(c => c.SignNumber.HasValue)
                .Select(c => c.SignNumber.Value)
                .Distinct();
        }
    }
}
