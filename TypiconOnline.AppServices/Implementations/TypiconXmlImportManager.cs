using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Migration.Typicon;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    public class TypiconXmlImportManager : ITypiconImportManager
    {
        private readonly IProjector<TypiconVersionProjection, TypiconEntity> _projector;
        private readonly TypiconDBContext _dbContext;
        private readonly ITypiconSerializer _serializer;

        public TypiconXmlImportManager(IProjector<TypiconVersionProjection, TypiconEntity> projector
            , TypiconDBContext dbContext
            , ITypiconSerializer serializer)
        {
            _projector = projector ?? throw new ArgumentNullException(nameof(projector));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public Result Import(byte[] file)
        {
            try
            {
                var xml = Encoding.UTF8.GetString(file);

                var projection = _serializer.Deserialize<TypiconVersionProjection>(xml);

                var entity = _projector.Project(projection);

                _dbContext.Set<TypiconEntity>().Add(entity.Value);

                int i = _dbContext.SaveChanges();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
