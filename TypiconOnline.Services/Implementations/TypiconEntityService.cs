using System;
using System.Collections.Generic;
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
        private readonly IUnitOfWork _unitOfWork;

        public TypiconEntityService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("UnitOfWork");
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
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
                while (response.TypiconEntity.ModifiedYears.Count > 0)
                {
                    ModifiedYear year = response.TypiconEntity.ModifiedYears[0];

                    while (year.ModifiedRules.Count > 0)
                    {
                        UnitOfWork.Repository<ModifiedRule>().Delete(year.ModifiedRules[0]);
                    }
                    UnitOfWork.Repository<ModifiedYear>().Delete(year);
                }

                UnitOfWork.Commit();
            }
        }

        public GetTypiconEntityResponse GetTypiconEntity(int id)
        {
            GetTypiconEntityResponse response = new GetTypiconEntityResponse();

            TypiconEntity typicon = null;
            try
            {
                typicon = UnitOfWork.Repository<TypiconEntity>().Get(x => x.Id == id);

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
                allTypiconEntities = UnitOfWork.Repository<TypiconEntity>().GetAll();
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
                string setting = folderPath + "\\" + response.TypiconEntity.Name + "\\Menology\\";

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

                setting = folderPath + "\\" + response.TypiconEntity.Name + "\\Triodion\\";

                fileReader.FolderPath = setting;

                foreach (TriodionRule rule in response.TypiconEntity.TriodionRules)
                {
                    rule.RuleDefinition = fileReader.Read(rule.DaysFromEaster.ToString());
                }

                setting = folderPath + "\\" + response.TypiconEntity.Name + "\\Sign\\";

                fileReader.FolderPath = setting;

                foreach (Sign sign in response.TypiconEntity.Signs)
                {
                    sign.RuleDefinition = fileReader.Read(sign.Name);
                }

                //commonRules

                ReloadCommonRules(response.TypiconEntity, folderPath);

                UnitOfWork.Commit();
            }
        }

        private void ReloadCommonRules(TypiconEntity typiconEntity, string folderPath)
        {
            folderPath = Path.Combine(folderPath, typiconEntity.Name, "Common");

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<FilesSearchResponse> files = fileReader.ReadsFromDirectory();

            foreach (FilesSearchResponse file in files)
            {
                CommonRule commonRule = typiconEntity.GetCommonRule(c => c.Name == file.Name);

                if (commonRule == null)
                {
                    commonRule = new CommonRule()
                    {
                        Name = file.Name,
                        RuleDefinition = file.Xml,
                        Owner = typiconEntity
                    };
                    typiconEntity.CommonRules.Add(commonRule);
                }
                else
                {
                    commonRule.RuleDefinition = file.Xml;
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
            throw new NotImplementedException();
        }

        public DeleteTypiconEntityResponse DeleteTypiconEntity(DeleteTypiconEntityRequest deleteTypiconEntityRequest)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
