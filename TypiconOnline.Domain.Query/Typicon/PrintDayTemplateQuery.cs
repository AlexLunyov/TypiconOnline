using System;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class PrintDayTemplateQuery : IQuery<PrintDayTemplate>
    {
        public PrintDayTemplateQuery(int typiconId, int number)
        {
            TypiconId = typiconId;
            Number = number;
        }

        public int TypiconId { get; }
        public int Number { get; }
    }
}
