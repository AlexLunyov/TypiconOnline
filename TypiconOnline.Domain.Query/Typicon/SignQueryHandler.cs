using JetBrains.Annotations;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class SignQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<SignQuery, Sign>
    {
        public SignQueryHandler(TypiconDBContext dbContext, IDataQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor) { }

        //private readonly IncludeOptions Includes = new IncludeOptions()
        //{
        //    Includes = new string[]
        //    {
        //        "SignName.Items"
        //    }
        //};

        public Sign Handle([NotNull] SignQuery query)
        {
            return GetSignRecursivly(query.SignId);
        }

        private SignDto GetSignRecursivly(int signId)
        {
            SignDto result = null;

            var sign = DbContext.Repository<Sign>().Get(c => c.Id == signId, Includes);

            if (sign != null)
            {
                result = new SignDto()
                {
                    Id = sign.Id,
                    SignName = new ItemText(sign.SignName),
                    IsTemplate = sign.IsTemplate,
                    Priority = sign.Priority,
                    RuleDefinition = sign.RuleDefinition,
                    IsAddition = sign.IsAddition
                };

                if (sign.TemplateId.HasValue)
                {
                    result.Template = GetSignRecursivly(sign.TemplateId.Value);
                }                                    

                //number
                if (sign.Number.HasValue)
                {
                    result.Number = sign.Number.Value;
                }
                else if (result.Template != null)
                {
                    result.Number = result.Template.Number;
                }
            }

            return result;
        }
    }
}
