using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Irmologion;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books
{
    public partial class BookStorage
    {
        /// <summary>
        /// Хранилище текстов Ирмология
        /// </summary>
        public class IrmologionClass
        {
            public ItemText GetTheotokion(GetTheotokionRequest request)
            {
                //TODO: реализовать механизм и хранения, и получения
                string expression = @"<text>
	                                    <cs-cs>Всемjрную слaву t человBкъ прозsбшую, и3 вLку р0ждшую, нбcную двeрь воспои1мъ мRjю дв7у, безпл0тныхъ пёснь, и3 вёрныхъ ўдобрeніе: сіs бо kви1сz нб7о, и3 хрaмъ бжcтвA: сіS преграждeніе вражды2 разруши1вши, ми1ръ введE, и3 цrтвіе tвeрзе: сію2 ќбw и3мyще вёры ўтверждeніе, поб0рника и4мамы и3зъ неS р0ждшагосz гDа. Дерзaйте u5бо, дерзaйте, лю1діе б9іи, и4бо т0й побэди1тъ враги2, ћкw всеси1ленъ.</cs-cs>
                                    </text>";
                return new ItemText(expression);
            }
        }

        #region Singletone pattern

        public static IrmologionClass Irmologion
        {
            get { return NestedIrmologion.instance; }
        }

        private class NestedIrmologion
        {
            static NestedIrmologion()
            {
            }

            internal static readonly IrmologionClass instance = new IrmologionClass();
        }

        #endregion
    }
}
