using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconMigrationTool.Experiments.XmlSerialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconMigrationTool.Experiments
{
    public class SerializationExperiment
    {
        Monastery monastery;

        public Monastery GetObject()
        {
            if (monastery == null)
            {
                monastery = new Monastery();

                monastery.Churches.Add(new Building() { Name = "Храм Христа Спасителя", FloorsCount = new ItemInt(2), InUsage = true });
                monastery.Churches.Add(new Building() { Name = "Троицкий собор", IsBroken = new BoolExpression(false), FloorsCount = new ItemInt(3) });
                monastery.Churches.Add(new Building() { Name = "Предтеченский скит", IsBroken = new BoolExpression(true), FloorsCount = new ItemInt(1) });
                monastery.LivingHouses.Add(new Building() { Name = "Архиерейский корпус", IsBroken = new BoolExpression(false), FloorsCount = new ItemInt(3) });
                monastery.LivingHouses.Add(new Building() { Name = "Трапезный корпус", IsBroken = new BoolExpression(false), FloorsCount = new ItemInt(3) });
                monastery.LivingHouses.Add(new Building() { Name = "Гостиница", IsBroken = new BoolExpression(true), FloorsCount = new ItemInt(2) });
                monastery.WorkHouses.Add(new Building() { Name = "Сапожня", IsBroken = new BoolExpression(true), FloorsCount = new ItemInt(2) });
                monastery.WorkHouses.Add(new Building() { Name = "Коровник", FloorsCount = new ItemInt(1), InUsage = true });
            }

            return monastery;
        }

        public T Deserialize<T>(string xml) where T : class
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlReaderSettings settings = new XmlReaderSettings();
            // No settings need modifying here

            using (StringReader textReader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        public string Serialize<T>(T value) where T: class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, value);
                }
                return textWriter.ToString();
            }
        }
    }
}
