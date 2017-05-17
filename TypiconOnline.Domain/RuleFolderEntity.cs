using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Элемент структуризации правил
    /// </summary>
    public class RuleFolderEntity : EntityBase<int>, IAggregateRoot
    {
        public RuleFolderEntity()
        {
            Name = "";
            Folders = new List<RuleFolderEntity>();
            Rules = new List<RuleEntity>();
        }

        //public IOwner Owner { get; set; }
        public string Name { get; set; }

        public RuleFolderEntity Parent { get; set; }

        public List<RuleFolderEntity> Folders { get; set; }

        public List<RuleEntity> Rules { get; set; }

        public string PathName
        {
            get
            {
                return (Parent != null) ? (Parent.PathName + "/" + Name) : Name;
            }
        }

        public RuleEntity FindRule(Predicate<RuleEntity> predicate)
        {
            if (predicate == null)
                return null;

            RuleEntity result = Rules.Find(predicate);

            if (result == null)
            {
                foreach (RuleFolderEntity folder in Folders)
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

        public List<RuleEntity> FindAllRules(Predicate<RuleEntity> predicate = null)
        {
            List<RuleEntity> resultRules = new List<RuleEntity>();

            List<RuleEntity> rules = (predicate == null) ? Rules : Rules.FindAll(predicate);

            if (rules != null)
            {
                resultRules.AddRange(rules);
            }

            foreach (RuleFolderEntity folder in Folders)
            {
                rules = (predicate == null) ? folder.Rules : folder.Rules.FindAll(predicate);

                if (rules != null)
                {
                    resultRules.AddRange(rules);
                }
            }

            return resultRules;
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
