using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Common
{
    public class DayXmlHelper<T>  where T : Day
    {
        IUnitOfWork _unitOfWork;
        IXmlSaver _xmlSaver;

        public DayXmlHelper(IUnitOfWork unitOfWork, IXmlSaver xmlSaver)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");
            if (xmlSaver == null)
                throw new ArgumentNullException("xmlSaver");

            _unitOfWork = unitOfWork;
            _xmlSaver = xmlSaver;
        }

        /// <summary>
        /// Сохраняет все имена в файлы
        /// </summary>
        public void Save()
        {
            List<T> days = _unitOfWork.Repository<T>().GetAll().ToList();

            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement(typeof(T).Name + "s");
            doc.AppendChild(root);

            foreach (T day in days)
            {
                XmlNode node = doc.CreateElement(typeof(T).Name);
                XmlAttribute idAttr = doc.CreateAttribute("id");
                idAttr.Value = day.Id.ToString();
                node.Attributes.Append(idAttr);
                node.InnerXml = day.Name1.StringExpression + day.Name2.StringExpression;
                root.AppendChild(node);
            }

            _xmlSaver.Save(doc);
        }

        public void Load()
        {
            XmlDocument doc = _xmlSaver.Load();

            XmlElement root = doc.DocumentElement;

            if ((root != null) && (root.HasChildNodes))
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    XmlAttribute attrId = node.Attributes["id"];
                    if (attrId != null)
                    {
                        int id = int.Parse(attrId.Value);
                        T day = _unitOfWork.Repository<T>().Get(c => c.Id == id);

                        if (day != null)
                        {
                            XmlNode name1Node = node.SelectSingleNode("Name1");
                            XmlNode name2Node = node.SelectSingleNode("Name2");

                            day.Name1.StringExpression = name1Node.OuterXml;
                            day.Name2.StringExpression = name2Node.OuterXml;
                        }
                    }
                }

                _unitOfWork.Commit();
            }
        }
    }
}
