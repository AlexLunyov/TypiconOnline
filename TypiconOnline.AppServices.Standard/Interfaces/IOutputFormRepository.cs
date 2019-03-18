using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOutputFormRepository
    {
        Result<OutputForm> Get(int typiconId, DateTime Date);

        void Save(int typiconId, DateTime date, OutputForm outputForm);
    }
}
