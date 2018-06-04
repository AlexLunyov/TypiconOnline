using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Элемент структуризации правил для для объектов класса Day
    /// </summary>
    public class RuleFolderEntity<DayType> : FolderEntity where DayType : Day
    {
        public RuleFolderEntity()
        {
            Name = "";
            Folders = new List<RuleFolderEntity<DayType>>();
            Rules = new List<RuleDayEntity<DayType>>();
        }

        //public IOwner Owner { get; set; }

        public new RuleFolderEntity<DayType> Parent { get; set; }

        public new List<RuleFolderEntity<DayType>> Folders { get; set; }

        public new List<RuleDayEntity<DayType>> Rules { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        public RuleDayEntity<DayType> FindRule(Predicate<RuleDayEntity<DayType>> predicate)
        {
            if (predicate == null)
                return null;

            RuleDayEntity<DayType> result = Rules.Find(predicate);

            if (result == null)
            {
                foreach (RuleFolderEntity<DayType> folder in Folders)
                {
                    result = folder.Rules.Find(predicate);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        public List<RuleDayEntity<DayType>> FindAllRules(Predicate<RuleDayEntity<DayType>> predicate = null)
        {
            List<RuleDayEntity<DayType>> resultRules = new List<RuleDayEntity<DayType>>();

            List<RuleDayEntity<DayType>> rules = (predicate == null) ? Rules : Rules.FindAll(predicate);

            if (rules != null)
            {
                resultRules.AddRange(rules);
            }
                
            foreach (RuleFolderEntity<DayType> folder in Folders)
            {
                rules = (predicate == null) ? folder.Rules : folder.Rules.FindAll(predicate);

                if (rules != null)
                {
                    resultRules.AddRange(rules);
                }
            }

            return resultRules;
        }
    }
}
