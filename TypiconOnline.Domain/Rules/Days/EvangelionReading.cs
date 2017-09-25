using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Евангельское чтение
    /// </summary>
    public class EvangelionReading : ValueObjectBase
    {
        public EvangelionReading() { }

        public EvangelionReading(XmlNode node) { }

        #region Properties


        #endregion

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
