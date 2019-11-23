using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    /// <summary>
    /// Конвертирует проекцию Устава в доменный сущность вместе со всеми вложенными коллекциями
    /// </summary>
    public class TypiconImportProjector : IProjector<TypiconVersionProjection, TypiconEntity>
    {
        private readonly CollectorSerializerRoot _serializerRoot;
        public TypiconImportProjector(CollectorSerializerRoot serializerRoot)
        {
            _serializerRoot = serializerRoot ?? throw new ArgumentNullException(nameof(serializerRoot));
        }

        public Result<TypiconEntity> Project(TypiconVersionProjection projection)
        {
            try
            {
                //Создаем TypiconEntity
                var entity = new TypiconEntity()
                {
                    Name = projection.Name,
                    DefaultLanguage = projection.DefaultLanguage,
                    Status = TypiconStatus.Draft,
                    OwnerId = projection.OwnerId
                };

                ////Не импортируем редакторов
                //entity.EditableUserTypicons = projection.Editors?
                //    .Select(userId => new UserTypicon() 
                //    { 
                //        Typicon = entity,
                //        UserId = userId
                //    }).ToList();

                //Создаем Черновик
                var version = new TypiconVersion()
                {
                    Typicon = entity,
                    CDate = DateTime.Now,
                    IsTemplate = projection.IsTemplate,
                    VersionNumber = 1,
                    IsModified = true
                };

                ImportSignsAndDayRules(version, projection);
                ImportCommonRules(version, projection.CommonRules);
                ImportExplicitAddRules(version, projection.ExplicitAddRules);
                ImportKathismas(version, projection.Kathismas);

                //clean projection's Signs & Variables Ids
                //version.Signs.ForEach(c => c.Id = 0);

                entity.Versions.Add(version);

                return Result.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result.Fail<TypiconEntity>(ex.Message);
            }
        }

        private void ImportKathismas(TypiconVersion version, List<KathismaProjection> kathismas)
        {
            kathismas.ForEach(c =>
            {
                var kathisma = new Kathisma()
                {
                    Number = c.Number,
                    NumberName = new ItemText(c.NumberName),
                    TypiconVersion = version
                };

                c.SlavaElements.ForEach(s =>
                {
                    var slavaElement = new SlavaElement();

                    s.PsalmLinks.ForEach(p =>
                    {
                        var psalmLink = new PsalmLink()
                        {
                            PsalmId = p.PsalmId,
                            StartStihos = p.StartStihos,
                            EndStihos = p.EndStihos
                        };
                        slavaElement.PsalmLinks.Add(psalmLink);
                    });

                    kathisma.SlavaElements.Add(slavaElement);
                });

                version.Kathismas.Add(kathisma);
            });
        }

        private void ImportExplicitAddRules(TypiconVersion version, List<ExplicitAddRuleProjection> explicitAddRules)
        {
            explicitAddRules.ForEach(c =>
            {
                var rule = new ExplicitAddRule()
                {
                    Date = c.Date,
                    RuleDefinition = c.RuleDefinition,
                    TypiconVersion = version
                };

                version.ExplicitAddRules.Add(rule);

                //Синхронизируем Переменные Устава
                rule.SyncRuleVariables(_serializerRoot);
            });
        }

        private void ImportCommonRules(TypiconVersion version, List<CommonRuleProjection> commonRules)
        {
            commonRules.ForEach(c =>
            {
                var rule = new CommonRule()
                {
                    Name = c.Name,
                    RuleDefinition = c.RuleDefinition,
                    TypiconVersion = version
                };

                version.CommonRules.Add(rule);

                //Синхронизируем Переменные Устава
                rule.SyncRuleVariables(_serializerRoot);
            });
        }

        private void ImportSignsAndDayRules(TypiconVersion version, TypiconVersionProjection projection, Sign template = null, int? signId = null)
        {
            //находим все дочерние знаки служб
            var signs = projection.Signs.Where(c => c.TemplateId == signId);

            foreach (var sign in signs)
            {
                var newSign = new Sign()
                {
                    IsAddition = sign.IsAddition,
                    ModRuleDefinition = sign.ModRuleDefinition,
                    Number = sign.Number,
                    Priority = sign.Priority,
                    RuleDefinition = sign.RuleDefinition,
                    SignName = sign.Name,
                    Template = template,
                    TypiconVersion = version
                };

                version.Signs.Add(newSign);

                //Синхронизируем Переменные Устава
                newSign.SyncRuleVariables(_serializerRoot);
                newSign.SyncModRuleVariables(_serializerRoot);

                //запускаем рекурсивно тот же метод для тех правил, у кого шаблоном является sign
                ImportSignsAndDayRules(version, projection, newSign, sign.Id);
            }

            //находим Menology Projections
            var menologies = projection.MenologyRules.Where(c => c.TemplateId == signId);
            
            foreach (var oldMenology in menologies)
            {
                var newMenology = new MenologyRule()
                {
                    Template = template,
                    TypiconVersion = version,
                    Date = new ItemDate(oldMenology.Date),
                    LeapDate = new ItemDate(oldMenology.LeapDate),
                    IsAddition = oldMenology.IsAddition,
                    ModRuleDefinition = oldMenology.ModRuleDefinition,
                    RuleDefinition = oldMenology.RuleDefinition,
                };

                //Добавляем ссылки на Тексты служб
                newMenology.DayRuleWorships = oldMenology.DayWorships
                        .Select(c => new DayRuleWorship()
                        {
                            DayRule = newMenology,
                            DayWorshipId = c.WorshipId,
                            Order = c.Order
                        })
                        .ToList();

                version.MenologyRules.Add(newMenology);

                //Синхронизируем Переменные Устава
                newMenology.SyncRuleVariables(_serializerRoot);
                newMenology.SyncModRuleVariables(_serializerRoot);
            }

            //находим Triodion Projections
            var triodions = projection.TriodionRules.Where(c => c.TemplateId == signId);

            foreach (var oldTriodion in triodions)
            {
                var newTriodion = new TriodionRule()
                {
                    Template = template,
                    TypiconVersion = version,
                    DaysFromEaster = oldTriodion.DaysFromEaster,
                    IsAddition = oldTriodion.IsAddition,
                    IsTransparent = oldTriodion.IsTransparent,
                    ModRuleDefinition = oldTriodion.ModRuleDefinition,
                    RuleDefinition = oldTriodion.RuleDefinition,
                };

                //Добавляем ссылки на Тексты служб
                newTriodion.DayRuleWorships = oldTriodion.DayWorships
                        .Select(c => new DayRuleWorship()
                        {
                            DayRule = newTriodion,
                            DayWorshipId = c.WorshipId,
                            Order = c.Order
                        })
                        .ToList();

                version.TriodionRules.Add(newTriodion);

                //Синхронизируем Переменные Устава
                newTriodion.SyncRuleVariables(_serializerRoot);
                newTriodion.SyncModRuleVariables(_serializerRoot);
            }
        }

    }
}
