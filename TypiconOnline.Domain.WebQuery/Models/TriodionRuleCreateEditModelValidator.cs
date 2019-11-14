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
    public class TriodionRuleCreateEditModelValidator : DayRuleModelValidatorBase<TriodionDayWorshipModel>, IValidator<TriodionRuleCreateEditModel>
    {
        public TriodionRuleCreateEditModelValidator(IRuleSerializerRoot ruleSerializer, TypiconDBContext dbContext) : base(ruleSerializer)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected TypiconDBContext DbContext { get; }

        public IEnumerable<ValidationResult> Validate(TriodionRuleCreateEditModel model)
        {
            var errors = ValidateDayRule(model);

            if (model.DaysFromEaster < -363
                || model.DaysFromEaster > 363)
            {
                errors.Add(new ValidationResult("Количество дней от Пасхи должно варьироваться в пределах одного календарного года (-364..364)", new List<string>() { "DayWorships" }));
            }

            //проверяем на уникальность DaysFromEaster
            TriodionRule found = null;

            if (model.Mode == ModelMode.Create)
            {
                //если объект создается, Id = Id версии Устава,
                //значит ищем TriodionRule с таким же DaysFromEaster
                found = DbContext.Set<TriodionRule>()
                    .FirstOrDefault(c => c.DaysFromEaster == model.DaysFromEaster
                                         && c.TypiconVersion.TypiconId == model.Id
                                             && c.TypiconVersion.BDate == null
                                             && c.TypiconVersion.EDate == null);
            }
            else
            {
                //Edit
                //сначала находим сам редактируемый объект
                found = DbContext.Set<TriodionRule>().FirstOrDefault(c => c.Id == model.Id);
                
                if (found?.DaysFromEaster == model.DaysFromEaster)
                {
                    //если значения DaysFromEaster найденного объекта и редактируемой модели равны,
                    //значит ошибки нет
                    found = null;
                }
                else if (found != null)
                {
                    //ищем дальше
                    found = DbContext.Set<TriodionRule>()
                        .FirstOrDefault(c => c.TypiconVersionId == found.TypiconVersionId
                            && c.DaysFromEaster == model.DaysFromEaster);
                }
            }
            if (found != null)
            {
                errors.Add(new ValidationResult("В Уставе уже есть Правило Триоди с заданным значением Количества дней от Пасхи", new List<string>() { "DaysFromEaster" }));
            }

            return errors;
        }
    }
}
