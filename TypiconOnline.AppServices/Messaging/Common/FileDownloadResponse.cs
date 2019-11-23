using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Messaging.Common
{
    public class FileDownloadResponse
    {
        public FileDownloadResponse(byte[] content, string contentType, string fileDownloadName)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentException("message", nameof(contentType));
            }

            if (string.IsNullOrEmpty(fileDownloadName))
            {
                throw new ArgumentException("message", nameof(fileDownloadName));
            }

            Content = content ?? throw new ArgumentNullException(nameof(content));
            ContentType = contentType;
            FileDownloadName = fileDownloadName;
        }

        public byte[] Content { get; }

        public string ContentType { get; }

        public string FileDownloadName { get; }
    }
}
