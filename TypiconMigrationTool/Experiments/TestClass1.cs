using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconMigrationTool.Experiments
{
    [TestFixture]
    public class TestClass1
    {
        [Test]
        public void TestMethod()
        {
            using (Context context = new Context())
            {
                User user = new User()
                {
                    Name = "User 1",
                    Root = new Folder()
                    {
                        Name = "Root folder",
                        Children = new List<Folder>()
                        {
                            new Folder() { Name = "Child folder 1"},
                            new Folder() { Name = "Child folder 2"}
                        }
                    }
                };
                context.UserSet.Add(user);

                context.SaveChanges();

                context.UserSet.Remove(user);

                context.SaveChanges();
            }

            Assert.Pass("Success.");
        }
    }
}
