using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.AppServices.Migration;

namespace TypiconOnline.AppServices.Jobs
{
    /// <summary>
    /// Обработчик задания на перезагрузку Правил для указанной версии Устава. <br/>
    /// Для имеющихся Правил Устава заменяет текстовое определение Правил
    /// </summary>
    public class ReloadRulesJobHandler : ICommandHandler<ReloadRulesJob>
    {
        private const string RULES_FOLDER = "rules_folder";

        private readonly IConfigurationRepository _configRepo;
        private readonly TypiconDBContext _dbContext;
        private readonly IJobRepository _jobs;

        public ReloadRulesJobHandler([NotNull] IConfigurationRepository configRepo, TypiconDBContext dbContext, IJobRepository jobs)
        {
            _configRepo = configRepo ?? throw new ArgumentNullException(nameof(configRepo));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        public Task<Result> ExecuteAsync(ReloadRulesJob job)
        {
            _jobs.Start(job);

            var path = GetFolder();

            //находим версию Устава
            var version = _dbContext.GetTypiconVersion(job.TypiconId, job.TypiconVersionStatus);

            Result result = Result.Ok();

            Result.Combine(path, version)
                .OnSuccess(() =>
                {
                    DoTheJob(path.Value, version.Value);
                })
                .OnSuccess(async () => 
                {
                    await _dbContext.UpdateTypiconVersionAsync(version.Value);

                    await _dbContext.ClearModifiedYearsAsync(version.Value.Id);

                    await _dbContext.ClearOutputFormsAsync(version.Value.TypiconId);

                    _jobs.Finish(job, string.Empty);
                })
                .OnFailure(err =>
                {
                    _jobs.Fail(job, err);

                    result = Result.Fail(err);
                });

            return Task.FromResult(result);
        }


        private Result<string> GetFolder()
        {
            try
            {
                return Result.Ok(_configRepo.GetConfigurationValue(RULES_FOLDER));
            }
            catch (Exception ex)
            {
                return Result.Fail<string>(ex.Message);
            }
        }

        private void DoTheJob(string folder, TypiconVersion typiconVersion)
        {
            ReloadMenologyRules(folder, typiconVersion);

            ReloadTriodionRules(folder, typiconVersion);

            ReloadSigns(folder, typiconVersion);

            ReloadCommonRules(folder, typiconVersion);

            ReloadExplicitAddRules(folder, typiconVersion);
        }

        private void ReloadMenologyRules(string folder, TypiconVersion typiconVersion)
        {
            string setting = Path.Combine(folder, typiconVersion.Typicon.ToString(), "Menology");

            var fileReader = new FileReader(setting);

            foreach (MenologyRule rule in typiconVersion.MenologyRules)
            {
                if (rule.Date.IsEmpty && rule.LeapDate.IsEmpty)
                {
                    rule.RuleDefinition = fileReader.Read(rule.GetNameByLanguage(typiconVersion.Typicon.DefaultLanguage));
                }
                else
                {
                    rule.RuleDefinition = fileReader.Read(rule.LeapDate.Expression);
                }

            }
        }

        private void ReloadTriodionRules(string folder, TypiconVersion typiconVersion)
        {
            string setting = Path.Combine(folder, typiconVersion.Typicon.Name.ToString(), "Triodion");

            var fileReader = new FileReader(setting);

            foreach (TriodionRule rule in typiconVersion.TriodionRules)
            {
                rule.RuleDefinition = fileReader.Read(rule.DaysFromEaster.ToString());
            }
        }

        private void ReloadExplicitAddRules(string folder, TypiconVersion typiconVersion)
        {
            string folderPath = Path.Combine(folder, typiconVersion.Typicon.Name.ToString(), "Explicit");

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<(string name, string content)> files = fileReader.ReadAllFromDirectory();

            foreach ((string name, string content) in files)
            {
                if (DateTime.TryParse(name, out DateTime date))
                {
                    var explicitRule = typiconVersion.ExplicitAddRules.FirstOrDefault(c => c.Date == date);

                    if (explicitRule == null)
                    {
                        explicitRule = new ExplicitAddRule()
                        {
                            Date = date,
                            RuleDefinition = content,
                            TypiconVersionId = typiconVersion.Id
                        };
                        typiconVersion.ExplicitAddRules.Add(explicitRule);
                    }
                    else
                    {
                        explicitRule.RuleDefinition = content;
                    }
                }
            }
        }

        private void ReloadCommonRules(string folder, TypiconVersion typiconVersion)
        {
            string folderPath = Path.Combine(folder, typiconVersion.Typicon.Name.ToString(), "Common");

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<(string name, string content)> files = fileReader.ReadAllFromDirectory();

            foreach ((string name, string content) in files)
            {
                CommonRule commonRule = typiconVersion.CommonRules.FirstOrDefault(c => c.Name == name);

                if (commonRule == null)
                {
                    commonRule = new CommonRule()
                    {
                        Name = name,
                        RuleDefinition = content,
                        TypiconVersionId = typiconVersion.Id
                    };
                    typiconVersion.CommonRules.Add(commonRule);
                }
                else
                {
                    commonRule.RuleDefinition = content;
                }
            }
        }

        private void ReloadSigns(string folder, TypiconVersion typiconVersion)
        {
            string setting = Path.Combine(folder, typiconVersion.Typicon.Name.ToString(), "Sign");

            var fileReader = new FileReader(setting);

            foreach (Sign sign in typiconVersion.Signs)
            {
                sign.RuleDefinition = fileReader.Read(sign.GetNameByLanguage(typiconVersion.Typicon.DefaultLanguage));
            }
        }

        private void ClearModifiedYears(TypiconVersion typiconVersion)
        {
            typiconVersion.ModifiedYears.Clear();
        }

        
    }
}
