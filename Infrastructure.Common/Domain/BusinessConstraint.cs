using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypiconOnline.Infrastructure.Common.Domain
{
	public class BusinessConstraint
	{
		private string _constraintDescription;
        private string _constraintPath;

        public BusinessConstraint(string principleDescription)
		{
            _constraintDescription = principleDescription;
		}

        public BusinessConstraint(string principleDescription, string principlePath)
        {
            _constraintDescription = principleDescription;
            _constraintPath = principlePath;
        }

        public string ConstraintDescription
        {
			get
			{
				return _constraintDescription;
			}
		}

        public string ConstraintPath
        {
            get
            {
                return _constraintPath;
            }
            set
            {
                _constraintPath = value;
            }
        }

        public string ConstraintFullDescription
        {
            get
            {
                return _constraintPath + ": " + _constraintDescription;
            }
        }
    }
}
