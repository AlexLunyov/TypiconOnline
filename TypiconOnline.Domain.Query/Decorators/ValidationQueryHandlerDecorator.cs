using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Decorators
{
    public class ValidationQueryHandlerDecorator<TQuery, TResult> : QueryDecoratorBase<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public ValidationQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated) : base(decorated)
        {
        }

        public override TResult Handle(TQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
