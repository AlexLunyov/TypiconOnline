using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore
{ 
    public interface IRepositoryFactory
    {
        IRepository<AggregateType> Create<AggregateType>(TypiconDBContext dbContext) where AggregateType : class, IAggregateRoot;
    }
}
