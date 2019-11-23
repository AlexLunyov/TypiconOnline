using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.AppServices.Migration.Typicon;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    public class TypiconXmlExportManager : ITypiconExportManager
    {
        private readonly IProjector<TypiconVersion, TypiconVersionProjection> _projector;
        private readonly TypiconDBContext _dbContext;

        private const string FILE_START = "Typicon"; 

        public TypiconXmlExportManager(IProjector<TypiconVersion, TypiconVersionProjection> projector
            , TypiconDBContext dbContext)
        {
            _projector = projector ?? throw new ArgumentNullException(nameof(projector));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Result<FileDownloadResponse> Export(int typiconVersionId)
        {
            var found = _dbContext.Set<TypiconVersion>().FirstOrDefault(c => c.Id == typiconVersionId);

            if (found != null)
            {
                var projection = _projector.Project(found);

                if (projection.Success)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TypiconVersionProjection));

                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, projection.Value);

                        var response = new FileDownloadResponse(stream.ToArray()
                            , "application/xml"
                            , GetFileName(projection.Value.Name.ToString(), found.VersionNumber));

                        return Result.Ok(response);
                    }
                }
            }

            return Result.Fail<FileDownloadResponse>($"Устав с Id={typiconVersionId} не был найден.");
        }

        private string GetFileName(string name, int versionNumber)
            => $"{FILE_START} {name} v.{versionNumber} {DateTime.Now.ToShortDateString()}.xml";
    }
}
