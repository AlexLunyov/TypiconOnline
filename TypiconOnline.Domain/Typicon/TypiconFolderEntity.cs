using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Элемент структуризации правил
    /// </summary>
    public class TypiconFolderEntity : FolderEntity
    {
        public TypiconFolderEntity()
        {
            Name = "";
            Folders = new List<TypiconFolderEntity>();
            Rules = new List<RuleEntity>();
        }

        public TypiconEntity Owner { get; set; }

        public new virtual TypiconFolderEntity Parent { get; set; }

        public new virtual List<TypiconFolderEntity> Folders { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        public TypiconEntity GetOwner()
        {
            TypiconEntity result = Owner;
            if ((result == null) && (Parent != null))
            {
                return Parent.GetOwner();
            }

            return result;
        }

        public MenologyRule FindMenologyRule(DateTime date)
        {
            //MenologyRule result = (MenologyRule)Rules.AsQueryable().FirstOrDefault(c => ((c is MenologyRule) &&
            //                                    (((MenologyRule)c).Day.GetCurrentDate(date.Year)) == date));

            MenologyRule result = (MenologyRule)Rules.FirstOrDefault(c => ((c is MenologyRule) &&
                                                (((MenologyRule)c).Day.GetCurrentDate(date.Year)) == date));

            if (result == null)
            {
                foreach (TypiconFolderEntity folder in Folders)
                {
                    result = folder.FindMenologyRule(date);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        public TriodionRule FindTriodionRule(int daysFromEaster)
        {
            TriodionRule result = (TriodionRule)Rules.AsQueryable().
                FirstOrDefault(c => ((c is TriodionRule) && ((c as TriodionRule).Day.DaysFromEaster == daysFromEaster)));

            if (result == null)
            {
                foreach (TypiconFolderEntity folder in Folders)
                {
                    result = folder.FindTriodionRule(daysFromEaster);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return result;
        }
    }
}
