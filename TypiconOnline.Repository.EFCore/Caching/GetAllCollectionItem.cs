using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Repository.EFCore.Caching
{
    public class GetAllCollectionItem
    {
        public List<string> Collection { get; set; } = new List<string>();
        public DateTime ExpirationDate { get; set; }

        //public IQueryable<DomainType> GetEntities<DomainType>(ICacheStorage cacheStorage)
        //{

        //}
    }
}
