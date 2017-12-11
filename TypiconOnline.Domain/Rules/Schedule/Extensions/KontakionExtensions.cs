using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Rules.Schedule.Extensions
{
    public static class KontakionExtensions
    {
        public static YmnosStructure ToYmnosStructure(this Kontakion kontakion, bool includeIkos = true)
        {
            //конвертируем структуру кондака в YmnosGroup
            YmnosGroup group = new YmnosGroup()
            {
                Annotation = (kontakion.Annotation != null) ? new ItemText(kontakion.Annotation) : null,
                Ihos = kontakion.Ihos,
                Prosomoion = (kontakion.Prosomoion != null) ? new Prosomoion(kontakion.Prosomoion) : null,
                Ymnis = new List<Ymnos>() { new Ymnos() { Text = kontakion.Ymnos } }
            };

            if (includeIkos && kontakion.Ikos != null)
            {
                group.Ymnis.Add(new Ymnos() { Text = kontakion.Ikos });
            }

            YmnosStructure result = new YmnosStructure();
            result.Groups.Add(group);

            return result;
        }
    }
}
