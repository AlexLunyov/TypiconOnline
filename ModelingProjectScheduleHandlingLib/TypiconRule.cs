using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class TypiconRule<T> : CommonRule where T : Day
{
    public virtual T Day { get; set; }
    /// <summary>
    /// Вычисляется из имени Day
    /// </summary>
    public override string Name { get; set; }
    public Sign Sign { get; set; }
}
