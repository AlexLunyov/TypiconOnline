﻿using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IBooksExportManager
    {
        Result<FileDownloadResponse> Export();
    }
}
