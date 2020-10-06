using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    public class TypiconExportProjector : IProjector<TypiconVersion, TypiconVersionProjection>
    {
        public Result<TypiconVersionProjection> Project(TypiconVersion entity)
        {
            try
            {
                var projection = new TypiconVersionProjection()
                {
                    Name = new ItemText(entity.Name),
                    Description = new ItemText(entity.Description),
                    SystemName = entity.Typicon.SystemName,
                    OwnerId = entity.Typicon.OwnerId,
                    Editors = entity.Typicon.EditableUserTypicons.Select(c => c.UserId).ToList(),
                    IsTemplate = entity.IsTemplate,
                    DefaultLanguage = entity.Typicon.DefaultLanguage,
                    CommonRules = entity.CommonRules.Select(c => new CommonRuleProjection()
                    {
                        Name = c.Name,
                        RuleDefinition = c.RuleDefinition,
                        VariableLinks = c.VariableLinks.Select(d => (d.VariableId, DefinitionType.Rule)).ToList()
                    }).ToList(),
                    ExplicitAddRules = entity.ExplicitAddRules.Select(c => new ExplicitAddRuleProjection()
                    {
                        Date = c.Date,
                        RuleDefinition = c.RuleDefinition,
                        VariableLinks = c.VariableLinks.Select(d => (d.VariableId, DefinitionType.Rule)).ToList()
                    }).ToList(),
                    Signs = entity.Signs.Select(c => new SignProjection()
                    {
                        Id = c.Id,
                        TemplateId = c.TemplateId,
                        IsAddition = c.IsAddition,
                        Name = new ItemText(c.SignName),
                        Number = (c.PrintTemplate != null) ? c.PrintTemplate.Number : default,
                        Priority = c.Priority,
                        RuleDefinition = c.RuleDefinition,
                        ModRuleDefinition = c.ModRuleDefinition,
                        VariableLinks = c.VariableLinks.Select(d => (d.VariableId, d.DefinitionType)).ToList()
                    }).ToList(),
                    MenologyRules = entity.MenologyRules.Select(c => new MenologyRuleProjection()
                    {
                        Id = c.Id,
                        TemplateId = c.TemplateId,
                        Date = c.Date.ToString(),
                        LeapDate = c.LeapDate.ToString(),
                        IsAddition = c.IsAddition,
                        DayWorships = c.DayRuleWorships.Select(d => (d.DayWorshipId, d.Order)).ToList(),
                        RuleDefinition = c.RuleDefinition,
                        ModRuleDefinition = c.ModRuleDefinition,
                        VariableLinks = c.VariableLinks.Select(d => (d.VariableId, d.DefinitionType)).ToList()
                    }).ToList(),
                    TriodionRules = entity.TriodionRules.Select(c => new TriodionRuleProjection()
                    {
                        Id = c.Id,
                        TemplateId = c.TemplateId,
                        DaysFromEaster = c.DaysFromEaster,
                        IsAddition = c.IsAddition,
                        IsTransparent = c.IsTransparent,
                        DayWorships = c.DayRuleWorships.Select(d => (d.DayWorshipId, d.Order)).ToList(),
                        RuleDefinition = c.RuleDefinition,
                        ModRuleDefinition = c.ModRuleDefinition,
                        VariableLinks = c.VariableLinks.Select(d => (d.VariableId, d.DefinitionType)).ToList()
                    }).ToList(),
                    Kathismas = entity.Kathismas.Select(c => new KathismaProjection()
                    {
                        Number = c.Number,
                        NumberName = new ItemText(c.NumberName),
                        SlavaElements = c.SlavaElements.Select(d => new SlavaElementProjection()
                        {
                            PsalmLinks = d.PsalmLinks.Select(e => new PsalmLinkProjection()
                            {
                                PsalmId = e.PsalmId,
                                StartStihos = e.StartStihos,
                                EndStihos = e.EndStihos
                            }).ToList()
                        }).ToList()
                    }).ToList(),
                    TypiconVariables = entity.TypiconVariables.Select(c => new TypiconVariableProjection()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Header = c.Header,
                        Description = c.Description,
                        Type = c.Type,
                        Value = c.Value
                    }).ToList(),
                    PrintWeekTemplate = (entity.PrintWeekTemplate != null)
                        ? new PrintWeekTemplateProjection()
                        {
                            DaysPerPage = entity.PrintWeekTemplate.DaysPerPage,
                            PrintFile = entity.PrintWeekTemplate.PrintFile,
                            PrintFileName = entity.PrintWeekTemplate.PrintFileName
                        }
                        : null,
                    PrintDayTemplates = entity.PrintDayTemplates.Select(c => new PrintDayTemplateProjection()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Number = c.Number,
                        PrintFile = c.PrintFile,
                        PrintFileName = c.PrintFileName,
                        Icon = c.Icon
                    }).ToList(),
                    ScheduleSettings = (entity.ScheduleSettings is ScheduleSettings schedule)
                    ? new ScheduleSettingsProjection() 
                    {
                        IsMonday = schedule.IsMonday,
                        IsTuesday = schedule.IsTuesday,
                        IsWednesday = schedule.IsWednesday,
                        IsThursday = schedule.IsThursday,
                        IsFriday = schedule.IsFriday,
                        IsSaturday = schedule.IsSaturday,
                        IsSunday = schedule.IsSunday,
                        Signs = schedule.Signs.Select(c => c.RuleId).ToList(),
                        MenologyRules = schedule.MenologyRules.Select(c => c.RuleId).ToList(),
                        TriodionRules = schedule.TriodionRules.Select(c => c.RuleId).ToList(),
                        IncludedDates = schedule.IncludedDates.ToList(),
                        ExcludedDates = schedule.ExcludedDates.ToList()
                    }
                    : null
                };

                return Result.Ok(projection);
            }
            catch (Exception ex)
            {
                return Result.Fail<TypiconVersionProjection>(ex.Message);
            }
        }
    }
}
