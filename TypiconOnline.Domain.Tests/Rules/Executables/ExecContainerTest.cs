using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class ExecContainerTest
    {
        [Test]
        public void Rules_Executables_ExecContainer_Add()
        {
            string xmlString = @"<rule>
	                                <switch>
		                                <condition>
			                                <int>-08</int>
		                                </condition>
		                                <case>
			                                <values>
				                                 <date>--11-04</date>
			                                </values>
			                                <action>
				                                <daymodification servicesign=""12"" iscustomname=""true"">
					                                <getclosestday dayofweek=""суббота"" weekcount=""-2""><date>--11-08</date></getclosestday>
				                                </daymodification>
			                                </action>
		                                </case>
		                                <default>
			                                <daymodification servicesign=""12"" iscustomname=""true"">
					                                <getclosestday dayofweek=""суббота"" weekcount=""-1""><date>--11-08</date></getclosestday>
				                                </daymodification>
		                                </default>
	                                </switch>
                                </rule>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ExecContainer container = new ExecContainer(xmlDoc.FirstChild);

            xmlString = @"<case>
			                <values>
				                    <date>--11-04</date>
			                </values>
			                <action>
				                <daymodification servicesign=""12"" iscustomname=""true"">
					                <getclosestday dayofweek=""суббота"" weekcount=""-2""><date>--11-08</date></getclosestday>
				                </daymodification>
			                </action>
		                </case>";
            xmlDoc.LoadXml(xmlString);

            DayModification dayModification = new DayModification(xmlDoc.FirstChild);

            container.ChildElements.Add(dayModification);

            Assert.AreEqual(container.ChildElements.Count, 2);
        }

        
    }
}
