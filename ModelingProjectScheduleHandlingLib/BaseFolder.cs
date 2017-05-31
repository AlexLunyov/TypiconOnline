using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class BaseFolder<T> where T: RuleEntity
{
    public List<T> ChildElements { get; set; }
}
