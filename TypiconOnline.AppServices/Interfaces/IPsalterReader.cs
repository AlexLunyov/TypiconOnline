using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IPsalterReader
    {
        bool Read();
        PsalterElementKind ElementType { get; }
        IPsalterElement Element { get; }
    }

    public enum PsalterElementKind { Kathisma, Psalm, PsalmAnnotation, PsalmText, Slava }
}
