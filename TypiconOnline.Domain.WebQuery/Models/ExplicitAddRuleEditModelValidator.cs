using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class ExplicitAddRuleEditModelValidator : RuleModelValidatorBase<ExplicitAddRuleEditModel>
    {
        public ExplicitAddRuleEditModelValidator(IRuleSerializerRoot ruleSerializer, TypiconDBContext dbContext) : base(ruleSerializer)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected TypiconDBContext DbContext { get; }

        public override IEnumerable<ValidationResult> Validate(ExplicitAddRuleEditModel model)
        {
            var errors = ValidateRule(model);

            if (model.Date.Date == DateTime.MinValue.Date)
            {
                errors.Add(new ValidationResult($"Дата обязательна для заполнения", new List<string>() { "Date" }));
            }
            else
            {
                //проверяем на уникальность Date
                ExplicitAddRule found = null;

                if (model.Mode == ModelMode.Create)
                {
                    //если объект создается, Id = Id версии Устава,
                    //значит ищем CommonRule с таким же Name
                    found = DbContext.Set<ExplicitAddRule>()
                        .FirstOrDefault(c => c.Date.Date == model.Date.Date
                                             && c.TypiconVersion.TypiconId == model.Id
                                             && c.TypiconVersion.BDate == null
                                             && c.TypiconVersion.EDate == null);
                }
                else
                {
                    //Edit
                    //сначала находим сам редактируемый объект
                    found = DbContext.Set<ExplicitAddRule>().FirstOrDefault(c => c.Id == model.Id);

                    if (found?.Date.Date == model.Date.Date)
                    {
                        //если значения DaysFromEaster найденного объекта и редактируемой модели равны,
                        //значит ошибки нет
                        found = null;
                    }
                    else if (found != null)
                    {
                        //ищем дальше
                        found = DbContext.Set<ExplicitAddRule>()
                            .FirstOrDefault(c => c.TypiconVersionId == found.TypiconVersionId
                                && c.Date.Date == model.Date.Date);
                    }
                }
                if (found != null)
                {
                    errors.Add(new ValidationResult("В Уставе уже есть Явное Правило с заданной датой", new List<string>() { "Date" }));
                }
            }

            if (string.IsNullOrEmpty(model.RuleDefinition))
            {
                errors.Add(new ValidationResult($"Правило для последовательности обязательно для заполнения", new List<string>() { "RuleDefinition" }));
            }

            return errors;
        }
    }
}
