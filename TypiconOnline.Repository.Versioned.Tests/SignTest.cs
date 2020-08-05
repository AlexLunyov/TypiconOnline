using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Versioned.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Repository.Versioned.Tests
{
    [TestClass]
    public class SignTest
    {
        [TestMethod]
        public void Sign_Create()
        {
            var dbContext = VersionedDbContextFactory.Create();

            var typicon = new Typicon();

            TypiconVersion prevTypiconVersion = null;

            //5 версий
            for (int version = 1; version < 6; version++)
            {
                

                var now = DateTime.Now;
                var newTypiconVersion = new TypiconVersion();

                if (prevTypiconVersion == null)
                {
                    //первый раз
                    //10 знаков служб
                    for (int item = 0; item < 10; item++)
                    {
                        var sign = new Sign();

                        var signVersion = new SignVersion()
                        {
                            RuleDefinition = $"RuleDefinition {item}",
                            ModRuleDefinition = $"ModRuleDefinition {item}",
                            Name = new ItemText(new ItemTextUnit("cs-ru", $"Sign Name v{version}.0")),
                            VersionOwner = sign
                        };

                        sign.Versions.Add(signVersion);

                        typicon.Signs.Add(sign);

                        newTypiconVersion.Signs.Add(signVersion);
                    }
                }
                else
                {
                    //Обновляем все знаки служб
                    prevTypiconVersion.Signs.ForEach(c =>
                    {
                        var signVersion = new SignVersion()
                        {
                            RuleDefinition = c.RuleDefinition,
                            ModRuleDefinition = c.ModRuleDefinition,
                            Name = new ItemText(new ItemTextUnit("cs-ru", $"Sign Name {c.RuleDefinition} v{version}.0")),
                            VersionOwner = c.VersionOwner,
                            PreviousVersion = c
                        };

                        c.PublishDate = now;
                        if (c.PreviousVersion != null)
                        {
                            c.PreviousVersion.ArchiveDate = now;
                        }

                        c.VersionOwner.Versions.Add(signVersion);

                        newTypiconVersion.Signs.Add(signVersion);
                    });


                    prevTypiconVersion.PublishDate = now;
                    newTypiconVersion.PreviousVersion = prevTypiconVersion;

                    if (prevTypiconVersion.PreviousVersion != null)
                    {
                        prevTypiconVersion.PreviousVersion.ArchiveDate = now;
                    }

                    
                }

                typicon.Versions.Add(newTypiconVersion);

                prevTypiconVersion = newTypiconVersion;
            }

            dbContext.Set<Typicon>().Add(typicon);

            var k = dbContext.SaveChanges();

            Assert.IsTrue(k > 0);
        }

        [TestMethod]
        public void Sign_DraftView()
        {
            var dbContext = DomainDbContextFactory.Create();

            var entities = dbContext.Set<Domain.Typicon.Sign>().ToList();

            Assert.IsNotNull(entities);
        }
    }
}
