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
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations
{
    public class TypiconEntityService : ITypiconEntityService
    {
        private IUnitOfWork unitOfWork;

        public TypiconEntityService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("_unitOfWork");
        }

        /// <summary>
        /// Удаляет все переходящие праздники у Устава с заданным Id
        /// </summary>
        /// <param name="id">Id Устава</param>
        public void ClearModifiedYears(int id)
        {
            GetTypiconEntityResponse response = GetTypiconEntity(id);

            if (response.TypiconEntity != null)
            {
                //response.TypiconEntity.ModifiedYears.ForEach(c =>
                //{
                //    //c.ModifiedRules.ForEach(d => d.ShortName = null);
                //    c.ModifiedRules.Clear();
                //});

                response.TypiconEntity.ModifiedYears.Clear();

                unitOfWork.SaveChanges();
            }
        }

        public GetTypiconEntityResponse GetTypiconEntity(int id)
        {
            var response = new GetTypiconEntityResponse();

            try
            {
                var typicon = unitOfWork.Repository<TypiconEntity>().Get(x => x.Id == id);

                if (typicon == null)
                {
                    response.Exception = GetStandardTypiconNotFoundException();
                }
                else
                {
                    response.TypiconEntity = typicon;
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
            IEnumerable<TypiconEntity> allTypiconEntities = null;

            try
            {
                allTypiconEntities = unitOfWork.Repository<TypiconEntity>().GetAll();
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
            GetTypiconEntityResponse response = GetTypiconEntity(id);

            if (response.TypiconEntity != null)
            {
                string setting = Path.Combine(folderPath, response.TypiconEntity.Name, "Menology");

                FileReader fileReader = new FileReader(setting);

                foreach (MenologyRule rule in response.TypiconEntity.MenologyRules)
                {
                    if (rule.Date.IsEmpty && rule.DateB.IsEmpty)
                    {
                        rule.RuleDefinition = fileReader.Read(rule.Name);
                    }
                    else
                    {
                        rule.RuleDefinition = fileReader.Read(rule.DateB.Expression);
                    }

                }

                setting = Path.Combine(folderPath, response.TypiconEntity.Name, "Triodion");

                fileReader.FolderPath = setting;

                foreach (TriodionRule rule in response.TypiconEntity.TriodionRules)
                {
                    rule.RuleDefinition = fileReader.Read(rule.DaysFromEaster.ToString());
                }

                setting = Path.Combine(folderPath, response.TypiconEntity.Name, "Sign");

                fileReader.FolderPath = setting;

                foreach (Sign sign in response.TypiconEntity.Signs)
                {
                    sign.RuleDefinition = fileReader.Read(sign.Name);
                }

                //commonRules

                ReloadCommonRules(response.TypiconEntity, folderPath);

                unitOfWork.SaveChanges();
            }
        }

        private void ReloadCommonRules(TypiconEntity typiconEntity, string folderPath)
        {
            folderPath = Path.Combine(folderPath, typiconEntity.Name, "Common");

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<(string name, string content)> files = fileReader.ReadAllFromDirectory();

            foreach ((string name, string content) in files)
            {
                CommonRule commonRule = typiconEntity.GetCommonRule(c => c.Name == name);

                if (commonRule == null)
                {
                    commonRule = new CommonRule()
                    {
                        Name = name,
                        RuleDefinition = content,
                        Owner = typiconEntity
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

        public InsertTypiconEntityResponse InsertTypiconEntity(InsertTypiconEntityRequest insertTypiconEntityRequest)
        {
            throw new NotImplementedException();
        }

        public UpdateTypiconEntityResponse UpdateTypiconEntity(UpdateTypiconEntityRequest updateTypiconEntityRequest)
        {
            var response = new UpdateTypiconEntityResponse();

            try
            {
                unitOfWork.Repository<TypiconEntity>().Update(updateTypiconEntityRequest.TypiconEntity);

                unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public DeleteTypiconEntityResponse DeleteTypiconEntity(DeleteTypiconEntityRequest deleteTypiconEntityRequest)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
