using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KanonasItem : RuleExecutable, ICustomInterpreted
    {
        public KanonasItem(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.KanonasSourceAttrName];
            Source = (attr != null) ? new ItemEnumType<YmnosSource>(attr.Value) : null;

            attr = node.Attributes[RuleConstants.KanonasPlaceAttrName];
            Place = (attr != null) ? new ItemEnumType<KanonasPlaceKand>(attr.Value) : null;

            attr = node.Attributes[RuleConstants.KanonasCountAttrName];
            Count = new ItemInt((attr != null) ? attr.Value : "");

            attr = node.Attributes[RuleConstants.KanonasMartyrionAttrName];
            Martyrion = new ItemBoolean((attr != null) ? attr.Value : "true");

            attr = node.Attributes[RuleConstants.KanonasIrmosCountAttrName];
            IrmosCount = new ItemInt((attr != null) ? attr.Value : "0");
        }

        #region Properties

        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public ItemEnumType<YmnosSource> Source { get; }

        public ItemEnumType<KanonasPlaceKand> Place { get; }

        /// <summary>
        /// Количество тропарей, которые берутся из выбранного источника
        /// </summary>
        public ItemInt Count { get; set; }

        /// <summary>
        /// Признак, использовать ли мученичны в каноне. По умолчанию - true
        /// </summary>
        public ItemBoolean Martyrion { get; set; }

        /// <summary>
        /// Количество ирмосов, которые берутся из выбранного источника. По умолчанию - 0
        /// </summary>
        public ItemInt IrmosCount { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasItem>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (Source == null)
            {
                AddBrokenConstraint(KanonasItemBusinessConstraint.SourceRequired, ElementName);
            }
            else if (!Source.IsValid)
            {
                AppendAllBrokenConstraints(Source);
            }

            if (Place == null)
            {
                AddBrokenConstraint(KanonasItemBusinessConstraint.PlaceRequired, ElementName);
            }
            else if (Place.IsValid == false)
            {
                AppendAllBrokenConstraints(Place);
            }

            if (!Count.IsValid)
            {
                AppendAllBrokenConstraints(Count);
            }
            else
            {
                
                if (Count.Value < 1)
                {
                    AddBrokenConstraint(KanonasItemBusinessConstraint.CountInvalid, ElementName);
                }
            }

            if (!IrmosCount.IsValid)
            {
                AppendAllBrokenConstraints(Count);
            }
            else
            {

                if (IrmosCount.Value < 0)
                {
                    AddBrokenConstraint(KanonasItemBusinessConstraint.IrmosCountInvalid, ElementName);
                }
            }

            if (!Martyrion.IsValid)
            {
                AppendAllBrokenConstraints(Martyrion);
            }
        }
    }
}
