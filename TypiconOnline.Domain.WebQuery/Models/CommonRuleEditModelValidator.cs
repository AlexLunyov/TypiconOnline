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
    public class CommonRuleEditModelValidator : RuleModelValidatorBase<CommonRuleEditModel>
    {
        public CommonRuleEditModelValidator(IRuleSerializerRoot ruleSerializer, TypiconDBContext dbContext) : base(ruleSerializer) 
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected TypiconDBContext DbContext { get; }

        public override IEnumerable<ValidationResult> Validate(CommonRuleEditModel model)
        {
            var errors = ValidateRule(model);

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new ValidationResult($"Наименование обязательно для заполнения", new List<string>() { "Name" }));
            }
            else
            {
                //проверяем на уникальность DaysFromEaster
                CommonRule found = null;

                if (model.Mode == ModelMode.Create)
                {
                    //если объект создается, Id = Id версии Устава,
                    //значит ищем CommonRule с таким же Name
                    found = DbContext.Set<CommonRule>()
                        .FirstOrDefault(c => c.Name == model.Name
                                             && c.TypiconVersion.TypiconId == model.Id
                                             && c.TypiconVersion.BDate == null
                                             && c.TypiconVersion.EDate == null);
                }
                else
                {
                    //Edit
                    //сначала находим сам редактируемый объект
                    found = DbContext.Set<CommonRule>().FirstOrDefault(c => c.Id == model.Id);

                    if (found?.Name == model.Name)
                    {
                        //если значения DaysFromEaster найденного объекта и редактируемой модели равны,
                        //значит ошибки нет
                        found = null;
                    }
                    else if (found != null)
                    {
                        //ищем дальше
                        found = DbContext.Set<CommonRule>()
                            .FirstOrDefault(c => c.TypiconVersionId == found.TypiconVersionId
                                && c.Name == model.Name);
                    }
                }
                if (found != null)
                {
                    errors.Add(new ValidationResult("В Уставе уже есть Общее Правило с заданным Наименованием", new List<string>() { "Name" }));
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
