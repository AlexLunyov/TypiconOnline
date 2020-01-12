using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Domain.Days;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using Microsoft.EntityFrameworkCore;

namespace TypiconOnline.AppServices.Migration.Books
{
    /// <summary>
    /// Экспортирует все богослужебные тексты книг Минеи и Триоди 
    /// </summary>
    public class BooksXmlExportManager : IBooksExportManager
    {
        private readonly BooksProjector _booksProjector;

        public BooksXmlExportManager(
            //IProjector<TypiconVersion, TypiconVersionProjection> projector,
            TypiconDBContext dbContext)
        {
            _booksProjector = new BooksProjector(dbContext ?? throw new ArgumentNullException(nameof(dbContext)));
        }

        public Result<FileDownloadResponse> Export()
        {
            try
            {
                var container = _booksProjector.Project();

                XmlSerializer serializer = new XmlSerializer(typeof(BooksContainer));

                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, container);

                    var response = new FileDownloadResponse(stream.ToArray()
                        , "application/xml"
                        , GetFileName());

                    return Result.Ok(response);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<FileDownloadResponse>($"При экспорте произошла ошибка: {ex.Message}.");
            }
        }

        private string GetFileName() => $"Typicon books - {DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.xml";

        
    }
}
