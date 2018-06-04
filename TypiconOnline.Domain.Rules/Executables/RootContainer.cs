using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Executables
{
    /// <summary>
    /// Корневой элемент Правил
    /// </summary>
    public class RootContainer: ExecContainer, IAsAdditionElement
    {
        public RootContainer(string name) : base(name) { }

        #region IRewritableElement implementation

        public IAsAdditionElement Parent { get; }

        public string AsAdditionName
        {
            get
            {
                string result = ElementName;

                if (Parent != null)
                {
                    result = $"{Parent.AsAdditionName}/{result}";
                }

                return result;
            }
        }

        /// <summary>
        /// Поле никак не используется
        /// </summary>
        public AsAdditionMode AsAdditionMode { get; set; }

        /// <summary>
        /// Ничего не делаем.
        /// </summary>
        /// <param name="source"></param>
        public void RewriteValues(IAsAdditionElement source) { }

        #endregion


    }
}
