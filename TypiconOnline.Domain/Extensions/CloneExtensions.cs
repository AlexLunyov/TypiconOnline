using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.Domain.Extensions
{
    public static class CloneExtensions
    {
        public static TypiconVersion Clone(this TypiconVersion source)
        {
            var version = new TypiconVersion()
            {
                BDate = source.BDate,
                CDate = DateTime.Now,
                EDate = source.EDate,
                IsModified = source.IsModified,
                IsTemplate = source.IsTemplate,
                ValidationStatus = source.ValidationStatus,
                VersionNumber = source.VersionNumber,
                PrevVersionId = source.Id
            };

            CloneTypiconVariables(version, source.TypiconVariables);

            CloneCommonRules(version, source.CommonRules);
            CloneExplicitAddRules(version, source.ExplicitAddRules);
            CloneKathismas(version, source.Kathismas);
            CloneSignsAndDayRules(version, source);

            return version;
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
                    Number = sign.Number,
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
                    Description = c.Description,
                    TypiconVersion = version
                };

                version.TypiconVariables.Add(entity);
            });
        }

        private static void CloneVariableRuleLinks<T>(this List<VariableRuleLink<T>> links, TypiconVersion version, T rule) where T: RuleEntity, new()
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
    }
}
