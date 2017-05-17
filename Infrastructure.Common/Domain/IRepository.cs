using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypiconOnline.Infrastructure.Common.Domain
{
	public interface IRepository<AggregateType> 
		: IReadOnlyRepository<AggregateType> where AggregateType 
		: IAggregateRoot
	{
		void Update(AggregateType aggregate);
		void Insert(AggregateType aggregate);
		void Delete(AggregateType aggregate);
	}
}
