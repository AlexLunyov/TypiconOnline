using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Элемент структуризации правил
    /// </summary>
    public class FolderEntity : EntityBase<int>, IAggregateRoot
    {
        public FolderEntity()
        {
            Name = "";
            Folders = new List<FolderEntity>();
            Rules = new List<RuleEntity>();
        }

        //public virtual int? OwnerId { get; set; }

        //public virtual TypiconEntity Owner { get; set; }

        public string Name { get; set; }

        //public int? ParentId { get; set; }
          
        public virtual FolderEntity Parent { get; set; }

        public virtual List<FolderEntity> Folders { get; set; }

        public virtual List<RuleEntity> Rules { get; set; }

        public string PathName
        {
            get
            {
                return (Parent != null) ? (Parent.PathName + "/" + Name) : Name;
            }
        }

        public RuleEntity FindRule(Expression<Func<RuleEntity, bool>> predicate)
        {
            if (predicate == null)
                return null;

            RuleEntity result = Rules.AsQueryable().Where(predicate).FirstOrDefault();

            if (result == null)
            {
                foreach (FolderEntity folder in Folders)
                {
                    result = folder.FindRule(predicate);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        

        //public List<RuleEntity> FindAllRules(Expression<Func<RuleEntity, bool>> predicate = null)
        public List<RuleEntity> FindAllRules(Predicate<RuleEntity> predicate = null)
        {
            List<RuleEntity> resultRules = new List<RuleEntity>();

            List<RuleEntity> rules = (predicate == null) ? Rules : Rules.FindAll(predicate); //AsQueryable().Where(predicate) as List<RuleEntity>;

            if (rules != null)
            {
                resultRules.AddRange(rules);
            }

            foreach (FolderEntity folder in Folders)
            {
                rules = (predicate == null) ? folder.Rules : folder.FindAllRules(predicate);//Rules.AsQueryable().Where(predicate) as List<RuleEntity>;

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

        public void AddFolder(FolderEntity folder)
        {
            folder.Parent = this;
            //folder.Owner = Owner;
            Folders.Add(folder);
        }

        public void AddRule(RuleEntity rule)
        {
            rule.Folder = this;
            Rules.Add(rule);
        }

        
    }
}
