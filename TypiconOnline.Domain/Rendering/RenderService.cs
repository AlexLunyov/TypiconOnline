using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rendering
{
    public class RenderService : RenderElement
    {
        public string Time;
        public bool IsDayBefore;
        public string AdditionalName;

        public RenderService(Service item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Service");
            }

            item.ThrowExceptionIfInvalid();

            Time = item.Time.Expression;
            IsDayBefore = item.IsDayBefore.Value;
            AdditionalName = item.AdditionalName;
        }
    }
}
