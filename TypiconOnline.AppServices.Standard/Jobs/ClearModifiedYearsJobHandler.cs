using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Jobs
{
    public class ClearModifiedYearsJobHandler : ICommandHandler<ClearModifiedYearsJob>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClearModifiedYearsJobHandler([NotNull] IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        private readonly IncludeOptions _includes = new IncludeOptions()
        {
            Includes = new string[]
            {
                "ModifiedRules"
            }
        };

        public void Execute(ClearModifiedYearsJob command)
        {
            var years = _unitOfWork.Repository<ModifiedYear>().GetAll(c => c.TypiconVersionId == command.TypiconId, _includes);

            if (years != null)
            {
                foreach (var year in years)
                {
                    _unitOfWork.Repository<ModifiedYear>().Remove(year);
                }

                _unitOfWork.SaveChanges();
            }
        }
    }
}
