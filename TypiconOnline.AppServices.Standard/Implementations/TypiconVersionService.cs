using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Exceptions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations
{
    public class TypiconVersionService : ITypiconVersionService
    {
        private IUnitOfWork unitOfWork;

        public TypiconVersionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("_unitOfWork");
        }

        /*
         * .Include(c => c.Template)
                .Include(c => c.Signs)
                    .ThenInclude(c => c.SignName)
                .Include(c => c.Signs)
                    .ThenInclude(c => c.Template)
                .Include(c => c.CommonRules)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.Date)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DateB)
        */

        private readonly IncludeOptions Includes = new IncludeOptions()
        {
            Includes = new string[]
            {
                "Template",
                "Signs.SignName",
                "Signs.Template",
                "CommonRules",
                "MenologyRules.Date",
                "MenologyRules.DateB",
                "MenologyRules.Template",
                "MenologyRules.DayRuleWorships.DayWorship.WorshipName",
                "MenologyRules.DayRuleWorships.DayWorship.WorshipShortName",
                "MenologyRules.DayRuleWorships.DayWorship.Parent",
                "TriodionRules.Template",
                "TriodionRules.DayRuleWorships.DayWorship.WorshipName",
                "TriodionRules.DayRuleWorships.DayWorship.WorshipShortName",
                "TriodionRules.DayRuleWorships.DayWorship.Parent",
                "ModifiedYears.ModifiedRules.RuleEntity",
                "ModifiedYears.ModifiedRules.Filter",
                "Kathismas.SlavaElements.PsalmLinks.Psalm"
            }
        };

        /// <summary>
        /// Удаляет все переходящие праздники у Устава с заданным Id
        /// </summary>
        /// <param name="id">Id Устава</param>
        public void ClearModifiedYears(int id)
        {
            GetTypiconVersionResponse response = GetTypiconVersion(id);

            if (response.TypiconVersion != null)
            {
                //response.TypiconVersion.ModifiedYears.ForEach(c =>
                //{
                //    //c.ModifiedRules.ForEach(d => d.ShortName = null);
                //    c.ModifiedRules.Clear();
                //});

                response.TypiconVersion.ModifiedYears.Clear();

                unitOfWork.SaveChanges();
            }
        }

        public GetTypiconVersionResponse GetTypiconVersion(int id)
        {
            var response = new GetTypiconVersionResponse();

            try
            {
                var typicon = unitOfWork.Repository<TypiconVersion>().Get(x => x.Id == id, Includes);

                if (typicon == null)
                {
                    response.Exception = GetStandardTypiconNotFoundException();
                }
                else
                {
                    response.TypiconVersion = typicon;
                }
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public GetTypiconEntitiesResponse GetAllTypiconEntities()
        {
            GetTypiconEntitiesResponse getTypiconEntitiesResponse = new GetTypiconEntitiesResponse();
            IEnumerable<TypiconVersion> allTypiconEntities = null;

            try
            {
                allTypiconEntities = unitOfWork.Repository<TypiconVersion>().GetAll();
                getTypiconEntitiesResponse.TypiconEntities = allTypiconEntities;
            }
            catch (Exception ex)
            {
                getTypiconEntitiesResponse.Exception = ex;
            }
            return getTypiconEntitiesResponse;
        }

        public void ReloadRules(int id, string folderPath)
        {
            GetTypiconVersionResponse response = GetTypiconVersion(id);

            if (response.TypiconVersion != null)
            {
                string setting = Path.Combine(folderPath, response.TypiconVersion.Name.ToString(), "Menology");

                FileReader fileReader = new FileReader(setting);

                foreach (MenologyRule rule in response.TypiconVersion.MenologyRules)
                {
                    if (rule.Date.IsEmpty && rule.LeapDate.IsEmpty)
                    {
                        rule.RuleDefinition = fileReader.Read(rule.GetNameByLanguage(response.TypiconVersion.DefaultLanguage));
                    }
                    else
                    {
                        rule.RuleDefinition = fileReader.Read(rule.LeapDate.ToString());
                    }

                }

                setting = Path.Combine(folderPath, response.TypiconVersion.Name.ToString(), "Triodion");

                fileReader.FolderPath = setting;

                foreach (TriodionRule rule in response.TypiconVersion.TriodionRules)
                {
                    rule.RuleDefinition = fileReader.Read(rule.DaysFromEaster.ToString());
                }

                setting = Path.Combine(folderPath, response.TypiconVersion.Name.ToString(), "Sign");

                fileReader.FolderPath = setting;

                foreach (Sign sign in response.TypiconVersion.Signs)
                {
                    sign.RuleDefinition = fileReader.Read(sign.GetNameByLanguage(response.TypiconVersion.DefaultLanguage));
                }

                //commonRules

                ReloadCommonRules(response.TypiconVersion, folderPath);

                unitOfWork.SaveChanges();
            }
        }

        private void ReloadCommonRules(TypiconVersion typiconEntity, string folderPath)
        {
            folderPath = Path.Combine(folderPath, typiconEntity.Name.ToString(), "Common");

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<(string name, string content)> files = fileReader.ReadAllFromDirectory();

            foreach ((string name, string content) in files)
            {
                CommonRule commonRule = typiconEntity.CommonRules.FirstOrDefault(c => c.Name == name);

                if (commonRule == null)
                {
                    commonRule = new CommonRule()
                    {
                        Name = name,
                        RuleDefinition = content,
                        TypiconVersionId = typiconEntity.Id,
                        //Owner = typiconEntity
                    };
                    typiconEntity.CommonRules.Add(commonRule);
                }
                else
                {
                    commonRule.RuleDefinition = content;
                }
            }
        }

        private ResourceNotFoundException GetStandardTypiconNotFoundException()
        {
            return new ResourceNotFoundException("Запрашиваемый Устав не был найден.");
        }

        #region not realized

        public InsertTypiconVersionResponse InsertTypiconVersion(InsertTypiconVersionRequest insertTypiconVersionRequest)
        {
            throw new NotImplementedException();
        }

        public UpdateTypiconVersionResponse UpdateTypiconVersion(UpdateTypiconVersionRequest updateTypiconVersionRequest)
        {
            var response = new UpdateTypiconVersionResponse();

            try
            {
                unitOfWork.Repository<TypiconVersion>().Update(updateTypiconVersionRequest.TypiconVersion);

                unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public DeleteTypiconVersionResponse DeleteTypiconVersion(DeleteTypiconVersionRequest deleteTypiconVersionRequest)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
