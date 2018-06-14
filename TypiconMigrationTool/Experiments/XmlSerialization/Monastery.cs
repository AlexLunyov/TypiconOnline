using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconMigrationTool.Experiments.XmlSerialization
{
    [Serializable]
    public class Monastery
    {
        public Monastery()
        {
            Churches = new List<Building>();
            LivingHouses = new List<Building>();
            WorkHouses = new List<Building>();
        }

        [XmlElement("Church")]
        public List<Building> Churches { get; set; }
        [XmlElement("LivingHouse")]
        public List<Building> LivingHouses { get; set; }
        [XmlElement("WorkHouse")]
        public List<Building> WorkHouses { get; set; }
    }

    public interface IBuilding { }

    [Serializable]
    public class Building : IBuilding//, IXmlSerializable
    {
        public Building() { }

        public string Name { get; set; }
        //[XmlAttribute]
        public BoolExpression IsBroken { get; set; }
        [XmlAttribute]
        public bool InUsage { get; set; }

        public ItemInt FloorsCount { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
