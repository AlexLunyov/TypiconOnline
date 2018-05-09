using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public class BusinessConstraint
    {
        public BusinessConstraint(string principleDescription)
        {
            ConstraintDescription = principleDescription;
        }

        public BusinessConstraint(string principleDescription, string principlePath)
        {
            ConstraintDescription = principleDescription;
            ConstraintPath = principlePath;
        }

        public string ConstraintDescription { get; }

        public string ConstraintPath { get; set; }

        public string ConstraintFullDescription => $"{ConstraintPath}: {ConstraintDescription}";
    }
}
