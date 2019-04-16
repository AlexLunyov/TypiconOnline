using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    public class ValidateTypiconVersionJob: IJob
    {
        public ValidateTypiconVersionJob(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public bool Equals(IJob other) => other is ValidateTypiconVersionJob j && Id == j.Id;
    }
}
