using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule.Extensions
{
    /// <summary>
    /// Вспомогательный класс для формирования последовательности канонов
    /// </summary>
    public static class KanonesExtensions
    {
        public static void Calculate(this List<Kanonas> kanonesCalc, RuleHandlerSettings handlerSettings, IReadOnlyList<KKanonasItemRule> kanones,
            IReadOnlyList<KKatavasiaRule> katavasiaColection, bool isOrthros)
        {
            if (kanonesCalc == null)
            {
                kanonesCalc = new List<Kanonas>();
            }
            else
            {
                kanonesCalc.Clear();
            }

            var katavasia = katavasiaColection.FirstOrDefault();

            for (int i = 0; i < kanones.Count; i++)
            {
                KKanonasItemRule item = kanones[i] as KKanonasItemRule;

                if (item.Calculate(handlerSettings) is Kanonas k)
                {
                    kanonesCalc.Add(k);
                }

                //это правило для канона Утрени, определение катавасии отсутствует и канон последний
                if (isOrthros && katavasia == null && i == kanones.Count - 1)
                {
                    //добавляем еще один канон, который будет состоять ТОЛЬКО из катавасий после 3, 6, 8, 9-х песен
                    if (item.CalculateEveryDayKatavasia(handlerSettings) is Kanonas k1)
                    {
                        kanonesCalc.Add(k1);
                    }
                }
            }

            if (katavasia != null)
            {
                kanonesCalc.Add(katavasia.Calculate(handlerSettings) as Kanonas);
            }
        }

        /// <summary>
        /// Вычисляет, является ли данный канон описанием катавасий
        /// </summary>
        /// <param name="kanonas"></param>
        /// <returns></returns>
        public static bool IsKatavasia(this Kanonas kanonas)
        {
            return kanonas.Odes.TrueForAll(odi => odi.Troparia.TrueForAll(troparion => troparion.Kind == YmnosKind.Katavasia));
        }

        /// <summary>
        /// Вычисляет, является ли канон с указанным индексом в коллекции последним, 
        /// за которым далее будут только катавасии.
        /// </summary>
        /// <param name="kanones"></param>
        /// <param name="index">Индекс запрашиваемого канона</param>
        /// <returns></returns>
        public static bool IsLastKanonasBeforeKatavasia(this IReadOnlyList<Kanonas> kanones, int index)
        {
            bool result = false;

            if (index == kanones.Count - 1)
            {
                //если канон в принципе последний, то возвращаем true
                result = true;
            }
            else
            {
                //смотрим следующий канон - если он катавасия, возвращаем true
                result = kanones[index + 1].IsKatavasia();
            }

            return result;
        }
    }
}
