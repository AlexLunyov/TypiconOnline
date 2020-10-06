using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.Domain.Extensions
{
    public static class CloneExtensions
    {
        /// <summary>
        /// Копирует дочерние коллекции и свойство PrintDayDefaultTemplate в указанный объект
        /// </summary>
        /// <param name="source"></param>
        /// <param name="version"></param>
        public static void CopyChildrenTo(this TypiconVersion source, TypiconVersion version)
        {
            CloneTypiconVariables(version, source.TypiconVariables);

            ClonePrintTemplates(version, source);

            CloneCommonRules(version, source.CommonRules);
            CloneExplicitAddRules(version, source.ExplicitAddRules);
            CloneKathismas(version, source.Kathismas);

            CloneScheduleSettings(version, source.ScheduleSettings);

            CloneSignsAndDayRules(version, source);
        }

        /// <summary>
        /// Копирует данный объект в указанный
        /// </summary>
        /// <param name="source"></param>
        /// <param name="version"></param>
        public static void CopyTo(this TypiconVersion source, TypiconVersion version)
        {
            version.BDate = source.BDate;
            version.CDate = DateTime.Now;
            version.EDate = source.EDate;
            version.IsModified = source.IsModified;
            version.IsTemplate = source.IsTemplate;
            version.ValidationStatus = source.ValidationStatus;
            version.Name = new ItemText(source.Name);
            version.Description = new ItemText(source.Description);
            version.VersionNumber = source.VersionNumber;
            version.PrevVersionId = source.Id;

            CloneTypiconVariables(version, source.TypiconVariables);

            ClonePrintTemplates(version, source);

            CloneCommonRules(version, source.CommonRules);
            CloneExplicitAddRules(version, source.ExplicitAddRules);
            CloneKathismas(version, source.Kathismas);

            CloneScheduleSettings(version, source.ScheduleSettings);

            CloneSignsAndDayRules(version, source);
        }

        /// <summary>
        /// Возвращает клонированную копию объекта без вложенных коллекций
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TypiconVersion Clone(this TypiconVersion source, bool deep = false)
        {
            var t = new TypiconVersion()
            {
                BDate = source.BDate,
                CDate = DateTime.Now,
                EDate = source.EDate,
                IsModified = source.IsModified,
                IsTemplate = source.IsTemplate,
                ValidationStatus = source.ValidationStatus,
                Name = new ItemText(source.Name),
                Description = new ItemText(source.Description),
                VersionNumber = source.VersionNumber,
                PrevVersionId = source.Id
            };

            if (deep)
            {
                source.CopyChildrenTo(t);
            }

            return t;
        }

        private static void CloneScheduleSettings(TypiconVersion version, ScheduleSettings scheduleSettings)
        {
            if (scheduleSettings != null)
            {
                version.ScheduleSettings = new ScheduleSettings()
                {
                    IsMonday = scheduleSettings.IsMonday,
                    IsTuesday = scheduleSettings.IsTuesday,
                    IsWednesday = scheduleSettings.IsWednesday,
                    IsThursday = scheduleSettings.IsThursday,
                    IsFriday = scheduleSettings.IsFriday,
                    IsSaturday = scheduleSettings.IsSaturday,
                    IsSunday = scheduleSettings.IsSunday
                };

                version.ScheduleSettings.IncludedDates.AddRange(scheduleSettings.IncludedDates);
                version.ScheduleSettings.ExcludedDates.AddRange(scheduleSettings.ExcludedDates);
            }
        }

        private static void ClonePrintTemplates(TypiconVersion version, TypiconVersion source)
        {
            //week
            version.PrintWeekTemplate = (source.PrintWeekTemplate is PrintWeekTemplate w)
                ? new PrintWeekTemplate()
                {
                    DaysPerPage = w.DaysPerPage,
                    PrintFile = w.PrintFile,
                    PrintFileName = w.PrintFileName,
                    TypiconVersion = version
                }
                : null;

            //days
            version.PrintDayTemplates
                .AddRange(source.PrintDayTemplates
                    .Select(c =>
                    {
                        var r = new PrintDayTemplate()
                        {
                            Name = c.Name,
                            Number = c.Number,
                            PrintFile = c.PrintFile,
                            PrintFileName = c.PrintFileName,
                            Icon = c.Icon,
                            IsRed = c.IsRed,
                            TypiconVersion = version
                        };
                        //default
                        if (source.PrintDayDefaultTemplate == c)
                        {
                            version.PrintDayDefaultTemplate = r;
                        }

                        return r;
                    }));
        }

        private static void CloneSignsAndDayRules(TypiconVersion versionTo, TypiconVersion versionFrom, Sign oldTemplate = null, Sign newTemplate = null)
        {
            //находим все дочерние знаки служб
            var signs = versionFrom.Signs.Where(c => c.Template == oldTemplate);

            foreach (var sign in signs)
            {
                var newSign = new Sign()
                {
                    IsAddition = sign.IsAddition,
                    ModRuleDefinition = sign.ModRuleDefinition,

                    PrintTemplate = (sign.PrintTemplate != null)
                        ? versionTo.PrintDayTemplates.First(c => c.Number == sign.PrintTemplate.Number)
                        : default,

                    Priority = sign.Priority,
                    RuleDefinition = sign.RuleDefinition,
                    SignName = new ItemText(sign.SignName),
                    Template = newTemplate,
                    TypiconVersion = versionTo
                };
                //запускаем рекурсивно тот же метод для тех правил, у кого шаблоном является sign
                CloneSignsAndDayRules(versionTo, versionFrom, sign, newSign);

                versionTo.Signs.Add(newSign);

                //VariableLinks
                sign.VariableLinks.CloneVariableModRuleLinks(versionTo, newSign);

                //PrintTemplateLinks
                sign.PrintTemplateLinks.ClonePrintTemplateLinks(versionTo, newSign);

                //ScheduleSettings
                CloneScheduleRules(sign
                        , newSign
                        , versionFrom.ScheduleSettings.Signs
                        , versionTo.ScheduleSettings.Signs
                        , versionTo.ScheduleSettings);
            }

            //находим MenologyRules
            var menologies = versionFrom.MenologyRules.Where(c => c.Template == oldTemplate);

            foreach (var oldMenology in menologies)
            {
                var newMenology = new MenologyRule()
                {
                    Date = new ItemDate(oldMenology.Date),
                    IsAddition = oldMenology.IsAddition,
                    LeapDate = new ItemDate(oldMenology.LeapDate),
                    ModRuleDefinition = oldMenology.ModRuleDefinition,
                    RuleDefinition = oldMenology.RuleDefinition,
                    Template = newTemplate,
                    TypiconVersion = versionTo
                };

                oldMenology.DayRuleWorships.ForEach(c =>
                {
                    newMenology.DayRuleWorships.Add(new DayRuleWorship()
                    {
                        DayRule = newMenology,
                        DayWorshipId = c.DayWorshipId,
                        Order = c.Order
                    });
                });

                versionTo.MenologyRules.Add(newMenology);

                //VariableLinks
                oldMenology.VariableLinks.CloneVariableModRuleLinks(versionTo, newMenology);

                //PrintTemplateLinks
                oldMenology.PrintTemplateLinks.ClonePrintTemplateLinks(versionTo, newMenology);

                //ScheduleSettings
                if (versionFrom.ScheduleSettings != null)
                {
                    CloneScheduleRules(oldMenology
                        , newMenology
                        , versionFrom.ScheduleSettings.MenologyRules
                        , versionTo.ScheduleSettings.MenologyRules
                        , versionTo.ScheduleSettings);
                }
            }

            //находим TriodionRules
            var triodions = versionFrom.TriodionRules.Where(c => c.Template == oldTemplate);

            foreach (var oldTriodion in triodions)
            {
                var newTriodion = new TriodionRule()
                {
                    DaysFromEaster = oldTriodion.DaysFromEaster,
                    IsAddition = oldTriodion.IsAddition,
                    IsTransparent = oldTriodion.IsTransparent,
                    ModRuleDefinition = oldTriodion.ModRuleDefinition,
                    RuleDefinition = oldTriodion.RuleDefinition,
                    Template = newTemplate,
                    TypiconVersion = versionTo
                };

                oldTriodion.DayRuleWorships.ForEach(c =>
                {
                    newTriodion.DayRuleWorships.Add(new DayRuleWorship()
                    {
                        DayRule = newTriodion,
                        DayWorshipId = c.DayWorshipId,
                        Order = c.Order
                    });
                });

                versionTo.TriodionRules.Add(newTriodion);

                //VariableLinks
                oldTriodion.VariableLinks.CloneVariableModRuleLinks(versionTo, newTriodion);

                //PrintTemplateLinks
                oldTriodion.PrintTemplateLinks.ClonePrintTemplateLinks(versionTo, newTriodion);

                //ScheduleSettings
                CloneScheduleRules(oldTriodion
                        , newTriodion
                        , versionFrom.ScheduleSettings.TriodionRules
                        , versionTo.ScheduleSettings.TriodionRules
                        , versionTo.ScheduleSettings);
            }
        }

        private static void CloneKathismas(TypiconVersion version, List<Kathisma> kathismas)
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
                            Psalm = p.Psalm,
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

        private static void CloneExplicitAddRules(TypiconVersion versionTo, List<ExplicitAddRule> explicitAddRules)
        {
            explicitAddRules.ForEach(c =>
            {
                var rule = new ExplicitAddRule()
                {
                    Date = c.Date,
                    RuleDefinition = c.RuleDefinition,
                    TypiconVersion = versionTo
                };

                versionTo.ExplicitAddRules.Add(rule);

                //VariableLinks
                c.VariableLinks.CloneVariableRuleLinks(versionTo, rule);
            });
        }

        private static void CloneCommonRules(TypiconVersion versionTo, List<CommonRule> commonRules)
        {
            commonRules.ForEach(c =>
            {
                var rule = new CommonRule()
                {
                    Name = c.Name,
                    RuleDefinition = c.RuleDefinition,
                    TypiconVersion = versionTo
                };

                versionTo.CommonRules.Add(rule);

                //VariableLinks
                c.VariableLinks.CloneVariableRuleLinks(versionTo, rule);
            });
        }

        private static void CloneTypiconVariables(TypiconVersion version, List<TypiconVariable> typiconVariables)
        {
            typiconVariables.ForEach(c =>
            {
                var entity = new TypiconVariable()
                {
                    Name = c.Name,
                    Type = c.Type,
                    Header = c.Header,
                    Description = c.Description,
                    TypiconVersion = version,
                    Value = c.Value
                };

                version.TypiconVariables.Add(entity);
            });
        }

        private static void CloneVariableRuleLinks<T>(this List<VariableRuleLink<T>> links, TypiconVersion version, T rule) where T : RuleEntity, new()
        {
            links.ForEach(link =>
            {
                var newVariable = version.TypiconVariables
                    .First(v => v.Name == link.Variable.Name
                                      && v.Type == link.Variable.Type);

                newVariable.AddLink(rule);
            });
        }

        private static void CloneVariableModRuleLinks<T>(this List<VariableModRuleLink<T>> links, TypiconVersion version, T rule) where T : ModRuleEntity, new()
        {
            links.ForEach(link =>
            {
                var newVariable = version.TypiconVariables
                    .First(v => v.Name == link.Variable.Name
                                      && v.Type == link.Variable.Type);

                newVariable.AddLink(rule, link.DefinitionType);
            });
        }

        private static void ClonePrintTemplateLinks<T>(this List<PrintTemplateModRuleLink<T>> links, TypiconVersion version, T rule) where T : ModRuleEntity, new()
        {
            links.ForEach(link =>
            {
                var newPrintTemplate = version.PrintDayTemplates
                    .First(v => v.Number == link.Template.Number);

                newPrintTemplate.AddLink(rule);
            });
        }

        /// <summary>
        /// клонирует связанные элементы для графика богослужений ScheduleSettings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rule"></param>
        /// <param name=""></param>
        private static void CloneScheduleRules<T>(this T oldRule
            , T newRule
            , List<ModRuleEntitySchedule<T>> oldList
            , List<ModRuleEntitySchedule<T>> newList
            , ScheduleSettings newSettings) where T : ModRuleEntity, new()
        {
            if (newSettings != null)
            {
                if (oldList.Any(c => c.RuleId == oldRule.Id))
                {
                    newList.Add(new ModRuleEntitySchedule<T>() { Rule = newRule, ScheduleSettings = newSettings });
                }
            }
        }

    }
}
