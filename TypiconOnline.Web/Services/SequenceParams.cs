using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;

namespace TypiconOnline.Web.Services
{
    public class SequenceParams
    {
        public string Language { get; set; } = "cs-ru";
        public bool? KekragariaShowPsalm { get; set; }

        public int? KekragariaYmnosCount { get; set; }

        public CustomParamsCollection<IRuleApplyParameter> ApplyParameters
        {
            get
            {
                var result = new CustomParamsCollection<IRuleApplyParameter>();

                if (KekragariaShowPsalm != null || KekragariaYmnosCount != null)
                {
                    var kekragaria = new KekragariaCustomParameter()
                    {
                        ShowPsalm = KekragariaShowPsalm,
                        TotalYmnosCount = KekragariaYmnosCount
                    };

                    result.Add(kekragaria);
                }

                return result;
            }
        }

        public CustomParamsCollection<IRuleCheckParameter> CheckParameters
        {
            get
            {
                var result = new CustomParamsCollection<IRuleCheckParameter>();

                //TODO: add implementation

                return result;
            }
        }
    }
}
