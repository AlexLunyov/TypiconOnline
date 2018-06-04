using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Класс описывает коолекцию песнопений.
    /// Таквыми являются Стихиры на Господи воззвах, на Хвалитех, и т.д.
    /// </summary>
    [Serializable]
    public class YmnosStructure : DayElementBase, IMergable<YmnosStructure>
    {
        public YmnosStructure() { }

        public YmnosStructure(YmnosGroup group)
        {
            Groups.Add(new YmnosGroup(group));
        }

        public YmnosStructure(YmnosStructure ymnosStructure)
        {
            if (ymnosStructure == null) throw new ArgumentNullException("YmnosStructure");

            ymnosStructure.Groups.ForEach(c => Groups.Add(new YmnosGroup(c)));

            if (ymnosStructure.Doxastichon != null)
            {
                Doxastichon = new YmnosGroup(ymnosStructure.Doxastichon);
            }

            ymnosStructure.Theotokion.ForEach(c => Theotokion.Add(new YmnosGroup(c)));
        }

        #region Properties
        [XmlElement(XmlConstants.YmnosStructureGroupNode)]
        public YmnosGroupCollection Groups { get; set; } = new YmnosGroupCollection();

        /// <summary>
        /// Славник
        /// </summary>
        [XmlElement(XmlConstants.YmnosStructureDoxastichonNode)]
        public YmnosGroup Doxastichon { get; set; }
        /// <summary>
        /// Богородичен
        /// </summary>
        [XmlElement(XmlConstants.YmnosStructureTheotokionNode)]
        public YmnosGroupCollection Theotokion { get; set; } = new YmnosGroupCollection();

        #endregion

        protected override void Validate()
        {
            foreach (YmnosGroup group in Groups)
            {
                if (!group.IsValid)
                {
                    AppendAllBrokenConstraints(group, /*ElementName + "." + */XmlConstants.YmnosStructureGroupNode);
                }
            }

            if (Doxastichon?.IsValid == false)
            {
                AppendAllBrokenConstraints(Doxastichon, /*ElementName + "." + */XmlConstants.YmnosStructureDoxastichonNode);
            }

            foreach (YmnosGroup group in Theotokion)
            {
                if (!group.IsValid)
                {
                    AppendAllBrokenConstraints(group, /*ElementName + "." + */XmlConstants.YmnosStructureTheotokionNode);
                }
            }
        }

        private int _sticheraCount = -1;
        /// <summary>
        /// Возвращает общее количество стихир, без учета славника и богородична
        /// </summary>
        /// <returns></returns>
        public int YmnosStructureCount
        {
            get
            { 
                //if (_sticheraCount == -1)
                //{
                    ThrowExceptionIfInvalid();

                    _sticheraCount = 0;

                    foreach (YmnosGroup group in Groups)
                    {
                        _sticheraCount += group.Ymnis.Count;
                    }
                //}

                return _sticheraCount;
            }
        }

        /// <summary>
        /// Возвращает глас Структуры, исходя из первого песнопения
        /// </summary>
        public int Ihos
        {
            get
            {
                ThrowExceptionIfInvalid();

                int result = 0;

                if (Groups.Count > 0)
                {
                    result = Groups[0].Ihos;
                }
                else if (Doxastichon != null)
                {
                    result = Doxastichon.Ihos;
                }
                else if (Theotokion.Count > 0)
                {
                    result = Theotokion[0].Ihos;
                }

                return result;
            }
        }

        public void Merge(YmnosStructure source)
        {
            if (source == null)
            {
                return;
            }

            Groups.AddRange(source.Groups);

            if (source.Doxastichon != null)
            {
                Doxastichon = source.Doxastichon;
            }

            if (source.Theotokion?.Count > 0)
            {
                Theotokion = source.Theotokion;
            }
        }
    }
}
