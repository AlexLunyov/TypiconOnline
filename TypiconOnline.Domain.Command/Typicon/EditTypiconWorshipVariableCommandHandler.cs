using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Domain.Rules.Models;
using System.Text.Json;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditTypiconWorshipVariableCommandHandler : EditRuleCommandHandlerBase<TypiconVariable>, ICommandHandler<EditTypiconWorshipVariableCommand>
    {
        public EditTypiconWorshipVariableCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public Task<Result> ExecuteAsync(EditTypiconWorshipVariableCommand command)
        {
            return Task.FromResult(Execute(command));
        }

        protected override Result UpdateValues(TypiconVariable entity, EditRuleCommandBase<TypiconVariable> command)
        {
            var c = command as EditTypiconWorshipVariableCommand;

            //редактируем только если Устав является Шаблоном
            if (entity.TypiconVersion.IsTemplate)
            {
                entity.Header = c.Header;
                entity.Description = c.Description;
            }

            try
            {
                var worships = JsonSerializer.Deserialize<List<WorshipModel>>(c.Value);

                var xml = "";

                if (worships.Count > 0)
                {
                    xml = SerializerRoot.TypiconSerializer
                    .Serialize(new WorshipContainer()
                    {
                        Worships = worships
                    });
                }

                entity.Value = xml;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Проищла ошибка при десериализации объекта {nameof(WorshipContainer)}");
            }
        }
    }
}
