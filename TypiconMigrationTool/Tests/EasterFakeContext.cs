using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Easter;

namespace TypiconMigrationTool.Tests
{
    public class EasterFakeContext : IEasterContext
    {
        public DateTime Date { get; }

        List<EasterItem> Easters { get; }

        public EasterFakeContext(DateTime date)
        {
            Date = date;

            Easters = InitializeEasterRepository();
        }

        public IEnumerable<EasterItem> GetAll()
        {
            return Easters;
        }

        public DateTime GetCurrentEaster(int year)
        {
            return (from easter in Easters where easter.Date.Year == year select easter.Date).FirstOrDefault();
        }

        private List<EasterItem> InitializeEasterRepository()
        {
            return new List<EasterItem>()
            {
                new EasterItem() { Date = Date.AddDays(-29) },
                new EasterItem() { Date = Date.AddDays(-22).AddYears(1) },
                new EasterItem() { Date = Date.AddDays(-28).AddYears(2) },
                new EasterItem() { Date = Date.AddDays(-27).AddYears(3) },
                new EasterItem() { Date = Date.AddDays(-20).AddYears(4) },
                new EasterItem() { Date = Date.AddDays(-13).AddYears(5) },
                new EasterItem() { Date = Date.AddDays(-25).AddYears(6) },
                new EasterItem() { Date = Date.AddDays(-21).AddYears(7) },
                new EasterItem() { Date = Date.AddDays(-14).AddYears(8) },
                new EasterItem() { Date = Date.AddDays(-18).AddYears(9) },
                new EasterItem() { Date = Date.AddDays(-17).AddYears(10) },
                new EasterItem() { Date = Date.AddDays(-16).AddYears(11) },
                new EasterItem() { Date = Date.AddDays(-15).AddYears(12) },
                new EasterItem() { Date = Date.AddDays(-14).AddYears(13) },
                new EasterItem() { Date = Date.AddDays(-13).AddYears(14) },
                new EasterItem() { Date = Date.AddDays(-12).AddYears(15) },
                new EasterItem() { Date = Date.AddDays(-11).AddYears(16) },
                new EasterItem() { Date = Date.AddDays(-10).AddYears(17) },
                new EasterItem() { Date = Date.AddDays(-9).AddYears(18) },
                new EasterItem() { Date = Date.AddDays(-8).AddYears(19) },
                new EasterItem() { Date = Date.AddDays(-7).AddYears(20) },
                new EasterItem() { Date = Date.AddDays(-6).AddYears(21) },
                new EasterItem() { Date = Date.AddDays(-5).AddYears(22) },
                new EasterItem() { Date = Date.AddDays(-4).AddYears(23) },
                new EasterItem() { Date = Date.AddDays(-3).AddYears(24) },
                new EasterItem() { Date = Date.AddDays(-2).AddYears(25) },
                new EasterItem() { Date = Date.AddDays(-1).AddYears(26) },
                new EasterItem() { Date = Date.AddDays(0).AddYears(27) },
                new EasterItem() { Date = Date.AddDays(1).AddYears(28) },
                new EasterItem() { Date = Date.AddDays(1).AddYears(29) },
                new EasterItem() { Date = Date.AddDays(2).AddYears(30) },
                new EasterItem() { Date = Date.AddDays(3).AddYears(31) },
            };

            //TODO: 10/10/2017 закомментировал, потому как поменялся функционал 
            //EasterStorage.Instance.EasterDays = easters;
        }

        public int GetDaysFromCurrentEaster(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
