using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TypiconOnline.Infrastructure.Common.Domain
{
	public interface IReadOnlyRepository<AggregateType>// where AggregateType : IAggregateRoot
	{
		AggregateType Get(Expression<Func<AggregateType, bool>> predicate, IncludeOptions options = null);
        //AggregateType Get(int id);
        IQueryable<AggregateType> GetAll(Expression<Func<AggregateType, bool>> predicate = null, IncludeOptions options = null);
    }
}
