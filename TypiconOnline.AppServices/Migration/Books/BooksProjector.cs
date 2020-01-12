using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Repository.EFCore.DataBase;
using Microsoft.EntityFrameworkCore;

namespace TypiconOnline.AppServices.Migration.Books
{
    public class BooksProjector
    {
        private readonly TypiconDBContext _dbContext;

        public BooksProjector(TypiconDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public BooksContainer Project()
        {
            return new BooksContainer()
            {
                Menologies = GetMenologies(),
                Triodions = GetTriodions()
            };
        }

        private List<TriodionWorshipProjection> GetTriodions()
        {
            return _dbContext.Set<TriodionDay>()
                .SelectMany(c => c.DayWorships)
                .Select(d => new TriodionWorshipProjection()
                {
                    Id = d.Id,
                    DaysFromEaster = (d.Parent as TriodionDay).DaysFromEaster,
                    IsCelebrating = d.IsCelebrating,
                    UseFullName = d.UseFullName,
                    WorshipName = (d.WorshipName != null) ? new ItemTextStyled(d.WorshipName) : default,
                    WorshipShortName = (d.WorshipShortName != null) ? new ItemText(d.WorshipShortName) : default
                })
                .AsNoTracking()
                .ToList();
        }

        private List<MenologyWorshipProjection> GetMenologies()
        {
            return _dbContext.Set<MenologyDay>()
                .SelectMany(c => c.DayWorships)
                .Select(d => new MenologyWorshipProjection()
                {
                    Id = d.Id,
                    Date = (d.Parent as MenologyDay).Date.ToString(),
                    LeapDate = (d.Parent as MenologyDay).LeapDate.ToString(),
                    IsCelebrating = d.IsCelebrating,
                    UseFullName = d.UseFullName,
                    WorshipName = (d.WorshipName != null) ? new ItemTextStyled(d.WorshipName) : default,
                    WorshipShortName = (d.WorshipShortName != null) ? new ItemText(d.WorshipShortName) : default
                })
                .AsNoTracking()
                .ToList();
        }
    }
}
