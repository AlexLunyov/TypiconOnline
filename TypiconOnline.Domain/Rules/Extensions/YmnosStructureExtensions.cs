using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class YmnosStructureExtensions
    {
        /// <summary>
        /// Производит слияние двух Структур песнопений. Славник и Богородичен переписывается
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="source"></param>
        public static void Merge(this YmnosStructure structure, YmnosStructure source)
        {
            if (structure == null || source == null)
            {
                return;
            }

            structure.Groups.AddRange(source.Groups);

            if (source.Doxastichon != null)
            {
                structure.Doxastichon = source.Doxastichon;
            }

            if (source.Theotokion?.Count > 0)
            {
                structure.Theotokion = source.Theotokion;
            }
        }
    }
}
