using JetBrains.Annotations;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class SignQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<SignQuery, SignDTO>
    {
        public SignQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor)
            : base(unitOfWork, queryProcessor) { }

        private readonly IncludeOptions Includes = new IncludeOptions()
        {
            Includes = new string[]
            {
                "SignName"
            }
        };

        public SignDTO Handle([NotNull] SignQuery query)
        {
            return GetSignRecursivly(query.SignId);
        }

        private SignDTO GetSignRecursivly(int signId)
        {
            SignDTO result = null;

            var sign = UnitOfWork.Repository<Sign>().Get(c => c.Id == signId, Includes);

            if (sign != null)
            {
                result = new SignDTO()
                {
                    Id = sign.Id,
                    SignName = new ItemText(sign.SignName),
                    IsTemplate = sign.IsTemplate,
                    Priority = sign.Priority,
                    RuleDefinition = sign.RuleDefinition,
                    IsAddition = sign.IsAddition
                };

                if (sign.TemplateId != null)
                {
                    result.Template = GetSignRecursivly((int)sign.TemplateId);
                }                                    

                //number
                if (sign.Number != null)
                {
                    result.Number = (int)sign.Number;
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
